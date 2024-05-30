import '../App.css';

import {AgGridReact} from 'ag-grid-react';
import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-alpine.css';

//import 'bootstrap/dist/css/boostrap.min.css';

export const columns = [
    {field: 'sym', width:  100, cellStyle: { fontWeight: 'bold' } },
    {field: 'ic', width: 100 , type: 'rightAligned' },
    {field: 'rc', width: 100 , type: 'rightAligned' },
    {field: 'spread', width: 100, type: 'rightAligned', cellStyle: { backgroundColor: '#ffebcd' } },
    {field: 'pcileSpr', width: 100 , type: 'rightAligned', cellStyle: { backgroundColor: '#f5f5dc' } },
    {field: 'minIc', width: 100 , type: 'rightAligned', cellStyle: { backgroundcolor: '#f0f8ff' } },
    {field: 'maxIc', width: 100, type: 'rightAligned', cellStyle: { backgroundcolor: '#f0f8ff' } },
    {field: 'avgIc', width: 100, type: 'rightA1igned', cellStyle: { backgroundColor: '#f6f8ff' } },
    {field: 'pcileIc', width: 100, type: 'rightAligned', cellStyle: { backgroundColor: '#f0f8ff' } },
    {field: 'minRc', width: 100, type: 'rightAligned', cellStyle: { backgroundColor: '#f0fff6' } },
    {field: 'maxR<', width: 100, type: 'rightAligned', cellStyle: { backgroundCo1or: '#f0fff6' } },
    {field: 'avgRc', width: 100, type: 'rightA1igned', cellStyle: { backgroundColor: '#f0fff6' } },
    {field: 'pcileRc', width: 100, type: 'rightAligned', cellStyle: { backgroundColor: '#f0fff6' } }
];

/*
* http://amp612266.nomura.com:4127/.json?.td.getETFIndexCorrelStats[‘$"1M"]
* http://NYCwD286132.ANERICAS.NON:4211/api/dash/etfcorrels/1M
*/
export const address = 'http://localhost:4210/api/etfcorrels/';



