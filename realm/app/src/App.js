import React, { useState, useEffect } from "react";
import LeaderBoard from "./LeaderBoard.js";
import logo from './logo.svg';
import './App.css';

import * as Realm from "realm-web";
const REALM_APP_ID = "leaderboard-anwug";
const app = new Realm.App({ id: REALM_APP_ID });

function App() {
  //const [user, setUser] = React.useState(app.currentUser);
  const [scores, setScores] = React.useState([]);

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

  return (
    <div className="App">
      <LeaderBoard winners={scores} />
    </div>
  );
}

export default App;
