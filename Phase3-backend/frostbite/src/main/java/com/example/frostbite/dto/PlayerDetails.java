package com.example.frostbite.dto;

import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.RequiredArgsConstructor;
import lombok.Setter;

//@Getter
//@Setter
//@NoArgsConstructor
//@RequiredArgsConstructor
public class PlayerDetails {
	// All args constructor
	public PlayerDetails(String username,String email, String firstname,String secondname,
			String nic, String quizmark, String quiztaken) {
		this.setUsername(username);
		this.setEmail(email);
		this.setFirstname(firstname);
		this.setSecondname(secondname);
		this.setNic(nic);
		this.setQuizmark(quizmark);
		this.setQuiztaken(quiztaken);
	}
	//Noargs constructor
	public PlayerDetails() {}
	
	public String getUsername() {
		return username;
	}
   // Getters and Setters
	public void setUsername(String username) {
		this.username = username;
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

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
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

	private String username;
	private String email;
	private String firstname;
	private String secondname;
	private String nic;
	private String quizmark;
	private String quiztaken;


}
