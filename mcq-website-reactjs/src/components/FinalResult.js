import React from 'react';
import './FinalResult.css';


function FinalResult({ numberOfCorrectAnswers, problems }) {
  return (
    <div>
      <h2 style={{ color: '#fff' , textAlign: 'center'  , marginBottom: '20px' }} > Number of correct answers :  {numberOfCorrectAnswers} </h2>
      <h4 style={{ color: '#fff' , marginTop: '20px', marginBottom: '30px' , textAlign: 'center' }} > You can close this tab and continue in Game.</h4>
      <div class="text-correct-answer"> Here are the correct answers:</div>
      <ul>
        {problems.map((problem, index) => (
          <li key={index}>
            Q{index+1}.   {problem.question} - {problem.correctAnswer}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default FinalResult;
