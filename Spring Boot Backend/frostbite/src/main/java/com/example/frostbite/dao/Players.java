package com.example.frostbite.dao;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import lombok.Getter;
import lombok.Setter;
//@Getter
//@Setter
@Entity
public class Players {
	@Id
	@GeneratedValue(strategy= GenerationType.AUTO)
	private Integer id;
	//-----------------Attributes of Players Data Access Object
	public String username;
	private String email;
	private String firstname;
	private String secondname;
	private String nic;
	private String quizmark;
	private String quiztaken;
	public String getUsername() {
		return username;
	}
	public void setUsername(String username) {
		this.username = username;
	}
	public String getEmail() {
		return email;
	}
	public void setEmail(String email) {
		this.email = email;
	}
	public String getFirstname() {
		return firstname;
	}
	public void setFirstname(String firstname) {
		this.firstname = firstname;
	}
	public String getSecondname() {
		return secondname;
	}
	public void setSecondname(String secondname) {
		this.secondname = secondname;
	}
	public String getNic() {
		return nic;
	}
	public void setNic(String nic) {
		this.nic = nic;
	}
	public String getQuizmark() {
		return quizmark;
	}
	public void setQuizmark(String quizmark) {
		this.quizmark = quizmark;
	}
	public String getQuiztaken() {
		return quiztaken;
	}
	public void setQuiztaken(String quiztaken) {
		this.quiztaken = quiztaken;
	}
	public Integer getId() {
		// TODO Auto-generated method stub
	if(id!=null) {return id;}
	else {return 0;}
	}


}
