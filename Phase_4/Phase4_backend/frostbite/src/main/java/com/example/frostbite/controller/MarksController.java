package com.example.frostbite.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.example.frostbite.dto.PlayerMarkDetails;
import com.example.frostbite.service.MarkService;

@RestController
@RequestMapping("marks")
public class MarksController {
	@Autowired
	MarkService markService;
	//---------------------------------Routes for methods in Markservice
	@PostMapping("addmarks")
	void createPlayer(@RequestBody PlayerMarkDetails playerMarkDetails) {
		 markService.addMarks(playerMarkDetails);
	}
    @PostMapping("getmarks")
    public PlayerMarkDetails getMarks(@RequestBody String username) {
        return markService.getMarks(username);
    }
    @PostMapping("checkuser")
    public Boolean checkUser(@RequestBody String username) {
        return markService.checkUser(username);
    }
}
