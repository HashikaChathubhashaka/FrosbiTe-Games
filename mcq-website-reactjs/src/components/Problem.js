import React from 'react';
import './Problem.css';
import './Problem.scss';

function Problem({ currentProblem, onClickAnswer, selectedAnswer, selectedAnswerIndex, showResult, onNextProblem }) {
  return (
        <div>
          <h2 className="text-center mb-4" style={{ color: '#fff' }}>{currentProblem.question}</h2>
          <div className="row">
            {currentProblem.options.map((option, index) => (
              <div key={index} className="col-md-6" style={{ marginBottom: '30px' }}>

                <button
                  onClick={() => onClickAnswer(option, index)}
                  disabled={showResult}
                  className={`btn ${showResult && option === currentProblem.correctAnswer ? 'btn-success' : option === selectedAnswer ? 'btn-danger' : 'btn-secondary'} w-100`}>
                  {option}
                
                </button>

                {showResult && index === selectedAnswerIndex && (
                  <div>

                {selectedAnswer === currentProblem.correctAnswer ? (
                <p className="text-correct  d-flex justify-content-center">{currentProblem.Specific_Feedback[index]}</p>
              ) : (
                <p className="text-wrong  d-flex justify-content-center"> {currentProblem.Specific_Feedback[index]}</p>

              )}

                  </div>
                )}
              </div>
            ))}
          </div>

          {showResult && (
            <div>

              <p className="mt-4 d-flex justify-content-center text-center text-feedback">{currentProblem.General_Feedback}</p>
              <div className="d-flex justify-content-center">
                <button class="next" onClick={onNextProblem}>
                  <span class="circle" aria-hidden="true">
                  <span class="icon arrow"></span>
                  </span>
                  <span class="button-text"> Next </span>
                </button>
              </div>
            </div>
          )}
        </div>
  );
}

export default Problem;