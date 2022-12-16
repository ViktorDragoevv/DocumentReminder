import React, { useState, useEffect } from 'react';
import { Button, Modal } from 'antd';
import * as yup from "yup";
import { useCookies } from "react-cookie";
import dayjs from 'dayjs';
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


function CustomModalDocument(props) {


    const [documentObject, setDocumentObject] = useState();
    const [locations, setLocations] = useState();
    const [categorySelect, setCategory] = useState();
    const [contactsSelect, setContacts] = useState();
    const [companySelect, setCompany] = useState();
    const [defValueSelect, setDefValueSelect] = useState();
    const [cookies, setCookie] = useCookies();
    console.log(documentObject);
    const { TextArea } = Input;
    const [open, setOpen] = useState(false);
    const [confirmLoading, setConfirmLoading] = useState(false);
    const [modalText, setModalText] = useState('Content of the modal');
    const { Option } = Select;
    const dateFormat = 'YYYY/MM/DD';


    const showModal = () => {
        setOpen(true);
        setDocumentObject(props.selectedRowKeys);
        console.log(props.selectedRowKeys);
    };

    const handleOk = () => {
        setModalText('The modal will be closed after two seconds');
        setConfirmLoading(true);
        //refreshPage();

        if (props.status == 1) {
            addDocument(documentObject.categoryName);
        }
        else if (props.status == 2) {
            console.log(documentObject);
            editDocument(documentObject.categoryName);
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
    function convert(str) {
        var date = new Date(str),
            mnth = ("0" + (date.getMonth() + 1)).slice(-2),
            day = ("0" + date.getDate()).slice(-2);
        return [date.getFullYear(), mnth, day].join("-");
    }

    const addDocument = async (input) => {
        console.log(input);

        var data = {
            CategoryID: input.category,
            Name: input.name,
            Status: input.status,
            ContactID: input.contact,
            locationID: input.location,
            CompanyID: input.company,
            Comments: input.comment,
            ExpirationDate: convert(input.date),
        }
        console.log(data);
        var response = await fetch('https://localhost:7174/api/DocumentModels', {
            method: 'POST',
            mode: 'cors',
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': `${cookies.Authorization}`
            }),
            body: JSON.stringify(data)

        });
        //var newContactLocation = locations.filter(x => x.value == input?.location);
        var newContact = await response.json();
        //console.log({ ...newContact, viewLocation: { id: newContactLocation[0]?.value, name: newContactLocation[0]?.label } });
        console.log(newContact);
        props.update(newContact);

    }
    const editDocument = async (input) => {
        // jsonDataa;
        console.log(input);
        var editedContact = await fetch('https://localhost:7174/api/DocumentModels/' + documentObject.id, {
            method: 'PUT',
            mode: 'cors',
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': `${cookies.Authorization}`
            }),
            body: JSON.stringify({ ...input, id: documentObject.id, locationID: input.location })

        })

        var updatedContactLocation = locations.filter(x => x.value == input.location);
        //props.update({ ...input, id: contactObject.id, viewLocation: { id: updatedContactLocation[0]?.value, name: updatedContactLocation[0]?.label } });
        var editedContactResponse = await editedContact.json();
        console.log(editedContactResponse);
        props.update(editedContactResponse);

    }


    const handleChange = e => {
        //setInput(e.target.value);
        setDocumentObject({ ...documentObject, firstName: e.target.value, });
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
            addDocument(values);
        }
        else if (props.status == 2) {
            console.log(documentObject);
            editDocument(values);
            console.log(values);
        }
    };
    

    var handleSubmit = (event) => {
        event.preventDefault();
        console.log(props.status);
        if (props.status == 1) {
            addDocument(documentObject.categoryName);
        }
        else if (props.status == 2) {
            console.log(documentObject);
            editDocument(documentObject.categoryName);
        }
    }
   


    const [form] = Form.useForm();
    React.useEffect(() => {
        form.setFieldsValue({
            name: documentObject?.name,
            status: documentObject?.status,
            email: documentObject?.email,
            comment: documentObject?.comments,
            location: documentObject?.viewLocation?.id,
            category: documentObject?.viewCategory?.id,
            contact: documentObject?.viewContact?.id,
            company: documentObject?.viewCompany?.id,
            date: dayjs(documentObject?.expirationDate),
        });
        console.log(dayjs(documentObject?.expirationDate));
        async function fetchLocation() {


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
            console.log(data);
            setLocations(data);

        };

        async function fetchCategory() {

            const response = await fetch('https://localhost:7174/api/CategoryModels',
                {

                    headers: new Headers({
                        'Content-Type': 'application/json',
                        'Authorization': `${cookies.Authorization}`
                    }),
                });
            let data = await response.json();
            console.log(data);
            setCategory(data);
        };
        async function fetchContacts() {

            const response = await fetch('https://localhost:7174/api/ContactsModels',
                {

                    headers: new Headers({
                        'Content-Type': 'application/json',
                        'Authorization': `${cookies.Authorization}`
                    }),
                });
            let data = await response.json();
            console.log(data);
            setContacts(data);
        };
        async function fetchCompany() {

            const response = await fetch('https://localhost:7174/api/CompanyModels',
                {

                    headers: new Headers({
                        'Content-Type': 'application/json',
                        'Authorization': `${cookies.Authorization}`
                    }),
                });
            let data = await response.json();
            console.log(data);
            setCompany(data);
        };
        fetchCompany();
        fetchContacts();
        fetchLocation();
        fetchCategory();
    }, [documentObject]);

    

    return (
        <>
            <Button type="primary" onClick={showModal}>
                {props.status == 1 ? "Add Document" : "Edit"}
            </Button>

            <Modal
                title={props.status == 1 ? "Add Document" : "Edit"}
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

                    <Form.Item name="category" label="Category" requiredMark="optional">
                        <Select placeholder="Select  Category" >
                            {categorySelect?.map(categorySelect => (
                                <Select.Option value={categorySelect.id} key={categorySelect.categoryName}>
                                    {categorySelect.categoryName}
                                </Select.Option>
                            ))}


                        </Select>
                    </Form.Item>

                    <Form.Item name="contact" label="Contacts" requiredMark="optional">
                        <Select placeholder="Select  Category" >
                            {contactsSelect?.map(contactsSelect => (
                                <Select.Option value={contactsSelect.id} key={contactsSelect.firstName}>
                                    {contactsSelect.firstName}
                                </Select.Option>
                            ))}


                        </Select>
                    </Form.Item>


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
                        <Input type="text" placeholder="Type your name" value={documentObject?.name}></Input>
                    </Form.Item>

                    <Form.Item name="status" label="Status" requiredMark="optional">
                        <Select placeholder="Select Type" value={documentObject?.status} options={[
                            {
                                value: 'Expiry',
                                label: 'Expiry',
                            },
                            {
                                value: 'No Expiry',
                                label: 'No Expiry',
                            },
                        ]}> 
                        </Select>
                    </Form.Item>
                    <Form.Item name="date" label="Date" requiredMark="required">
                       
                    <DatePicker format={dateFormat} value={dayjs(documentObject?.name, 'YYYY-MM-DD')} />
                        
                    </Form.Item>

                    <Form.Item name="comment" label="Comments">

                        <TextArea
                            showCount
                            value={documentObject?.comments}
                            maxLength={100}
                            style={{ height: 120, resize: 'none' }}
                            placeholder="Comments"
                        />
                    </Form.Item>

                    <Form.Item name="location" label="Location" requiredMark="optional">
                        <Select placeholder="Select location" >
                            {locations?.map(locations => (
                                <Select.Option value={locations.id} key={locations.name}>
                                    {locations.name}
                                </Select.Option>
                            ))}


                        </Select>
                    </Form.Item>

                    <Form.Item name="company" label="Company" requiredMark="optional">
                        <Select placeholder="Select company" >
                            {companySelect?.map(company => (
                                <Select.Option value={company.id} key={company.name}>
                                    {company.name}
                                </Select.Option>
                            ))}


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


export default CustomModalDocument;