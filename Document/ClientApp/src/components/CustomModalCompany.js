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

function CustomModalCompany(props) {




    const [companyObject, setCompanyObject] = useState();
    const [locations, setLocations] = useState();
    const [defValueSelect, setDefValueSelect] = useState();
    const [cookies, setCookie] = useCookies();
    console.log(companyObject);

    const [open, setOpen] = useState(false);
    const [confirmLoading, setConfirmLoading] = useState(false);
    const [modalText, setModalText] = useState('Content of the modal');
    const { Option } = Select;
    //let [locationOptions, setLocationOptions] = useState();
    //const [contactObjectFromParent, setContactObjectFromParent] = useState();


    const showModal = () => {
        setOpen(true);
        setCompanyObject(props.selectedRowKeys);
        console.log(props.selectedRowKeys);
    };

    const handleOk = () => {
        setModalText('The modal will be closed after two seconds');
        setConfirmLoading(true);
        //refreshPage();

        if (props.status == 1) {
            addCompany(companyObject.categoryName);
        }
        else if (props.status == 2) {
            console.log(companyObject);
            editCompany(companyObject.categoryName);
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


    const addCompany = async (input) => {
        console.log(input);
        var data = {
            name: input.name,
            tradeName: input.tradeName,
            type: input.type,
            phone : input.phone,
            locationID: input.location,
        }
        console.log(data);
        var response = await fetch('https://localhost:7174/api/CompanyModels', {
            method: 'POST',
            mode: 'cors',
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': `${cookies.Authorization}`
            }),
            body: JSON.stringify(data)

        });
        //var newContactLocation = locations.filter(x => x.value == input?.location);
        var newCompany = await response.json();
        //console.log({ ...newContact, viewLocation: { id: newContactLocation[0]?.value, name: newContactLocation[0]?.label } });
        console.log(newCompany);
        props.update(newCompany);

    }
    const editCompany = async (input) => {
        // jsonDataa;
        console.log(input);
        var editedCompany = await fetch('https://localhost:7174/api/CompanyModels/' + companyObject.id, {
            method: 'PUT',
            mode: 'cors',
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': `${cookies.Authorization}`
            }),
            body: JSON.stringify({ ...input, id: companyObject.id, locationID: input.location })

        })

        var updatedContactLocation = locations.filter(x => x.value == input.location);
        //props.update({ ...input, id: contactObject.id, viewLocation: { id: updatedContactLocation[0]?.value, name: updatedContactLocation[0]?.label } });
        var editedContactResponse = await editedCompany.json();
        console.log(editedContactResponse);
        props.update(editedContactResponse);

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
        setCompanyObject({ ...companyObject, firstName: e.target.value, });
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
            addCompany(values);
        }
        else if (props.status == 2) {
            console.log(companyObject);
            editCompany(values);
            console.log(values);
        }
    };

    var handleSubmit = (event) => {
        event.preventDefault();
        console.log(props.status);
        if (props.status == 1) {
            addCompany(companyObject.categoryName);
        }
        else if (props.status == 2) {
            console.log(companyObject);
            editCompany(companyObject.categoryName);
        }
    }




    const [form] = Form.useForm();
    React.useEffect(() => {


        form.setFieldsValue({
            name: companyObject?.name,
            tradeName: companyObject?.tradeName,
            phone: companyObject?.phone,
            type: companyObject?.type,
            location: companyObject?.location?.id,
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
    }, [companyObject]);


    return (
        <>
            <Button type="primary" onClick={showModal}>
                {props.status == 1 ? "Add Company" : "Edit"}
            </Button>

            <Modal
                title={props.status == 1 ? "Add Company" : "Edit"}
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
                        name="name"
                        label="Name"
                        rules={[
                            {
                                required: true,
                                message: "Please enter name",
                            },
                            { whitespace: true },
                            { min: 3 },
                        ]}
                        hasFeedback
                    >
                        <Input type="text" placeholder="Type name" value={companyObject?.name}></Input>
                    </Form.Item>

                    <Form.Item
                        name="tradeName"
                        label="Trade Name"
                        rules={[
                            {
                                required: true,
                                message: "Please enter Trade Name",
                            },
                            { whitespace: true },
                            { min: 3 },
                        ]}
                        hasFeedback
                    >
                        <Input placeholder="Type your name" value={companyObject?.tradeName} />
                    </Form.Item>

                    <Form.Item
                        name="phone"
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

                    <Form.Item name="type" label="Select" rules={[
                        {
                            required: true,
                            message: "Please select type",
                        },
                        { whitespace: true },
                    ]}>
                        <Select placeholder="Select type" options={[
                            {
                                value: 'Vendor',
                                label: 'Vendor',
                            },
                            {
                                value: 'Customer',
                                label: 'Customer',
                            },
                        ]} >



                        </Select>
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


export default CustomModalCompany;