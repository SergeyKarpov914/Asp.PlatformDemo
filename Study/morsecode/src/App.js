import React from 'react'
import { useState } from 'react';
import {Text} from 'react-native';
import { convert } from './code2text.js';
import './App.css';

function App() {

  const [morseMessage, Message] = useState("                 ->       ");

  const [downTime, setDownTime] = useState(0);
  const [endTime, setEndTime] = useState(0);

  const [letter, morseLetter] = useState("");
  const [count, increment] = useState(0);

  /////////////////////////////////////////////////////////////////////////////////////

  function handleDown() {
//  console.info("---------------------------------------------");

    setDownTime(setTime("Down"));

//  console.info("Mouse down:" + downTime);
  }  

  function handleUp() {
    //   console.log("Mouse up: "+ upTime);
    playAudio();

    let up = setTime("Up");
    
    let holdTime = up - downTime;
    let gapTime = up - endTime;

    setEndTime(up);

//  console.log("Hold: "+ holdTime + "   " + "Gap: " + gapTime) ;

    let element = morseElement(holdTime);

    if(gapTime > 500 || count >= 4){                // letter is complete    
      let char = convert(letter);
      Message(morseMessage + " " + char);

      console.log(up + " " + endTime + "  " + holdTime + " " + gapTime + "  ("+ letter + " > " + char + ")   " + morseMessage);

      morseLetter(element);               // start letter with first dot or dash   
      increment(1);      
    }            
    if(gapTime < 300){                // add next dot or dash to letter
      morseLetter(letter + element); 
      increment(count + 1);      

      console.log(count + "  " + gapTime);
    }   
  } 
  
  /////////////////////////////////////////////////////////////////////////////////////

  function morseElement(hold){
     if(hold < 120){ return '.'; }   
     if(hold > 200){ return '-'; }   
     return '';
  }

  function setTime(what) {
    let time = new Date().getTime() - 1701380000000;
//  console.log("   -> "+what+ " " + time);
    return time;
  }

  const playAudio = (segment) => {
    let path = "";
    
    if(segment === '.'){ path = require("./dot.mp3").default; }
    if(segment === '-'){ path = require("./dash.mp3").default; }

  //  const audio = new Audio(path);
  //  console.log("playing " + path);

  //  audio.play();
  }

  /////////////////////////////////////////////////////////////////////////////////////////
  
  return (
    <div className="App">
      <label>
        Write your post:
        <br/><br/><br/>
      </label> 
      <br /><br /><br /><br />
        <Text numberOfLines={5}  >{morseMessage}</Text>
      <br/><br/><br/>
      <button onMouseDown={() => handleDown()} onMouseUp={() => handleUp()}>Morse Key</button>
      <br/><br/><br/>
      <button onClick={() => Message("                 ->")}>Reset</button>
      <br />
    </div>
  );
}



export default App;
