import React, { useState, useEffect } from "react";
import LeaderBoard from "./LeaderBoard.js";
import './App.css';

import * as Realm from "realm-web";
const REALM_APP_ID = "leaderboard-anwug";
const app = new Realm.App({ id: REALM_APP_ID });

function App() {
  //const [user, setUser] = React.useState(app.currentUser);
  const [scores, setScores] = useState([]);

  useEffect(() => {
    const loginAnonymous = async () => {
      const user = await app.logIn(Realm.Credentials.anonymous());
      //setUser(user);
      var winners = await user.functions.winners()
      setScores(winners);

      // TODO - unregister watch when unmounting component etc.
      const winnersCollection = user.mongoClient("mongodb-atlas").db("reinvent_demo").collection("scores");
      for await (const change of winnersCollection.watch()) {
        console.log("Collection updated.")
        winners = await user.functions.winners()
        setScores(winners);
      }
    }
    loginAnonymous();
  }, []);


  function shuffle(a) {
    let array = [...a];
    let currentIndex = array.length, randomIndex;

    // While there remain elements to shuffle...
    while (currentIndex !== 0) {

      // Pick a remaining element...
      randomIndex = Math.floor(Math.random() * currentIndex);
      currentIndex--;

      // And swap it with the current element.
      [array[currentIndex], array[randomIndex]] = [
        array[randomIndex], array[currentIndex]];
    }

    return array;
  }
  const onClick = () => {
    console.log("Shuffle");
    setScores(shuffle(scores));
    console.log(scores);
  };

  return (
    <div className="App">
      <button onClick={onClick}>Shuffle</button>
      <LeaderBoard winners={scores} />
    </div>
  );
}

export default App;
