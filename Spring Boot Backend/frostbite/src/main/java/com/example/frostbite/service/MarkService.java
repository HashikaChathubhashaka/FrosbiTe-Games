package com.example.frostbite.service;

import java.util.stream.StreamSupport;

import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.frostbite.dao.PlayerMarks;
import com.example.frostbite.dto.PlayerMarkDetails;
import com.example.frostbite.repositry.PlayerMarkRepositry;



@Service
public class MarkService {
	@Autowired
	PlayerMarkRepositry playerMarkRepositry; // Autowired Dependency Injection
	@Autowired
	ModelMapper mapper;
	
	public void addMarks(PlayerMarkDetails playerMarkDetails) {
		//this.playerRepositry.save(mapper.map(playerDetails,  Player.class));
		this.playerMarkRepositry.save (mapper.map(playerMarkDetails, PlayerMarks.class));
		
	}
	
    public PlayerMarkDetails getMarks(String username) {
        PlayerMarks player = StreamSupport.stream(playerMarkRepositry.findAll().spliterator(), false)
                                     .filter(p -> {
                                         String playerName = p.getUsername();
                                         if(playerName != null ) {return playerName.equals(username);}
                                         else {return false;}
                                     })
                                     .findFirst()
                                     .orElse(null);
        
        if (player != null) {
            return mapper.map(player, PlayerMarkDetails.class);
        } else {
            throw new RuntimeException("Player with username " + username + " not found");
        }
    }
    
    public boolean checkUser(String username) {
        PlayerMarks player = StreamSupport.stream(playerMarkRepositry.findAll().spliterator(), false)
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
