package com.example.frostbite.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.example.frostbite.dto.PlayerDetails;
import com.example.frostbite.service.UserService;

@RestController
@RequestMapping("user")
public class UserController {
	@Autowired
	UserService userService;
	//---------------------------------------Routes for User Service
	@GetMapping("players")
	List<PlayerDetails> getAllPlayers(){
		return userService.getAllPlayers();
	}
	@PostMapping("addplayer")
	void createPlayer(@RequestBody PlayerDetails playerDetails) {
		 userService.addPlayer(playerDetails);
	}
	
    @PostMapping("/players/getByUsername")
    public PlayerDetails getPlayerByUsername(@RequestBody String username) {
        return userService.getPlayerByUsername(username);
    }
    
    @PostMapping("checkplayer")
    public Boolean checkPlayer(@RequestBody String username) {
        return userService.checkPlayer(username);
    }
}

