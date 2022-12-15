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
import { useCookies } from "react-cookie";
import CustomModalCompany from "./CustomModalCompany";

function Company() {

    const [dataCompany, setDataCompany] = useState();
    const [searchText, setSearchText] = useState('');
    const [searchedColumn, setSearchedColumn] = useState('');
    const [selectedRowKeys, setSelectedRowKeys] = useState([]);
    const searchInput = useRef(null);
    const [selectedCompanyObject, setSelectedCompanyObject] = useState();
    const [cookies, setCookie] = useCookies();

    useEffect(() => {

        async function fetchData() {

            //const token = await authService.getAccessToken();
            const response = await fetch('https://localhost:7174/api/CompanyModels', {
                headers: !cookies.Authorization ? {} : { 'Authorization': `${cookies.Authorization}` },
                //headers: !token ? {} : { 'Authorization': `Bearer ${token}` },
                //headers: { 'Access-Control-Allow-Origin': '*' },
                //headers: { 'content-type': 'application/json; charset=utf-8' }
            });
            let data = await response.json();
            setDataCompany(AddKeyProp(data));

            console.log(data);
        };

        fetchData();
    }, []);

    function AddKeyProp(arr) {
        const mappedArray = arr.map((company, index) => ({

            key: company.id,
            id: company.id,
            name: company.name,
            tradeName: company.tradeName,
            phone: company.phone,
            phoneNumber: company.phoneNumber,
            type: company.type,
            locationID: company?.viewLocation?.name,
            location: company?.viewLocation,
            viewLocation: company?.viewLocation,

        }))
        return mappedArray;
    };

    const deleteContacts = () => {


        console.log(selectedCompanyObject.id);



        var jsonData = {
            "ID": selectedCompanyObject.id
        }

        fetch('https://localhost:7174/api/CompanyModels/' + selectedCompanyObject.id, {
            method: 'DELETE',
            mode: 'cors',
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': `${cookies.Authorization}`
            }),

        })

        setDataCompany(dataCompany.filter((item) => item.id !== selectedCompanyObject.id));
    };

    const addContactFromChild = newContact => {
        setDataCompany(AddKeyProp([...dataCompany, newContact]));
        console.log(newContact);
    }

    const updateCategoryFromChild = newContact => {
        //let updatedItem = dataContacts.find((element) => { return element.id === newCategory.ID });
        let changedContact = dataCompany.map(a => a.id == newContact.id ? newContact : a);
        setDataCompany(AddKeyProp(changedContact));

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
            title: 'tradeName',
            dataIndex: 'tradeName',
            key: 'tradeName',
            ...getColumnSearchProps('tradeName'),
            sorter: (a, b) => a.tradeName.length - b.tradeName.length,
            sortDirections: ['descend', 'ascend'],
        },
        {
            title: 'phone',
            dataIndex: 'phone',
            key: 'phone',
            ...getColumnSearchProps('phone'),
            sorter: (a, b) => a.phone.length - b.phone.length,
            sortDirections: ['descend', 'ascend'],
        },
        {
            title: 'type',
            dataIndex: 'type',
            key: 'type',
            ...getColumnSearchProps('type'),
            sorter: (a, b) => a.type.length - b.type.length,
            sortDirections: ['descend', 'ascend'],
        },
        {
            title: 'locationID',
            dataIndex: 'locationID',
            key: 'locationID',
            ...getColumnSearchProps('locationID'),
            sorter: (a, b) => a.locationID.length - b.locationID.length,
            sortDirections: ['descend', 'ascend'],
        },
    ];

    const onSelectChange = (newSelectedRowKeys) => {
        setSelectedRowKeys(newSelectedRowKeys);

        let selectedCompanyObject = findArrayElementById(dataCompany, newSelectedRowKeys);
        setSelectedCompanyObject(selectedCompanyObject);
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
            <Button type="primary" onClick={deleteContacts} disabled={!selectedCompanyObject}>
                Delete
            </Button>
            <CustomModalCompany status={1} update={addContactFromChild}></CustomModalCompany>
            <CustomModalCompany status={2} update={updateCategoryFromChild} selectedRowKeys={selectedCompanyObject}></CustomModalCompany>
            <div style={{ marginBottom: 16 }}>
                <span style={{ marginLeft: 8 }}>
                    {hasSelected ? `Selected ${selectedRowKeys.length} items` : ''}
                </span>
            </div>
            <Table rowSelection={rowSelection} columns={columns} dataSource={dataCompany} />

        </div>
    );

}
export default Company;