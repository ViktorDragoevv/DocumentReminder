import React, { useRef, useState } from 'react';
import { SearchOutlined } from '@ant-design/icons';
import { InputRef } from 'antd';
import { Button, Input, Space, Table } from 'antd';
import { ColumnsType, ColumnType } from 'antd/es/table';
import { FilterConfirmProps } from 'antd/es/table/interface';
import Highlighter from 'react-highlight-words';

function CustomTable(props) {
    let data = props.data;
    //let col = props.col;
    //console.log(data);
    const [searchText, setSearchText] = useState('');
    const [searchedColumn, setSearchedColumn] = useState('');
    const searchInput = useRef(null);

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

    const [selectedRowKeys, setSelectedRowKeys] = useState([]);


    const deleteCategory = () => {

        
        console.log(selectedRowKeys);

        

        var jsonData = {
            "ID": selectedRowKeys
        }

        fetch('https://localhost:7174/api/CategoryModels/' + selectedRowKeys, {  // Enter your IP address here

            method: 'DELETE',
            mode: 'cors',
            headers: { 'Content-Type': 'application/json' },
            //body: JSON.stringify(jsonData) // body data type must match "Content-Type" header

        })
    };



    function findArrayElementByTitle(array, ID) {
        return array.find((element) => {
            return element.id == ID;
        })
    }



    const onSelectChange = (newSelectedRowKeys) => {
        console.log('selectedRowKeys changed: ', newSelectedRowKeys);
        setSelectedRowKeys(newSelectedRowKeys);
        //console.log(newSelectedRowKeys);

        let obj = findArrayElementByTitle(data, newSelectedRowKeys);
        props.setSelected(obj);
        //console.log(object);
    };

    const rowSelection = {
        selectedRowKeys,
        onChange: onSelectChange,
        type: 'radio',
    };
    const hasSelected = selectedRowKeys.length > 0;

    


    const columns = [
     
        {
            title: 'categoryName',
            dataIndex: 'categoryName',
            key: 'categoryName',
            ...getColumnSearchProps('categoryName'),
            sorter: (a, b) => a.categoryName.length - b.categoryName.length,
            sortDirections: ['descend', 'ascend'],
        },
    ];

    var newData = data?.map((category, index) => {
        return {
            key: category.id,
            id: category.id,
            categoryName: category.categoryName,
        };
    
    });

    //console.log(newData);


    return (
        <div>
            <div style={{ marginBottom: 16 }}>
                <span style={{ marginLeft: 8 }}>
                    {hasSelected ? `Selected ${selectedRowKeys.length} items` : ''}
                </span>
            </div>
            <Table rowSelection={rowSelection} columns={columns} dataSource={newData} />
        </div>
    );
}

export default CustomTable;