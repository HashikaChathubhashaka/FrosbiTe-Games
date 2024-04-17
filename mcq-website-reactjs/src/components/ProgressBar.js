import React from 'react';
import './ProgressBar.css';


function ProgressBar({ currentProblemIndex, problems }) {
    function getProgressBarColor() {
        const progressValue = ((currentProblemIndex + 1) / problems.length) * 100;
        if (progressValue <= 10) {
          return "#f21111";
        } else if (progressValue <= 20) {
          return "#f23711";
        } else if (progressValue <= 30) {
          return "#f25c11";
        } else if (progressValue <= 40) {
          return "#f28211";
        } else if (progressValue <= 50) {
          return "#f2a711";
        } else if (progressValue <= 60) {
          return "#f2cc11";
        } else if (progressValue <= 70) {
          return "#f2f211";
        } else if (progressValue <= 80) {
          return "#cdf211";
        } else if (progressValue <= 90) {
          return "#a7f211";
        } else if (progressValue <= 100) {
          return "#11f228";
        }
        
        else {
          return "white";
        }
      }

  return (
    <div className="progress">
      <div className="progress-bar" style={{ width: `${((currentProblemIndex ) / problems.length) * 100}%`, backgroundColor: getProgressBarColor() }}></div>
    </div>
  );
}

export default ProgressBar;