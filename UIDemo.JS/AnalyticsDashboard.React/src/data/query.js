import '../App.css';

import { format } from "date-fns";

export function query(start, end, acct, sym){
    return '(' + dateFormat(start, end) + itemFormat(acct) 
                                        + itemFormat(sym) 
                                        + itemFormat('0') 
                                        + lastFormat('PRODSS');
}

const dateFormat = (start, end) => { {return format(start, "yyyy.MM.dd") + ' ' + 
                                             format(end, "yyyy.MM.dd") + ';';} };

const itemFormat = (item) => { {return '`$"' + item + '";' ;} };
const lastFormat = (item) => { {return '`$"' + item + '")' ;} };
