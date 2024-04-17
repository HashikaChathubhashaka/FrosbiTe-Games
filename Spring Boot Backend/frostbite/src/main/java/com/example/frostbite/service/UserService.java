package com.example.frostbite.service;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.Optional;
import java.util.stream.StreamSupport;

import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.frostbite.dao.Players;
import com.example.frostbite.dto.PlayerDetails;
import com.example.frostbite.repositry.PlayerRepositry;


@Service
public class UserService {
	@Autowired
	PlayerRepositry playerRepositry; // Autowired Dependency Injection
	@Autowired
	ModelMapper mapper;
	
	
	public List<PlayerDetails> getAllPlayers() {
		// TODO load allthe users
		List<PlayerDetails> playerDetails = new ArrayList<>();
		playerRepositry.findAll().forEach((player)->{
			playerDetails.add(this.mapper.map(player, PlayerDetails.class));
		});
		//users.add(new UserDetails("user1","user1@abc.com"));
		return playerDetails;
	}
	
	public void addPlayer(PlayerDetails playerDetails) {
		//this.playerRepositry.save(mapper.map(playerDetails,  Player.class));
		this.playerRepositry.save (mapper.map(playerDetails, Players.class));
		
	}

    public PlayerDetails getPlayerByUsername(String username) {
        Players player = StreamSupport.stream(playerRepositry.findAll().spliterator(), false)
                                     .filter(p -> {
                                         String playerName = p.getUsername();
                                         if(playerName != null ) {return playerName.equals(username);} //insintric exception handling for nullpointer
                                         else {return false;}
                                     })
                                     .findFirst()
                                     .orElse(null);
        
        if (player != null) {
            return mapper.map(player, PlayerDetails.class);
        } else {
            throw new RuntimeException("Player with username " + username + " not found"); // Throwing exception
        }
    }
    
    public boolean checkPlayer(String username) {
        Players player = StreamSupport.stream(playerRepositry.findAll().spliterator(), false)
                .filter(p -> {
                    String playerName = p.getUsername();
                    if(playerName != null ) {return playerName.equals(username);}
                    else {return false;}
                })
                .findFirst()
                .orElse(null);
    if (player==null) {return false;}
    else {return true;}
    }
    
	

	
}