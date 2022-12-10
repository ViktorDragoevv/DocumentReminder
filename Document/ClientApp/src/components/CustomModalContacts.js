import React, { useState, useEffect } from 'react';
import { Button, Modal } from 'antd';
import { SmileOutlined } from '@ant-design/icons';
import * as yup from "yup";
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


let schema = yup.object().shape({
    name: yup.string().required(),
    age: yup
        .number()
        .required()
        .typeError('Number only.')
        .positive()
        .integer()
        .round(),
});

const yupSync = {
    async validator({ field }, value) {
        await schema.validateSyncAt(field, { [field]: value });
    },
};




function CustomModalContacts(props) {

    const [contactObject, setContactObject] = useState();


    const [open, setOpen] = useState(false);
    const [confirmLoading, setConfirmLoading] = useState(false);
    const [modalText, setModalText] = useState('Content of the modal');
    const { Option } = Select;
    //const [contactObjectFromParent, setContactObjectFromParent] = useState();


    const showModal = () => {
        setOpen(true);
        setContactObject(props.selectedRowKeys);
        console.log(props.selectedRowKeys);
    };

    const handleOk = () => {
        setModalText('The modal will be closed after two seconds');
        setConfirmLoading(true);
        //refreshPage();

        if (props.status == 1) {
            addcategory(contactObject.categoryName);
        }
        else if (props.status == 2) {
            console.log(contactObject);
            editCategory(contactObject.categoryName);
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
            "firstName": input.firstName,
            "lastName": input.firstName,
            "email": input.email,
            "phoneNumber": input.phoneNumber
        }

        console.log("funk:" + input);

        var response = await fetch('https://localhost:7174/api/ContactsModels', {  // Enter your IP address here

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

        console.log(input.id);
        var jsonDataa = { ...input, id: contactObject.id };
        console.log("vliza:");
        //'console.log(JSON.stringify(...input,contactObject.id));
        console.log(jsonDataa);
        fetch('https://localhost:7174/api/ContactsModels/' + contactObject.id, {  // Enter your IP address here

            method: 'PUT',
            mode: 'cors',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(jsonDataa) // body data type must match "Content-Type" header

        });
        props.update(jsonDataa);

    }


    const handleChange = e => {
        //setInput(e.target.value);
        setContactObject({ ...contactObject, firstName: e.target.value, });
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
            addcategory(values);
        }
        else if (props.status == 2) {
            console.log(contactObject);
            editCategory(values);
        }
    };

    var handleSubmit = (event) => {
        event.preventDefault();
        console.log(event.target); // from elements property
        //console.log(event.target.username.value)          // or directly
        console.log(props.status);
        if (props.status == 1) {
            addcategory(contactObject.categoryName);
        }
        else if (props.status == 2) {
            console.log(contactObject);
            editCategory(contactObject.categoryName);
        }
    }


    const [form] = Form.useForm();
    React.useEffect(() => {
        form.setFieldsValue({
            firstName: contactObject?.firstName,
            lastName: contactObject?.lastName,
            email: contactObject?.email,
            phoneNumber: contactObject?.phoneNumber,
        });
    }, [contactObject]);

    return (
        <>
            <Button type="primary" onClick={showModal}>
                {props.status == 1 ? "Add Contact" : "Edit"}
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
                        name="firstName"
                        label="First Name"
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
                        <Input type="text" placeholder="Type your name" value={contactObject?.firstName}></Input>
                    </Form.Item>

                    <Form.Item
                        name="lastName"
                        label="Last Name"
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
                        <Input placeholder="Type your name" value={contactObject?.firstName} />
                    </Form.Item>

                    <Form.Item
                        name="email"
                        label="Email"
                        rules={[
                            {
                                required: true,
                                message: "Please enter your email",
                            },
                            { type: "email", message: "Please enter a valid email" },
                        ]}
                        hasFeedback
                    >
                        <Input placeholder="Type your email" />
                    </Form.Item>

                    <Form.Item
                        name="phoneNumber"
                        label="Phone number"
                        rules={[
                            {
                                required: true,
                                message: "Please enter your phone number",
                            },
                            { whitespace: true },
                            { min: 10 },
                        ]}
                        hasFeedback
                    >
                        <Input placeholder="Type your phone number" type="number" />
                    </Form.Item>
                    


                   

                    <Form.Item wrapperCol={{ span: 24 }}>
                        <Button block type="primary" htmlType="submit">
                            Add Contact
                        </Button>
                    </Form.Item>
                </Form>



            </Modal>
        </>
    );

       
}


export default CustomModalContacts;