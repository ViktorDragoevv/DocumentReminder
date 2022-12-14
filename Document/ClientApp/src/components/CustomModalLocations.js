import React, { useState, useEffect } from 'react';
import { Button, Modal } from 'antd';
import { useCookies } from "react-cookie";
import {
    Cascader,
    DatePicker,
    Form,
    Input,
    InputNumber,
    Mentions,
    Select,
    TimePicker,
    TreeSelect,
} from 'antd';


function CustomModalLocations(props) {

    const [locationObject, setLocationObject] = useState();
    const [open, setOpen] = useState(false);
    const [confirmLoading, setConfirmLoading] = useState(false);
    const [modalText, setModalText] = useState('Content of the modal');
    const { Option } = Select;
    const [cookies, setCookie] = useCookies();





    const showModal = () => {
        setOpen(true);
        setLocationObject(props.selectedRowKeys);
        console.log(props.selectedRowKeys);
    };

    const handleOk = () => {
        setModalText('The modal will be closed after two seconds');
        setConfirmLoading(true);

        if (props.status == 1) {
            addLocation(locationObject.categoryName);
        }
        else if (props.status == 2) {
            console.log(locationObject);
            editLocation(locationObject.categoryName);
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


    const addLocation = async (input) => {


        console.log(input);

        console.log("funk:" + input);
        console.log(JSON.stringify(input));
        var response = await fetch('https://localhost:7174/api/LocationModels', {  // Enter your IP address here
            method: 'POST',
            mode: 'cors',
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': `${cookies.Authorization}`
            }),
            body: JSON.stringify(input) // body data type must match "Content-Type" header

        });
        var newCategory = await response.json();
        
        props.update(newCategory);

    }
    const editLocation = async (input) => {

        console.log(input.id);
        var jsonDataa = { ...input, id: locationObject.id};
        console.log("vliza:");
        //'console.log(JSON.stringify(...input,contactObject.id));
        console.log(jsonDataa);
        fetch('https://localhost:7174/api/LocationModels/' + locationObject.id, {  // Enter your IP address here
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

    const handleChange = e => {
        //setInput(e.target.value);
        setLocationObject({ ...locationObject, firstName: e.target.value, });
        //console.log(e.target.value);
    }

    // form

    const formItemLayout = {
        labelCol: {
            xs: { span: 24 },
            sm: { span: 6 },
        },
        wrapperCol: {
            xs: { span: 24 },
            sm: { span: 14 },
        },
    };

    const submit = (values) => {

        setOpen(false);
        if (props.status == 1) {
            addLocation(values);
        }
        else if (props.status == 2) {
            console.log(locationObject);
            editLocation(values);
        }
    };

    var handleSubmit = (event) => {
        event.preventDefault();
        console.log(event.target); // from elements property
        //console.log(event.target.username.value)          // or directly
        console.log(props.status);
        if (props.status == 1) {
            addLocation(locationObject.categoryName);
        }
        else if (props.status == 2) {
            console.log(locationObject);
            editLocation(locationObject.categoryName);
        }
    }


    const [form] = Form.useForm();
    React.useEffect(() => {
        form.setFieldsValue({
            name: locationObject?.name,
            address: locationObject?.address,
            code: locationObject?.code,
            city: locationObject?.city,
        });
        //setLocationOptions(contactObject?.locationID);
    }, [locationObject]);


    return (
        <>
            <Button type="primary" onClick={showModal}>
                {props.status == 1 ? "Add Location" : "Edit"}
            </Button>

            <Modal
                title={props.status == 1 ? "Add Location" : "Edit"}
                open={open}
                onOk={handleOk}
                confirmLoading={confirmLoading}
                onCancel={handleCancel}
                footer={null}
                forceRender

            >





                <Form
                    autoComplete="off"
                    labelCol={{ span: 8 }}
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
                        name="name"
                        label="Name"
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
                        <Input type="text" placeholder="Type name" value={locationObject?.firstName}></Input>
                    </Form.Item>

                    <Form.Item
                        name="code"
                        label="Code"
                        rules={[
                            {
                                required: true,
                                message: "Please enter your code",
                            },
                            { whitespace: true },
                            { min: 4 },
                        ]}
                        hasFeedback
                    >
                        <Input placeholder="Type your code" type="number" />
                    </Form.Item>

                    <Form.Item
                        name="address"
                        label="Address"
                        rules={[
                            {
                                required: true,
                                message: "Please enter your address",
                            },
                            { whitespace: true },
                            { min: 3 },
                        ]}
                        hasFeedback
                    >
                        <Input type="text" placeholder="Type your address" value={locationObject?.firstName}></Input>
                    </Form.Item>

                    <Form.Item
                        name="city"
                        label="City"
                        rules={[
                            {
                                required: true,
                                message: "Please enter your city",
                            },
                            { whitespace: true },
                            { min: 3 },
                        ]}
                        hasFeedback
                    >
                        <Input type="text" placeholder="Type your city" value={locationObject?.firstName}></Input>
                    </Form.Item>




                    <Form.Item wrapperCol={{ span: 24 }}>
                        <Button block type="primary" htmlType="submit">
                            {props.status == 1 ? "Add Location" : "Edit"}
                        </Button>
                    </Form.Item>
                </Form>



            </Modal>
        </>
    );



}


export default CustomModalLocations;