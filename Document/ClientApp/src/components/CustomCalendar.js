import React, { useState, useEffect } from 'react';
import { BadgeProps } from 'antd';
import { Badge, Calendar } from 'antd';
import dayjs from 'dayjs';
import { useCookies } from "react-cookie";




function CustomCalendar() {



    const [dataDocuments, setDataDocuments] = useState();
    const [cookies, setCookie] = useCookies();

    useEffect(() => {

        async function fetchData() {

            //const token = await authService.getAccessToken();
            const response = await fetch('https://localhost:7174/api/DocumentModels', {
                headers: !cookies.Authorization ? {} : { 'Authorization': `${cookies.Authorization}` },
                //headers: !token ? {} : { 'Authorization': `Bearer ${token}` },
                //headers: { 'Access-Control-Allow-Origin': '*' },
                //headers: { 'content-type': 'application/json; charset=utf-8' }
            });
            let data = await response.json();
            setDataDocuments(data);

            console.log(data);
        };

        fetchData();
    }, []);
    //console.log(date)
    const getListData = (value) => {
    let listData;
        for (let i = 0; i < dataDocuments?.length; i++) {
        
            if (convert(value) == convert(dataDocuments[i].expirationDate)) {
        listData = [
            { type: 'warning', content: `${dataDocuments[i].viewContact.firstName} You have ${dataDocuments[i].name} expiring today`},
            ];
        }
    }
    return listData || [];
};
    

const getMonthData = (value) => {
    if (value.month() === 8) {
        return 1394;
    }
    
};
    function convert(str) {
        var date = new Date(str),
            mnth = ("0" + (date.getMonth() + 1)).slice(-2),
            day = ("0" + date.getDate()).slice(-2);
        return [date.getFullYear(), mnth, day].join("-");
    }

    const monthCellRender = (value) => {
        const num = getMonthData(value);
        return num ? (
            <div className="notes-month">
                <section>{num}</section>
                <span>Backlog number</span>
            </div>
        ) : null;
    };

    const dateCellRender = (value) => {
        const listData = getListData(value);
        return (
            <ul className="events">
                {listData.map((item) => (
                    <li key={item.content}>
                        <Badge status={item.type} text={item.content} />
                    </li>
                ))}
            </ul>
        );
        
    };

    return <Calendar dateCellRender={dateCellRender} monthCellRender={monthCellRender}  />;
};

export default CustomCalendar;