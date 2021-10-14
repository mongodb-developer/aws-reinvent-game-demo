import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import "./Particles.css";
import initParticles from "./Particles.js";

function onClick() {
  console.log("click");
  console.log(document.getElementById("particles").dataset.boom);
  document.getElementById("particles").__particles.boom.kaboom();
}

ReactDOM.render(
  <React.StrictMode>
    <App onClick={onClick} />
    <div id="particles" />
  </React.StrictMode>,
  document.getElementById("root")
);

initParticles("particles");

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
