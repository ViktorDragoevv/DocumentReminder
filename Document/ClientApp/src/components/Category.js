import React, { useState, useEffect } from "react";
import authService from "./api-authorization/AuthorizeService";
import CustomModal from "./CustomModal";
import CustomTable from "./CustomTable";
import { FileAddOutlined } from '@ant-design/icons';
import { useCookies } from "react-cookie";
import { Button } from 'antd';




function Category() {

    const [userData, setUserData] = useState({});

    const [dataCategory, setData] = useState();

    const getGitHubUserWithFetch = async () => { };

    const [category, setCategory] = useState();
    const [cookies, setCookie] = useCookies();
    const [object, setObject] = useState();

    useEffect(() => {

        async function fetchData() {
            const token = await authService.getAccessToken();
            const response = await fetch('https://localhost:7174/api/CategoryModels', {
                headers: !cookies.Authorization ? {} : { 'Authorization': `${cookies.Authorization}` },
                //headers: { 'Access-Control-Allow-Origin': '*' },
                //headers: { 'content-type': 'application/json; charset=utf-8' }
            });
            let data = await response.json();
            setData(data);
            console.log(data);
        };

        fetchData();
    }, []);

 

    /*const data = [
        {
            id: '1',
            categoryName: 'New York No. 1 Lake Park',
        },
        {
            id: '2',
            categoryName: 'London No. 1 Lake Park',
        },
        {
            id: '3',
            categoryName: 'Sidney No. 1 Lake Park',
        },
        {
            id: '4',
            categoryName: 'London No. 2 Lake Park',
        },
    ];*/

    const [selectedRowKeys, setSelectedRowKeys] = useState();

    const deleteCategory = () => {

        console.log(selected);

        console.log(selectedRowKeys);



        var jsonData = {
            "ID": selected.id
        }

        fetch('https://localhost:7174/api/CategoryModels/' + selected.id, {  // Enter your IP address here
            method: 'DELETE',
            mode: 'cors',
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': `${cookies.Authorization}`
            }),
            //body: JSON.stringify(jsonData) // body data type must match "Content-Type" header

        })



        setData(dataCategory.filter((item) => item.id !== selected.id));
    };

    const addCategorys = newCategory => {
        setData([...dataCategory, newCategory]);
        console.log(newCategory);
    }

    const updateCategorys = newCategory => {
        let updatedItem = dataCategory.find((element) => { return element.id === newCategory.ID });
        let changedCategories = dataCategory.map(a => a.id == newCategory.id ? newCategory : a);
        
        setData(changedCategories);
        
    }

    const [selected, setSelected] = useState();
    const [loading, setLoading] = useState(false);

    const getSelectedFromChild = selectedCategory => {
        setSelected(selectedCategory);
    };


  
    return (
        <div className="App">
            <div >
                <FileAddOutlined /> 
                <p>Cartegory List:</p>
            </div>

            <CustomModal status={1} update={addCategorys}></CustomModal>
            <CustomModal selectedRowKeys={selected} update={updateCategorys} status={2}></CustomModal>
            <Button type="primary" onClick={deleteCategory} disabled={!selected} loading={loading}>
                Delete 
            </Button>
            <CustomTable data={dataCategory} setSelected={getSelectedFromChild}></CustomTable>

        </div>
    );



}


export default Category;