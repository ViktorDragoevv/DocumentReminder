import React, { useState } from 'react';
import { Table } from 'antd';
import { ColumnsType } from 'antd/es/table';
import { TableRowSelection } from 'antd/es/table/interface';
import { Button, Modal, Input } from 'antd';



function CustomModalNotifications() {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [selectedRowKeys, setSelectedRowKeys] = useState([]);

    const showModal = () => {
        setIsModalOpen(true);
    };

    const handleOk = () => {
        setIsModalOpen(false);
    };

    const handleCancel = () => {
        setIsModalOpen(false);
    };

    const onSelectChange = (newSelectedRowKeys) => {
        setSelectedRowKeys(newSelectedRowKeys);

        //let selectedContactObject = findArrayElementById(dataDocuments, newSelectedRowKeys);
        //setSelectedDocumentObject(selectedContactObject);
    };

    const rowSelection = {
        selectedRowKeys,
        onChange: onSelectChange,
        type: 'radio',
    };

    const ColumnsType = [
        {
            title: 'Name',
            dataIndex: 'name',
        },
        {
            title: 'Age',
            dataIndex: 'age',
        },
        {
            title: 'Address',
            dataIndex: 'address',
        },
    ];

    return (
        <>
           
             
                    <Table rowSelection={rowSelection} columns={ColumnsType} showHeader={false} />
                 
                
                    <Input placeholder="Type your code" type="number" />
                
                
            
        </>
    );
}
export default CustomModalNotifications;