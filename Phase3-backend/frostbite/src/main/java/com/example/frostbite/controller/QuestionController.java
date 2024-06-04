package com.example.frostbite.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.example.frostbite.dto.QuestionDetails;
import com.example.frostbite.service.QuestionService;

@RestController
@RequestMapping("question")
public class QuestionController {
	@Autowired
	QuestionService questionService;
	//-------------------------------Routes for Methods in questionService
	@GetMapping("questions")
	List<QuestionDetails> getAllQuestion(){
		return questionService.getAllQuestions();
	}
	@PostMapping("addquestion")
	void createQuestion(@RequestBody QuestionDetails questionDetails) {
		questionService.addQuestion(questionDetails);
	}
}
