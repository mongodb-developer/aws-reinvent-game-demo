import React from "react";
import { Flipped } from 'react-flip-toolkit';

function LineItem({ winner, ...rest }) {
    return <React.Fragment>
        <Flipped flipId={winner.username}>
            <div className="username line-item">{winner.username}</div>
        </Flipped>
        <Flipped flipId={winner.username + '---score'}>
            <div className="score line-item">{winner.score}</div>
        </Flipped>
    </React.Fragment>

}

export default LineItem;