package com.example.frostbite;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.properties.EnableConfigurationProperties;

import com.example.frostbite.config.RsaKeyProperties;
@EnableConfigurationProperties(RsaKeyProperties.class)
@SpringBootApplication
public class FrostbiteApplication {

	public static void main(String[] args) {
		SpringApplication.run(FrostbiteApplication.class, args);
	}

}
