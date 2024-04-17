package com.example.frostbite.repositry;

// Player Repo Using OOP concepts 
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.CrudRepository;
import org.springframework.data.repository.query.Param;

import com.example.frostbite.dao.Players;

public interface PlayerRepositry extends CrudRepository<Players, Integer> {

}