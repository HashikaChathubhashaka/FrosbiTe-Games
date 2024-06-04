package com.example.frostbite.repositry;

import org.springframework.data.repository.CrudRepository;

import com.example.frostbite.dao.PlayerMarks;

// Player Mark Repo using OOP concepts
public interface PlayerMarkRepositry extends CrudRepository<PlayerMarks, Integer> {

}