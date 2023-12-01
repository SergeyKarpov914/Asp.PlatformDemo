import React from 'react'
import { useState } from 'react';
import {Text} from 'react-native';
import { convert } from './code2text.js';
import './App.css';

function App() {

  const [morseMessage, Message] = useState("");
  const [downAction, setDownMessage] = useState("->");
  const [upAction, setUpMessage] = useState("<-");

  const [downTime, setDownTime] = useState(0);
  const [upTime, setUpTime] = useState(0);
  const [holdTime, setHoldTime] = useState(0);


  function setTime(what) {
    let time = new Date().getTime() - 1701380000000;
    console.log("   -> "+what+ " " + time);
    return time;
  }

  const playAudio = () => {
    let path = require("./dash.mp3").default;
    const audio = new Audio(path);
    
    console.log("playing beep");

    audio.play();

//    const audioPromise = audio.play();
//    if (audioPromise !== undefined) {
//        audioPromise
//            .then(() => {
//                console.log("works");
//            })
//            .catch((err) => {
//                console.info(err);
//            });
//    }
  }

  function handleDown() {
    console.info("------------------"+convert("...")+convert("---")+convert("...")+"---------------------------");
  
    setDownMessage(downAction + " down");

    let dir = "Down";
    setDownTime(setTime(dir));

//  console.info("Mouse down:" + downTime);
  }  
  function handleUp() {
    setUpMessage(upAction + " up");
    
//   console.log("Mouse up: "+ upTime);
  
    playAudio();

    let up = setTime("Up");
    
    let holdTime = up - downTime;
    let char = convert(morseChar(holdTime));

    Message(morseMessage + char);

    console.log("Mouse up: "+ char + "  " + morseMessage) ;
  } 
  
  function morseChar(hold)
  {
     if(hold < 120){
        return '.';
     }   
     if(hold > 200){
        return '-';
     }   
     return '';
  }
   
  return (
    <div className="App">
      <label>
        Write your post:
        <br/><br/><br/>
        <textarea name="postContent" rows={20} cols={60}>{morseMessage}</textarea>
      </label> 
      <br /><br /><br /><br />
        <Text numberOfLines={5}  >{morseMessage}</Text>
      <br/><br/><br/>
      <button onMouseDown={() => handleDown()} onMouseUp={() => handleUp()}>Morse Key</button>
      <br />
    </div>
  );


}



export default App;
