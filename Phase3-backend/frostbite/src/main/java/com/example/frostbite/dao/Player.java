package com.example.frostbite.dao;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import lombok.Getter;
import lombok.Setter;
@Getter
@Setter
@Entity
public class Player {
	@Id
	@GeneratedValue(strategy= GenerationType.AUTO)
	private Integer id;
	//-----------------Attributes of Player Data Access Object
	private String username;
	private String email;
	private String firstname;
	private String secondname;
	private String nic;
	private String quizmark;
	private String quiztaken;


}
