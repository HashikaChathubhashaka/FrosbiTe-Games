package com.example.frostbite.dto;

public class PlayerScoreDetails {
	public String username;
	private Integer score;
	//All Args Constructor
	PlayerScoreDetails(String username,Integer score ){
		this.username=username;
		this.score=score;
	}
	//No Args Constructor
	PlayerScoreDetails(){}
	//Getters and Setters
	public Integer getScore() {
		return score;
	}
	public void setScore(Integer score) {
		this.score = score;
	}
	public String getUsername() {
		return username;
	}
	public void setUsername(String username) {
		this.username = username;
	}
}
