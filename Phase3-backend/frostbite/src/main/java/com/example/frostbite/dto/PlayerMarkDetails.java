package com.example.frostbite.dto;

public class PlayerMarkDetails {
	public String username;
	private Integer marks;
	//All Args Constructor
	PlayerMarkDetails(String username,Integer marks ){
		this.username=username;
		this.marks=marks;
	}
	//No Args Constructor
	PlayerMarkDetails(){}
	//Getters and Setters
	public Integer getMarks() {
		return marks;
	}
	public void setMarks(Integer marks) {
		this.marks = marks;
	}
	public String getUsername() {
		return username;
	}
	public void setUsername(String username) {
		this.username = username;
	}
}
