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

function Contacts() {

    const [dataContacts, setData] = useState();
    const [searchText, setSearchText] = useState('');
    const [searchedColumn, setSearchedColumn] = useState('');
    const [selectedRowKeys, setSelectedRowKeys] = useState([]);
    const searchInput = useRef(null);
    const [selectedContactObject, setSelectedContactObject] = useState();

    useEffect(() => {

        async function fetchData() {

            const token = await authService.getAccessToken();
            const response = await fetch('https://localhost:7174/api/ContactsModels', {
                headers: !token ? {} : { 'Authorization': `Bearer ${token}` },
                //headers: { 'Access-Control-Allow-Origin': '*' },
                //headers: { 'content-type': 'application/json; charset=utf-8' }
            });
            let data = await response.json();
            setData(AddKeyProp(data));
            
            console.log(data);
        };

        fetchData();
    }, []);

    function AddKeyProp(arr)
    {
        const mappedArray = arr.map((contacts, index) => ({
            
            key: contacts.id,
            id: contacts.id,
            firstName: contacts.firstName,
            lastName: contacts.lastName,
            email: contacts.email,
            phoneNumber: contacts.phoneNumber,
            locationID: contacts.locationID,
            
        }))
        return mappedArray;
    };

    const deleteContacts = () => {


        console.log(selectedRowKeys);



        var jsonData = {
            "ID": selectedContactObject.id
        }
        
        fetch('https://localhost:7174/api/ContactsModels/' + selectedContactObject.id, {  // Enter your IP address here
            method: 'DELETE',
            mode: 'cors',
            headers: { 'Content-Type': 'application/json' },
            //body: JSON.stringify(jsonData) // body data type must match "Content-Type" header

        })
    };

    const addContactFromChild = newContact => {
        setData(AddKeyProp([...dataContacts, newContact]));
        console.log(newContact);
    }

    const updateCategoryFromChild = newContact => {
        //let updatedItem = dataContacts.find((element) => { return element.id === newCategory.ID });
        let changedContact = dataContacts.map(a => a.id == newContact.id ? newContact : a);
        console.log(newContact);
        setData(AddKeyProp(changedContact));
        console.log(newContact);
        console.log(changedContact);

    }


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
            title: 'firstName',
            dataIndex: 'firstName',
            key: 'firstName',
            ...getColumnSearchProps('firstName'),
            sorter: (a, b) => a.firstName.length - b.firstName.length,
            sortDirections: ['descend', 'ascend'],
        },
        {
            title: 'lastName',
            dataIndex: 'lastName',
            key: 'lastName',
            ...getColumnSearchProps('lastName'),
            sorter: (a, b) => a.lastName.length - b.lastName.length,
            sortDirections: ['descend', 'ascend'],
        },
        {
            title: 'email',
            dataIndex: 'email',
            key: 'email',
            ...getColumnSearchProps('email'),
            sorter: (a, b) => a.email.length - b.email.length,
            sortDirections: ['descend', 'ascend'],
        },
        {
            title: 'phoneNumber',
            dataIndex: 'phoneNumber',
            key: 'phoneNumber',
            ...getColumnSearchProps('phoneNumber'),
            sorter: (a, b) => a.phoneNumber.length - b.phoneNumber.length,
            sortDirections: ['descend', 'ascend'],
        },
    ];

    const onSelectChange = (newSelectedRowKeys) => {
        console.log('selectedRowKeys changed: ', newSelectedRowKeys);
        setSelectedRowKeys(newSelectedRowKeys);
        //console.log(newSelectedRowKeys);

        let selectedContactObject = findArrayElementById(dataContacts, newSelectedRowKeys);
        setSelectedContactObject(selectedContactObject);
        console.log(dataContacts);
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



    return (

        <div>
            <Button type="primary" onClick={deleteContacts} disabled={!selectedContactObject}>
                Delete
            </Button>
            <CustomModalContacts status={1} update={addContactFromChild}></CustomModalContacts>
            <CustomModalContacts status={2} update={updateCategoryFromChild} selectedRowKeys={selectedContactObject}></CustomModalContacts>
            <div style={{ marginBottom: 16 }}>
                <span style={{ marginLeft: 8 }}>
                    {hasSelected ? `Selected ${selectedRowKeys.length} items` : ''}
                </span>
            </div>
            <Table rowSelection={rowSelection} columns={columns} dataSource={dataContacts} />
            
        </div>
    );

}
export default Contacts;