import LineItem from "./LineItem.js";
import "./LeaderBoard.css";
import { Flipper } from "react-flip-toolkit";
import React from "react";

function LeaderBoard({ winners }) {
  console.log("Winners:", winners);
  const flipKey = Object.entries(winners)
    .map(([username, winner]) => username)
    .join("");
  const winnerItems = Object.entries(winners).map(([username, winner]) => (
    <LineItem key={username} winner={{ username, ...winner }} />
  ));

  return (
    <div className="leaderboard">
      <h1>Leaderboard</h1>
      <Flipper className="leaderboard-items" flipKey={flipKey}>
        {winnerItems}
      </Flipper>
    </div>
  );
}

export default LeaderBoard;
