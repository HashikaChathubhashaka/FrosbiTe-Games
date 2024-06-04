package com.example.frostbite.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.example.frostbite.dto.PlayerScoreDetails;
import com.example.frostbite.service.ScoreService;

@RestController
@RequestMapping("score")
public class ScoreController {
	
    @Autowired
    private ScoreService scoreService;
	
    @GetMapping("all")
    public List<PlayerScoreDetails> getAllPlayerScores() {
        return scoreService.getAllPlayerScores();
    }
    @PostMapping("add")
    public void addScore(@RequestBody PlayerScoreDetails playerScoreDetails) {
        scoreService.addScores(playerScoreDetails);
    }
    @PostMapping("getscore")
    public PlayerScoreDetails getPlayerScore(@RequestBody String username) {
        return scoreService.getPlayerScore(username);
    }
    
    @PutMapping("updatescore")
    public void updateScore(@RequestBody PlayerScoreDetails playerScoreDetails) {
        if (playerScoreDetails.getUsername() == null) {
            throw new IllegalArgumentException("Username is required for updating player details.");
        }
        scoreService.updatePlayerScore(playerScoreDetails.getUsername(), playerScoreDetails);
    }
}
