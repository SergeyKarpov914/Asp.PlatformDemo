import './App.css';

import {AgGridReact} from 'ag-grid-react';
import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-alpine.css';

import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

import {useState, useRef, useEffect, useMemo, useCallback} from 'react';

import {styles}  from './data/style.js';
import {columns} from './data/gridData.js';
import {address} from './data/gridData.js';

function App() {

  const [style, setStyle] = useState({ height: '100%', width: '100%', });
  const [rowData, setRowData] = useState();

  const gridRef = useRef();

  const [columnDefs, setColumnDefs] = useState(columns);

  const defaultColDef = useMemo( ()=> (
    {
      sortable: true, 
      filter: true
    }
  ));

  const rowStyle = { height: 35 };

  const getRowStyle = () => { return { height: 35  }; };

  const [selectedValue, setSelectedValue] = useState("option1");

  const handleRadioChange = (value) => {setSelectedValue(value)};

  const getData = period => {
    fetch(period)
    .then(result => result.json())
    .then(rowData => setRowData(rowData))
  };

  useEffect(() => getData( address + '1M'), []);

  const [startDate, setStartDate] = useState(new Date());

  return (
    <div>
      <div style={styles.container}>
        <div style={ styles.radioGroup } >
           <div style={ styles.radioButton } >
              <input type="radio" id="option1" value="option1"
                  checked={ selectedValue === "option1" }
                  onChange={() => {handleRadioChange("option1"); getData(address + '1M'); }}
              />
            <label htmlFor="option1" style={ styles.radioLabel } >1M</label>
           </div>
           <div style={ styles.radioButton } >
               <input type="radio" id="option2" value="option2"
                  checked={ selectedValue === "option2" }
                  onChange={() => {handleRadioChange("option2"); getData(address + '3M'); }}
                />
                <label htm1For="option2" style={ styles.radioLabel } >3M</label>
           </div>
           <div style={ styles.radioButton } >
                <input type="radio" id="option3" value="cption3"
                    checked={ selectedValue === "option3"}
                    onChange={() => {handleRadioChange("option3"); getData(address + '6M'); }}
                />
            <label htm1For="option3" style={styles.radioLabel } >6M </label>
            </div>
            <div  style={ styles.radioButton } >
                <input type="radio" id="option4" value="option4"
                    checked={ selectedValue === "option4"}
                    onChange={() => {handleRadioChange("option3"); getData(address + '1Y'); }}
                />
            <label htmlFor="option3" style={styles.radioLabel } >1Y</label>
          </div>
          <div  style={ styles.datePicker } >
                <DatePicker  selected={startDate} onChange={(date) => setStartDate(date)} /> 
          </div>
        </div>
      </div>
  
      <div className="ag-theme-alpine" style={{ margin: 20, width: '100', height: 1100}}>
          <AgGridReact 
              ref={gridRef}
              rowData={rowData} columnDefs={columnDefs}
              animateRows={true} rowSelection='multiple'
              defaultColDef={defaultColDef}
              rowStyle={rowStyle}
              getRowStyle={getRowStyle}
              />
      </div>
    </div>
  );
}
export default App;
