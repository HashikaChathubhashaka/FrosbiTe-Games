import React from "react";

import MCQ from "./components/mcq_site";
import "./App.css";

//Adding Logos
import TeamLogo from "./images/team_logo.png"; 
import GameLogo from "./images/game_logo.png";

function App() {
  return (

    <div className="wrapper">
      <div className="content">

        {/* header */}
        <div className="text-center bg-dark text-light py-3" style={{ whiteSpace: 'nowrap' }}>
          <div className="container">
            <img src={GameLogo} alt="Game Logo" className="logo" />
          </div>
    </div>

        {/* main content */}
        <div className="main-content">
          <ul className="circles">
            {[...Array(10)].map((_, index) => (
              <li key={index}></li>
            ))}
          </ul>
          <MCQ title="MCQ" />
        </div>
      </div>

      {/* footer */}
      <footer className="footer text-center bg-dark text-light py-2" style={{ position: 'relative' }}>
        <p>You can Gain More Coins by selecting More Correct Answers</p>
        <hr style={{ color: 'white', width: '100%' }} />
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100%' }}>
          <img src={TeamLogo} alt="Team Logo" style={{ width: '280px', height: '60px' }} />
        </div>
      </footer>
    </div>
  );
}

export default App;
