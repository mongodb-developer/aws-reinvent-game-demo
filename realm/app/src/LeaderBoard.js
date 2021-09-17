import LineItem from "./LineItem.js";
import "./LeaderBoard.css";

function LeaderBoard({ winners }) {
    const winnerItems = winners.map((winner) => <LineItem winner={winner} key={winner.username} />);

    return (
        <div className="leaderboard">
            <h1>Leaderboard</h1>
            <table className="leaderboard-items">
                <tbody>
                    {winnerItems}
                </tbody>
            </table>
        </div>
    );
}

export default LeaderBoard;