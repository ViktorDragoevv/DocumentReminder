import React, { useState } from 'react';
import { Button, Modal } from 'antd';
import { Input } from 'antd';

function CustomModal(props) { 
    


    const [object, setObject] = useState();
    

    const [open, setOpen] = useState(false);
    const [confirmLoading, setConfirmLoading] = useState(false);
    const [modalText, setModalText] = useState('Content of the modal');


    const showModal = () => {
        setOpen(true);
        setObject(props.selectedRowKeys);
        //console.log(object);
    };

    const handleOk = () => {
        setModalText('The modal will be closed after two seconds');
        setConfirmLoading(true);
        //refreshPage();
        
        if (props.status == 1) {
            addcategory(object.categoryName);
        }
        else if (props.status == 2) {
            console.log(object);
            editCategory(object.categoryName);
        }
            
       
        setTimeout(() => {
            setOpen(false);
            setConfirmLoading(false);
        }, 2000);
    };

    const handleCancel = () => {
        console.log('Clicked cancel button');
        setOpen(false);
    };

    
    const addcategory = async (input) => {

        var jsonData = {
            "CategoryName": input
        }

        var response = await fetch('https://localhost:7174/api/CategoryModels', {  // Enter your IP address here

            method: 'POST',
            mode: 'cors',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(jsonData) // body data type must match "Content-Type" header

        });
        var newCategory = await response.json();
        //console.log(newCategory);
        props.update(newCategory);

    }
    const editCategory = async (input) => {

        console.log(object.id);
        var jsonDataa = {
            "id": parseInt(object.id),
            "categoryName": input.toString()
        }

        fetch('https://localhost:7174/api/CategoryModels/' + object.id, {  // Enter your IP address here

            method: 'PUT',
            mode: 'cors',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(jsonDataa) // body data type must match "Content-Type" header

        });
        props.update(jsonDataa);

    }

    function refreshPage() {
        window.location.reload(false);
    }

    const handleChange = e => {
        //setInput(e.target.value);
        setObject({ ...object, categoryName: e.target.value, });
        //console.log(e.target.value);
    }

    return (
        <>
            <Button type="primary" onClick={showModal}>
                {props.status == 1 ? "Add Category" : "Edit"}
            </Button>
            <Modal
                title={props.status == 1 ? "Add Category" : "Edit"}
                open={open}
                onOk={handleOk}
                confirmLoading={confirmLoading}
                onCancel={handleCancel}

            >

                <Input type="text" value={object?.categoryName} onChange={handleChange}></Input>

            </Modal>
        </>
    );
}


export default CustomModal;