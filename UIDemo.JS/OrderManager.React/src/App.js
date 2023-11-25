import './App.css';

import {AgGridReact} from 'ag-grid-react';
import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-alpine.css';

import {useState, useRef, useEffect, useMemo, useCallback} from 'react';

import Connector from './hub/signalr-connect.ts'

function App() {

  const { newMessage, events } = Connector();
  const [message, setMessage] = useState("initial value");
  useEffect(() => {
    events((_, message) => setMessage(message));
  });
  const [rowData, setRowData] = useState();

  const gridRef = useRef();

  const [columnDefs, setColumnDefs] = useState([
    {field: 'firstName'},
    {field: 'middleName'},
    {field: 'lastName'}
  ]);

  const defaultColDef = useMemo( ()=> (
    {
      sortable: true, 
      filter: true
    }
  ));

  const cellClickedListener = useCallback( event => {
    console.log('cellClicked', event);
  }, []);

  useEffect(() => {
    fetch('https://localhost:5001/api/employees')
    .then(result => result.json())
    .then(rowData => setRowData(rowData))
  }, []);

  const buttonListener = useCallback( e => {
    gridRef.current.api.deselectAll();
  }, []);

  return (
    <div>
      <span>message from signalR: <span style={{ color: "green" }}>{message}</span></span>
      <br />
      <button onClick={() => newMessage((new Date()).toISOString())}>send date </button>
      <br />
      <div className="ag-theme-alpine" style={{width: 500, height: 500}}>
        <AgGridReact 
            ref={gridRef}
            rowData={rowData} columnDefs={columnDefs}
            animateRows={true} rowSelection='multiple'
            onCellClicked={cellClickedListener}
            defaultColDef={defaultColDef}/>
      </div>
    </div>
  );
}

{/* <button onClick={buttonListener}>Push Me</button>
 */}
export default App;
