import React, { useState, useEffect } from "react";
import logo from './logo.svg';
import './App.css';

import * as Realm from "realm-web";
const REALM_APP_ID = "leaderboard-anwug";
const app = new Realm.App({ id: REALM_APP_ID });

function App() {
  const [user, setUser] = React.useState(app.currentUser);
  const [scores, setScores] = React.useState([]);

  useEffect(() => {
    const loginAnonymous = async () => {
      console.log("Logging in.")
      const user = await app.logIn(Realm.Credentials.anonymous());
      setUser(user);
      console.log(await user.functions.winners());
      console.log("done.")
    }
    loginAnonymous();
  }, []);

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React, Maybe?
        </a>
      </header>
    </div>
  );
}

export default App;
