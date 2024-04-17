import React, { useEffect, useState } from "react";
import axios from 'axios';
import { login, getUserProfile } from '../api/playerAPI';
// import { getToken } from '../api/databaseAPI';

import './mcq_site.css';
import ProgressBar from './ProgressBar';
import Problem from './Problem';
import FinalResult from './FinalResult';

import problemData from "./problemData";

function MCQSite() {
  const [problems] =  useState(problemData);
  const [currentProblemIndex, setCurrentProblemIndex] = useState(0);
  const [selectedAnswer, setSelectedAnswer] = useState("");
  const [selectedAnswerIndex, setSelectedAnswerIndex] = useState(null);
  const [showResult, setShowResult] = useState(false);
  const [showFinalResult, setShowFinalResult] = useState(false);
  const [numberOfCorrectAnswers , setnumberOfCorrectAnswers]= useState(0);

  const currentProblem = problems[currentProblemIndex];


  // For Backend
  const [token_1, settoken_1] = useState(""); 
  const [playername,setplayername] = useState("");

  // useEffect(() => {
  //   const fetchData = async () => {
  //     try {
  //       const token = await login();
  //       settoken_1(token);
  //       console.log('Token:', token);
  //     } catch (error) {
  //       console.error('Error:', error);
  //     }
  //   };

  //   fetchData();
  // }, []);

  // useEffect(() => {
  //   const fetchUserProfile = async () => {
  //     if (token_1) {
  //       try {
  //         const username = await getUserProfile(token_1);
  //         setplayername(username);
  //         console.log('Profile Response:', username);
  //       } catch (error) {
  //         console.error('Error:', error);
  //       }
  //     }
  //   };

  //   fetchUserProfile();
  // }, [token_1]);









  useEffect(() => {
    const fetchToken = async () => {
      try {
  
        const response = await 
        axios.post('http://localhost:8080/token', {
            headers: {
              'Authorization': 'Basic dXNlcjpwYXNzd29yZA=='
            }
        });
        console.log('Token:', response.data);
      } catch (error) {
        console.error('Error:', error);
      }
    };
  
    fetchToken();
  }, []);















  const onClickAnswer = (answer, index) => {
    if (!showResult) {
      setSelectedAnswer(answer);
      setSelectedAnswerIndex(index);
      setShowResult(true);
  
      // Check if the selected answer is correct
      if (answer === currentProblem.correctAnswer) {
        // If correct, increment numberOfCorrectAnswers
        setnumberOfCorrectAnswers(prevCount => prevCount + 1);
        
      }
    }
  };

  const onNextProblem = () => {
    setSelectedAnswer("");
    setSelectedAnswerIndex(null);
    setShowResult(false);
    if (currentProblemIndex + 1 < problems.length) {
      setCurrentProblemIndex(prevIndex => prevIndex + 1);
    } else {
      setShowFinalResult(true);
    }
  };



  return (

    <div>
      <div className="playername">Player - <span style={{ fontWeight: 'bold' }}>{playername}</span></div>

      {showFinalResult ? null : (
      <ProgressBar currentProblemIndex={currentProblemIndex} problems={problems} />
      )}

      {showFinalResult ? (
        <FinalResult numberOfCorrectAnswers={numberOfCorrectAnswers} problems={problems} />
      ) : (
        <Problem
          currentProblem={problems[currentProblemIndex]}
          showFinalResult={showFinalResult}
          onClickAnswer={onClickAnswer}
          selectedAnswer={selectedAnswer}
          selectedAnswerIndex={selectedAnswerIndex}
          showResult={showResult}
          onNextProblem={onNextProblem}
        />
      )}
    </div>
  );
}

export default MCQSite;
