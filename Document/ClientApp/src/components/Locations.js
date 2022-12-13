import React, { useRef, useState, useEffect } from "react";
import { SearchOutlined } from '@ant-design/icons';
import { InputRef } from 'antd';
import { Button, Input, Space, Table } from 'antd';
import { ColumnsType, ColumnType } from 'antd/es/table';
import { FilterConfirmProps } from 'antd/es/table/interface';
import Highlighter from 'react-highlight-words';
import authService from "./api-authorization/AuthorizeService";
import CustomModal from "./CustomModal";
import CustomTable from "./CustomTable";
import CustomModalContacts from "./CustomModalContacts";
import CustomModalLocations from "./CustomModalLocations";
import { useCookies } from "react-cookie";

function Locations() {



    const [dataLocations, setDataLocations] = useState();
    const searchInput = useRef(null);
    const [searchText, setSearchText] = useState('');
    const [searchedColumn, setSearchedColumn] = useState('');
    const [selectedRowKeys, setSelectedRowKeys] = useState([]);
    const [selectedLocationObject, setSelectedLocationObject] = useState();
    const [cookies, setCookie] = useCookies();


    useEffect(() => {
        console.log(cookies.Authorization);
        async function fetchData() {
            
            //const token = await authService.getAccessToken();
            const response = await fetch('https://localhost:7174/api/LocationModels', {
                headers: !cookies.Authorization ? {} : { 'Authorization': `${cookies.Authorization}` },
                //headers: { 'Access-Control-Allow-Origin': '*' },
                //headers: { 'content-type': 'application/json; charset=utf-8' }
            });
            let data = await response.json();
            setDataLocations(AddKeyProp(data));

        };

        fetchData();
    }, []);

    function AddKeyProp(arr) {
        const mappedArray = arr.map((location, index) => ({

            key: location.id,
            id: location.id,
            name: location.name,
            code: location.code,
            address: location.address,
            city: location.city,

        }))
        return mappedArray;
    };


    const getColumnSearchProps = (dataIndex) => ({
        filterDropdown: ({ setSelectedKeys, selectedKeys, confirm, clearFilters, close }) => (
            <div style={{ padding: 8 }} onKeyDown={(e) => e.stopPropagation()}>
                <Input
                    ref={searchInput}
                    placeholder={`Search ${dataIndex}`}
                    value={selectedKeys[0]}
                    onChange={(e) => setSelectedKeys(e.target.value ? [e.target.value] : [])}
                    onPressEnter={() => handleSearch(selectedKeys, confirm, dataIndex)}
                    style={{ marginBottom: 8, display: 'block' }}
                />
                <Space>
                    <Button
                        type="primary"
                        onClick={() => handleSearch(selectedKeys, confirm, dataIndex)}
                        icon={<SearchOutlined />}
                        size="small"
                        style={{ width: 90 }}
                    >
                        Search
                    </Button>
                    <Button
                        onClick={() => clearFilters && handleReset(clearFilters)}
                        size="small"
                        style={{ width: 90 }}
                    >
                        Reset
                    </Button>
                    <Button
                        type="link"
                        size="small"
                        onClick={() => {
                            confirm({ closeDropdown: false });
                            setSearchText((selectedKeys)[0]);
                            setSearchedColumn(dataIndex);
                        }}
                    >
                        Filter
                    </Button>
                    <Button
                        type="link"
                        size="small"
                        onClick={() => {
                            close();
                        }}
                    >
                        close
                    </Button>
                </Space>
            </div>
        ),
        filterIcon: (filtered) => (
            <SearchOutlined style={{ color: filtered ? '#1890ff' : undefined }} />
        ),
        onFilter: (value, record) =>
            record[dataIndex]
                .toString()
                .toLowerCase()
                .includes((value).toLowerCase()),
        onFilterDropdownOpenChange: (visible) => {
            if (visible) {
                setTimeout(() => searchInput.current?.select(), 100);
            }
        },
        render: (text) =>
            searchedColumn === dataIndex ? (
                <Highlighter
                    highlightStyle={{ backgroundColor: '#ffc069', padding: 0 }}
                    searchWords={[searchText]}
                    autoEscape
                    textToHighlight={text ? text.toString() : ''}
                />
            ) : (
                text
            ),
    });

    const handleSearch = (
        selectedKeys,
        confirm,
        dataIndex,
    ) => {
        confirm();
        setSearchText(selectedKeys[0]);
        setSearchedColumn(dataIndex);
    };

    const handleReset = (clearFilters) => {
        clearFilters();
        setSearchText('');
    };

    const onSelectChange = (newSelectedRowKeys) => {
        console.log('selectedRowKeys changed: ', newSelectedRowKeys);
        setSelectedRowKeys(newSelectedRowKeys);
        //console.log(newSelectedRowKeys);

        let selectedLocationObject = findArrayElementById(dataLocations, newSelectedRowKeys);
        setSelectedLocationObject(selectedLocationObject);
        console.log(dataLocations);
    };

    const rowSelection = {
        selectedRowKeys,
        onChange: onSelectChange,
        type: 'radio',
    };

    function findArrayElementById(array, ID) {
        return array.find((element) => {
            return element.id == ID;
        })
    }

    

    const hasSelected = selectedRowKeys.length > 0;

    const columns = [
        {
            title: 'id',
            dataIndex: 'id',
            key: 'id',
            width: '30%',
            ...getColumnSearchProps('id'),
        },
        {
            title: 'name',
            dataIndex: 'name',
            key: 'name',
            ...getColumnSearchProps('name'),
            sorter: (a, b) => a.name.length - b.name.length,
            sortDirections: ['descend', 'ascend'],
        },
        {
            title: 'code',
            dataIndex: 'code',
            key: 'code',
            ...getColumnSearchProps('code'),
            sorter: (a, b) => a.code.length - b.code.length,
            sortDirections: ['descend', 'ascend'],
        },
        {
            title: 'address',
            dataIndex: 'address',
            key: 'address',
            ...getColumnSearchProps('address'),
            sorter: (a, b) => a.address.length - b.address.length,
            sortDirections: ['descend', 'ascend'],
        },
        {
            title: 'city',
            dataIndex: 'city',
            key: 'city',
            ...getColumnSearchProps('city'),
            sorter: (a, b) => a.city.length - b.city.length,
            sortDirections: ['descend', 'ascend'],
        },
    ];

    const addContactFromChild = newContact => {
        /*setData(AddKeyProp([...dataContacts, newContact]));
        console.log(newContact);*/
    }

    const updateCategoryFromChild = newContact => {
        //let updatedItem = dataContacts.find((element) => { return element.id === newCategory.ID });
        /*let changedContact = dataContacts.map(a => a.id == newContact.id ? newContact : a);
        console.log(newContact);
        setData(AddKeyProp(changedContact));
        console.log(newContact);
        console.log(changedContact);*/

    }



    const deleteLocations = () => {


        console.log(selectedRowKeys);



        var jsonData = {
            "ID": selectedLocationObject.id
        }

        fetch('https://localhost:7174/api/LocationModels/' + selectedLocationObject.id, {  // Enter your IP address here
            method: 'DELETE',
            mode: 'cors',
            headers: { 'Content-Type': 'application/json' },
            //body: JSON.stringify(jsonData) // body data type must match "Content-Type" header

        })
    };


    return (

        <div>
            <Button type="primary" onClick={deleteLocations} disabled={!selectedLocationObject}>
                Delete
            </Button>
            <CustomModalLocations status={1} update={addContactFromChild}></CustomModalLocations>
            <CustomModalLocations status={2} update={updateCategoryFromChild} selectedRowKeys={selectedLocationObject}></CustomModalLocations>
            <Table rowSelection={rowSelection} columns={columns} dataSource={dataLocations} />

        </div>
    );

}
export default Locations;