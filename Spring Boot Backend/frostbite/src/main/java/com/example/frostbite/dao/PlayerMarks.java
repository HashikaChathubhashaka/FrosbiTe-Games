package com.example.frostbite.dao;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;

@Entity
public class PlayerMarks {
	@Id
	@GeneratedValue(strategy= GenerationType.AUTO)
	private Integer id;
	//-----------------Attributes of PlayerMMarks Data Access Object
	public String username;
	private Integer marks;

	public String getUsername() {
		return username;
	}
	public void setUsername(String username) {
		this.username = username;
	}
	public Integer getMarks() {
		return marks;
	}
	public void setMarks(Integer marks) {
		this.marks = marks;
	}
}
