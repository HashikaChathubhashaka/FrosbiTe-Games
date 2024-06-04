package com.example.frostbite.dao;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import lombok.Data;

//@Data
@Entity
public class Questions {
	@Id
	@GeneratedValue(strategy = GenerationType.SEQUENCE)
	private Integer id;
	//-----------------Attributes of Question Data Access Object
	private String questionTitle;
    private String option1;
    private String option2;
    private String option3;
    private String option4;
    private String rightAnswer;
    private String feedback1;
    private String feedback2;
    private String feedback3;
    private String feedback4;
    private String generalFeedback;
    private String category;
	public String getOption1() {
		return option1;
	}
	public void setOption1(String option1) {
		this.option1 = option1;
	}
	public String getOption3() {
		return option3;
	}
	public void setOption3(String option3) {
		this.option3 = option3;
	}
	public String getOption2() {
		return option2;
	}
	public void setOption2(String option2) {
		this.option2 = option2;
	}
	public String getOption4() {
		return option4;
	}
	public void setOption4(String option4) {
		this.option4 = option4;
	}
	public String getRightAnswer() {
		return rightAnswer;
	}
	public void setRightAnswer(String rightAnswer) {
		this.rightAnswer = rightAnswer;
	}
	public String getFeedback1() {
		return feedback1;
	}
	public void setFeedback1(String feedback1) {
		this.feedback1 = feedback1;
	}
	public String getFeedback3() {
		return feedback3;
	}
	public void setFeedback3(String feedback3) {
		this.feedback3 = feedback3;
	}
	public String getFeedback2() {
		return feedback2;
	}
	public void setFeedback2(String feedback2) {
		this.feedback2 = feedback2;
	}
	public String getFeedback4() {
		return feedback4;
	}
	public void setFeedback4(String feedback4) {
		this.feedback4 = feedback4;
	}
	public String getGeneralFeedback() {
		return generalFeedback;
	}
	public void setGeneralFeedback(String generalFeedback) {
		this.generalFeedback = generalFeedback;
	}
	public String getCategory() {
		return category;
	}
	public void setCategory(String category) {
		this.category = category;
	}
	public String getQuestionTitle() {
		return questionTitle;
	}
	public void setQuestionTitle(String questionTitle) {
		this.questionTitle = questionTitle;
	}

}
