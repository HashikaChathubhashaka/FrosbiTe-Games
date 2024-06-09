package com.example.frostbite.service;

import java.util.ArrayList;
import java.util.List;

import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.frostbite.dto.QuestionDetails;
import com.example.frostbite.repositry.QuestionRepositry;
import com.example.frostbite.dao.Questions;

@Service
public class QuestionService {
	@Autowired
	QuestionRepositry questionRepositry; // Autowired Dependency Injection
	@Autowired
	ModelMapper mapper;
	
	
	public List<QuestionDetails> getAllQuestions() {
		// TODO load allthe users
		List<QuestionDetails> questionDetails = new ArrayList<>();
		questionRepositry.findAll().forEach((question)->{
			questionDetails.add(this.mapper.map(question, QuestionDetails.class));
		});
		//users.add(new UserDetails("user1","user1@abc.com"));
		return questionDetails;
	}
	
	public void addQuestion(QuestionDetails questionDetails) {
		//this.questionRepositry.save (mapper.map(questionDetails,  Questions.class));
		this.questionRepositry.save(mapper.map(questionDetails, Questions.class));
	}
}
