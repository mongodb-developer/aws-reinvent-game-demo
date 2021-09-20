import LineItem from "./LineItem.js";
import "./LeaderBoard.css";
import { Flipper } from 'react-flip-toolkit';
import React from "react";

function LeaderBoard({ winners }) {
    const flipKey = winners.map((winner) => winner.username).join('');
    const winnerItems = winners.map((winner) => (
        <LineItem key={winner.username} winner={winner} />
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