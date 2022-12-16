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
import CustomModalDocument from "./CustomModalDocument";
import { useCookies } from "react-cookie";



function Documents() {


    const [dataDocuments, setDataDocuments] = useState();
    const [searchText, setSearchText] = useState('');
    const [searchedColumn, setSearchedColumn] = useState('');
    const [selectedRowKeys, setSelectedRowKeys] = useState([]);
    const searchInput = useRef(null);
    const [selectedDocumentObject, setSelectedDocumentObject] = useState();
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
            setDataDocuments(AddKeyProp(data));

            console.log(data);
        };

        fetchData();
    }, []);

    function AddKeyProp(arr) {
        const mappedArray = arr.map((document, index) => ({

            key: document.id,
            id: document.id,
            name: document.name,
            status: document.status,
            comments: document.comments,
            viewCategory: document?.viewCategory,
            categoryName: document?.viewCategory.categoryName,
            viewCompany: document?.viewCompany,
            companyName : document?.viewCompany.name,
            viewContact: document?.viewContact,
            locationName: document?.viewLocation.name,
            viewLocation: document?.viewLocation,
            contactName: document?.viewContact.firstName,
            expirationDate: document?.expirationDate,

        }))
        return mappedArray;
    };

    const deleteContacts = () => {


        console.log(selectedDocumentObject.id);



        var jsonData = {
            "ID": selectedDocumentObject.id
        }

        fetch('https://localhost:7174/api/DocumentsModel/' + selectedDocumentObject.id, {
            method: 'DELETE',
            mode: 'cors',
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': `${cookies.Authorization}`
            }),

        })

        setDataDocuments(dataDocuments.filter((item) => item.id !== selectedDocumentObject.id));
    };

    const addDocumentFromChild = newContact => {
        setDataDocuments(AddKeyProp([...dataDocuments, newContact]));
        console.log(newContact);
    }

    const updateDocumentyFromChild = newContact => {
        let changedContact = dataDocuments.map(a => a.id == newContact.id ? newContact : a);
        setDataDocuments(AddKeyProp(changedContact));

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
            title: 'name',
            dataIndex: 'name',
            key: 'name',
            ...getColumnSearchProps('name'),
            sorter: (a, b) => a.name.length - b.name.length,
            sortDirections: ['descend', 'ascend'],
        },
        {
            title: 'categoryName',
            dataIndex: 'categoryName',
            key: 'categoryName',
            ...getColumnSearchProps('categoryName'),
            sorter: (a, b) => a.categoryName.length - b.categoryName.length,
            sortDirections: ['descend', 'ascend'],
        },
        {
            title: 'companyName',
            dataIndex: 'companyName',
            key: 'companyName',
            ...getColumnSearchProps('companyName'),
            sorter: (a, b) => a.companyName.length - b.companyName.length,
            sortDirections: ['descend', 'ascend'],
        },
        {
            title: 'status',
            dataIndex: 'status',
            key: 'status',
            ...getColumnSearchProps('status'),
            sorter: (a, b) => a.status.length - b.status.length,
            sortDirections: ['descend', 'ascend'],
        },
        {
            title: 'locationName',
            dataIndex: 'locationName',
            key: 'locationName',
            ...getColumnSearchProps('locationName'),
            sorter: (a, b) => a.locationName.length - b.locationName.length,
            sortDirections: ['descend', 'ascend'],
        },
        {
            title: 'contactName',
            dataIndex: 'contactName',
            key: 'contactName',
            ...getColumnSearchProps('contactName'),
            sorter: (a, b) => a.contactName.length - b.contactName.length,
            sortDirections: ['descend', 'ascend'],
        },
        {
            title: 'comments',
            dataIndex: 'comments',
            key: 'comments',
            ...getColumnSearchProps('comments'),
            sorter: (a, b) => a.comments.length - b.comments.length,
            sortDirections: ['descend', 'ascend'],
        },
        {
            title: 'expirationDate',
            dataIndex: 'expirationDate',
            key: 'expirationDate',
            ...getColumnSearchProps('expirationDate'),
            sorter: (a, b) => a.expirationDate.length - b.expirationDate.length,
            sortDirections: ['descend', 'ascend'],
        },


    ];

    const onSelectChange = (newSelectedRowKeys) => {
        setSelectedRowKeys(newSelectedRowKeys);

        let selectedContactObject = findArrayElementById(dataDocuments, newSelectedRowKeys);
        setSelectedDocumentObject(selectedContactObject);
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
            <Button type="primary" onClick={deleteContacts} disabled={!selectedDocumentObject}>
                Delete
            </Button>
            <CustomModalDocument status={1} update={addDocumentFromChild}></CustomModalDocument>
            <CustomModalDocument status={2} update={updateDocumentyFromChild} selectedRowKeys={selectedDocumentObject}></CustomModalDocument>
            <div style={{ marginBottom: 16 }}>
                <span style={{ marginLeft: 8 }}>
                    {hasSelected ? `Selected ${selectedRowKeys.length} items` : ''}
                </span>
            </div>
            <Table rowSelection={rowSelection} columns={columns} dataSource={dataDocuments} />

        </div>
    );


}
export default Documents;