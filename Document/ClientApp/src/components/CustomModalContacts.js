import React, { useState, useEffect } from 'react';
import { Button, Modal } from 'antd';
import { SmileOutlined } from '@ant-design/icons';
import * as yup from "yup";
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
    const [locations, setLocations] = useState();
    const [defValueSelect, setDefValueSelect] = useState();
    const [cookies, setCookie] = useCookies();
    console.log(contactObject);

    const [open, setOpen] = useState(false);
    const [confirmLoading, setConfirmLoading] = useState(false);
    const [modalText, setModalText] = useState('Content of the modal');
    const { Option } = Select;
    //let [locationOptions, setLocationOptions] = useState();
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

        var jsonData = { ...input, locationID: input.location };

        var response = await fetch('https://localhost:7174/api/ContactsModels', {  // Enter your IP address here
            method: 'POST',
            mode: 'cors',
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': `${cookies.Authorization}`
            }),
            body: JSON.stringify(jsonData) // body data type must match "Content-Type" header

        });
        var newContactLocation = locations.filter(x => x.value == input.location);
        var newCategory = await response.json();
        props.update({ ...newCategory, viewLocation: { id: newContactLocation[0].value, name: newContactLocation[0].label }});

    }
    const editCategory = async (input) => {


        var jsonDataa = { ...input, id: contactObject.id, locationID: input.location };
        fetch('https://localhost:7174/api/ContactsModels/' + contactObject.id, {  // Enter your IP address here
            method: 'PUT',
            mode: 'cors',
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': `${cookies.Authorization}`
            }),
            body: JSON.stringify(jsonDataa) // body data type must match "Content-Type" header

        }).then((response) => response.json())
            .then((data) => console.log(data));;


        var updatedContactLocation = locations.filter(x => x.value == input.location);
        props.update({ ...input, id: contactObject.id, viewLocation: { id: updatedContactLocation[0].value, name: updatedContactLocation[0].label } });

    }

    /*const  loadLocation = async () => {

        async function fetchData() {

            const token = await authService.getAccessToken();
            const response = await fetch('https://localhost:7174/api/LocationModels', {
                headers: !token ? {} : { 'Authorization': `Bearer ${token}` },
                //headers: { 'Access-Control-Allow-Origin': '*' },
                //headers: { 'content-type': 'application/json; charset=utf-8' }
            });
            let data = await response.json();
            setDataLocations(AddKeyProp(data));

            console.log(data);
        };
        setLocations(locations.data);
        console.log(response);
    }*/

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
            console.log(values);
        }
    };

    var handleSubmit = (event) => {
        event.preventDefault();
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
            location: {
                value: contactObject?.location?.id,
                label: contactObject?.location?.name,
            }
        });
        async function fetchData() {

            
            const response = await fetch('https://localhost:7174/api/LocationModels',
                {

                    headers: new Headers({
                        'Content-Type': 'application/json',
                        'Authorization': `${cookies.Authorization}`
                    }),
                });
            let data = await response.json();
            const optionsforSelect = data.map((location, index) => ({

                value: location.id,
                label: location.name,

            }))
            setLocations(optionsforSelect);
            
        };

        fetchData();
        //setLocationOptions(contactObject?.locationID);
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

                    <Form.Item name="location" label="Location" requiredMark="optional">
                        <Select placeholder="Select your location" options={locations ?? []} >

                            
                                    
                        </Select>
                    </Form.Item>
                    


                   

                    <Form.Item wrapperCol={{ span: 24 }}>
                        <Button block type="primary" htmlType="submit">
                            {props.status == 1 ? "Add Contact" : "Edit"}
                        </Button>
                    </Form.Item>
                </Form>



            </Modal>
        </>
    );

       
}


export default CustomModalContacts;