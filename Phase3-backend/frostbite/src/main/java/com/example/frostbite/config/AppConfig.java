package com.example.frostbite.config;

import org.modelmapper.ModelMapper;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class AppConfig {
	// Bean for Model mapper
	@Bean
	ModelMapper getMapper() {
		return new ModelMapper();
	}

}