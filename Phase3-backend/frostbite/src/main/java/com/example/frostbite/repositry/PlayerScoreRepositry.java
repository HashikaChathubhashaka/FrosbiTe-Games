package com.example.frostbite.repositry;

import org.springframework.data.repository.CrudRepository;

import com.example.frostbite.dao.PlayerScores;


public interface PlayerScoreRepositry extends CrudRepository<PlayerScores, Integer> {

}
