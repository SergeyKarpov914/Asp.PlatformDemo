import React from 'react'
import { useState } from 'react';
import './App.css';

function App() {

  const [morseMessage, Message] = useState("");
  const [downAction, setDownMessage] = useState("->");
  const [upAction, setUpMessage] = useState("<-");

  const [downTime, setDownTime] = useState(0);
  const [upTime, setUpTime] = useState(0);

  function setTime() {
    return new Date().getTime();}

  const playAudio = () => {
    let path = require("./beep.mp3").default;
    const audio = new Audio(path);
   const audioPromise = audio.play();
    if (audioPromise !== undefined) {
        audioPromise
            .then(() => {
                console.log("works");
            })
            .catch((err) => {
                console.info(err);
            });
    }
  }

  function handleDown() {
    setDownMessage(downAction + " down");
    setDownTime(setTime());

    baseTime = downTime;
    console.info("Mouse down:" + downTime + "   " + baseTime);
  }  
  function handleUp() {
    setUpMessage(upAction + " up");
    
    console.log("playing beep");
    playAudio();

    setUpTime(setTime());
    let delay = upTime - baseTime;
    
    console.log("Mouse down: " + upTime + " - " + baseTime + " = " + delay);
  }  
   
  return (
    <div className="App">
      <label>
        Write your post:
        <br/><br/><br/>
        <textarea name="postContent" rows={4} cols={40}>{morseMessage}</textarea>
      </label> 
      <br /><br /><br /><br />
      <button onMouseDown={() => handleDown()} onMouseUp={() => handleUp()}>Morse Key</button>
      <br />
    </div>
  );
}

export default App;
