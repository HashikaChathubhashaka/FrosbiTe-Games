package com.example.frostbite.service;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.StreamSupport;

import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.frostbite.dao.PlayerScores;
import com.example.frostbite.dto.PlayerScoreDetails;
import com.example.frostbite.repositry.PlayerScoreRepositry;

@Service
public class ScoreService {
	@Autowired
	PlayerScoreRepositry playerScoreRepositry;
	@Autowired
	ModelMapper mapper;
	
	public void addScores(PlayerScoreDetails playerScoreDetails) {
		this.playerScoreRepositry.save (mapper.map(playerScoreDetails, PlayerScores.class));	
	}
	
    public PlayerScoreDetails getPlayerScore(String username) {
        PlayerScores player = StreamSupport.stream(playerScoreRepositry.findAll().spliterator(), false)
                                     .filter(p -> {
                                         String playerName = p.getUsername();
                                         if(playerName != null ) {return playerName.equals(username);}
                                         else {return false;}
                                     })
                                     .findFirst()
                                     .orElse(null);
        
        if (player != null) {
            return mapper.map(player, PlayerScoreDetails.class);
        } else {
            throw new RuntimeException("Player with username " + username + " not found");
        }
    }
    
	public List<PlayerScoreDetails> getAllPlayerScores() {
		List<PlayerScoreDetails> playerScoreDetails = new ArrayList<>();
		playerScoreRepositry.findAll().forEach((player)->{
			playerScoreDetails.add(this.mapper.map(player, PlayerScoreDetails.class));
		});
		return playerScoreDetails;
	}
	
    public void updatePlayerScore(String username, PlayerScoreDetails playerScoreDetails) {
        PlayerScores existingPlayer = StreamSupport.stream(playerScoreRepositry.findAll().spliterator(), false)
                .filter(p -> username.equals(p.getUsername()))
                .findFirst()
                //.orElseThrow(() -> new RuntimeException("Player with username " + username + " not found"));
                .orElse(this.playerScoreRepositry.save (mapper.map(playerScoreDetails, PlayerScores.class)));
        // Map updated details to existing player
        existingPlayer.setScore(playerScoreDetails.getScore());

        playerScoreRepositry.save(existingPlayer);
    }
	
}
