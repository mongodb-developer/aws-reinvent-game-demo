import React, { useState, useEffect } from "react";
import LeaderBoard from "./LeaderBoard.js";
import "./App.css";

import * as Realm from "realm-web";
const REALM_APP_ID = "mongo_world_demo-ebxbj";
const app = new Realm.App({ id: REALM_APP_ID });

export function winnerDiff(oldWinners, newWinners) {
  if (oldWinners === null) {
    return Object.fromEntries(
      newWinners.map((newWinner, index) => {
        const username = newWinner.username;
        const score = newWinner.score;
        return [
          username,
          {
            score: score,
            change: null,
            index: index,
          },
        ];
      })
    );
  } else {
    return Object.fromEntries(
      newWinners.map((newWinner, index) => {
        const username = newWinner.username;
        const score = newWinner.score;
        const oldWinner =
          oldWinners === null || oldWinners[username] === undefined
            ? { index: null, score: null }
            : oldWinners[username];
        const previousIndex = oldWinner.index;
        const previousScore = oldWinner.score;
        let change = null; // Player's position and score are unchanged.
        if (previousIndex === null || index < previousIndex) {
          change = "up"; // Player has moved up at least one space.
        } else if (index > previousIndex) {
          change = "down"; // Player has been moved down (because someone else moved up)
        } else if (score > previousScore) {
          change = "inc"; // Player's score has increased, but no position change.
        }
        return [
          username,
          {
            score: score,
            change: change,
            index: index,
            previousIndex: previousIndex,
            previousScore: previousScore,
          },
        ];
      })
    );
  }
}

function App({ onClick }) {
  console.log("App render!!!");
  const [scores, setScores] = useState([]);

  useEffect(() => {
    const loginAnonymous = async () => {
      const user = await app.logIn(Realm.Credentials.anonymous());
      var winners = winnerDiff(null, await user.functions.winners());
      setScores(winners);

      const winnersCollection = user
        .mongoClient("mongodb-atlas")
        .db("mongo_world_demo")
        .collection("scores");
      const winnerStream = winnersCollection.watch();
      for await (const change of winnerStream) {
        winners = winnerDiff(winners, await user.functions.winners());
        setScores(winners);
      }

      return () => {
        winnerStream.return();
        console.log("Unregister from change stream.");
      };
    };
    loginAnonymous();
  }, []);

  useEffect(() => {
    const boom = () => {
      const increased = Object.entries(scores).filter(
        ([username, winner]) =>
          winner.change === "up" || winner.change === "inc"
      );
      if (increased.length > 0) {
        const element =
          document.querySelectorAll(".score")[increased[0][1].index];
        const rect = element.getBoundingClientRect();
        document.getElementById("particles").__particles.boom.kaboom({
          x: rect.left + (rect.right - rect.left) / 2,
          y: rect.top + (rect.bottom - rect.top) / 2,
        });
      }
    };
    boom();
  });

  return (
    <div className="App">
      <LeaderBoard winners={scores} />
    </div>
  );
}

export default App;
