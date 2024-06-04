package com.example.frostbite.repositry;

import org.springframework.data.repository.CrudRepository;

import com.example.frostbite.dao.Questions;

// Question Repo using OOP concepts
public interface QuestionRepositry extends CrudRepository<Questions, Integer> {

}