import React, { useState } from 'react';
import { Button, Modal } from 'antd';
import { Input, Form } from 'antd';
import { useCookies } from "react-cookie";
function CustomModal(props) { 
    


    const [object, setObject] = useState();
    

    const [open, setOpen] = useState(false);
    const [confirmLoading, setConfirmLoading] = useState(false);
    const [modalText, setModalText] = useState('Content of the modal');
    const [cookies, setCookie] = useCookies();

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
            "CategoryName": input.category
        }
        console.log(input.lastName);

        var response = await fetch('https://localhost:7174/api/CategoryModels', {  // Enter your IP address here
            method: 'POST',
            mode: 'cors',
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': `${cookies.Authorization}`
            }),
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
            "categoryName": input.category.toString()
        }

        fetch('https://localhost:7174/api/CategoryModels/' + object.id, {  // Enter your IP address here
            method: 'PUT',
            mode: 'cors',
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': `${cookies.Authorization}`
            }),
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


    const [form] = Form.useForm();
    React.useEffect(() => {
        form.setFieldsValue({
            category: object?.categoryName,
        });
    }, [object]);


    const submit = (values) => {

        if (props.status == 1) {
            addcategory(values);
            setOpen(false);
        }
        else if (props.status == 2) {
            console.log(object);
            editCategory(values);
            setOpen(false);
        }
    };

    return (
        <>
            <Button type="primary" onClick={showModal}>
                {props.status == 1 ? "Add Category" : "Edit"}
            </Button>
           
                <Modal
                    title={props.status == 1 ? "Add Contact" : "Edit"}
                    open={open}
                    onOk={handleOk}
                    confirmLoading={confirmLoading}
                    onCancel={handleCancel}
                    footer={null}
                    forceRender

            >
                <Form
                    autoComplete="off"
                    labelCol={{ span: 10 }}
                    wrapperCol={{ span: 14 }}
                    onFinish={(values) => {
                        console.log({ values });
                        submit(values)
                    }}
                    onFinishFailed={(error) => {
                        console.log({ error });
                    }}
                    form={form}

                >
                    <Form.Item
                        name="category"
                        label="Category"
                        rules={[
                            {
                                required: true,
                                message: "Please enter your name",
                            },
                            { whitespace: true },
                            { min: 3 },
                        ]}
                        hasFeedback
                    >
                        <Input placeholder="Type your name" value={object?.categoryName} />
                    </Form.Item>
                    <Form.Item wrapperCol={{ span: 24 }}>
                    <Button block type="primary" htmlType="submit">
                            {props.status == 1 ? "Add Category" : "Edit"}
                    </Button>
                    </Form.Item>
                    </Form>

                </Modal>
        </>
    );
}


export default CustomModal;