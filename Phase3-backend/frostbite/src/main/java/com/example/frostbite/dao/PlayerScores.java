package com.example.frostbite.dao;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;

@Entity
public class PlayerScores {
	@Id
	@GeneratedValue(strategy= GenerationType.AUTO)
	private Integer id;
	//-----------------Attributes of PlayerMMarks Data Access Object
	private String username;
	private Integer score;
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
