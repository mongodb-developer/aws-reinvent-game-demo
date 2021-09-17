import React from "react";

function LineItem({ winner }) {
    return <tr className="line-item">
        <td className="username">{winner.username}</td>
        <td className="score">{winner.score}</td>
    </tr>
}

export default LineItem;