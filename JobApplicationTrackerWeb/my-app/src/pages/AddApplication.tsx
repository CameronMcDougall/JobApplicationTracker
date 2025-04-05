import { Container, Row, Col, Form, Button } from "react-bootstrap";
import ApplicationClient from "../rest-client/applicationClient";
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';
import { AddApplicationRequest } from "../interfaces/requests/AddApplicationRequest";
import { ApplicationStatus } from "../enums/ApplicationStatus.enum";
import DatePicker from "react-datepicker";
import { ConvertApplicationStatus } from "../utils/applicationUtils";
import moment from "moment";
import "moment-timezone";
import "../css/addApplication.css";

function AddApplication() {
    const [companyName, setCompanyName] = useState('');
    const [position, setPosition] = useState('');
    const [dateApplied, setDataApplied] = useState(new Date());
    const [applicationStatus, setApplicationStatus] = useState(ApplicationStatus.Interview);
    const navigate = useNavigate();

    async function CreateApplication() {
        const request: AddApplicationRequest = {
            companyName: companyName,
            dateApplied: dateApplied,
            position: position,
            status: applicationStatus
        }

        let value = await ApplicationClient.addApplication(request);
        if (value.statusCode == 201) {
            navigate('/')
        }
    }

    function HandleChangedStatus(value: string | null) {
        if (value == null) {
            return;
        }

        setApplicationStatus(ConvertApplicationStatus(value));
    }

    return (<>
        <Container>
            <Row>
                <h2 id="add-application-title" className="text-center">Create a Application</h2>
            </Row>
            <Row>
                <Col className="col-sm-2"></Col>
                <Col>
                    <label htmlFor="company-name">
                        Company Name
                    </label>
                </Col>
                <Col>
                    <label htmlFor="position" >
                        Position
                    </label>
                </Col>
                <Col className="col-sm-2"></Col>
            </Row>
            <Row>
                <Col className="col-sm-2"></Col>
                <Col>
                    <input name="company-name" aria-label="Company Name" value={companyName} onChange={(change) => setCompanyName(change.target.value)} />
                </Col>
                <Col>
                    <input name="position" value={position} onChange={(change) => setPosition(change.target.value)} />
                </Col>
                <Col className="col-sm-2"></Col>
            </Row>
            <Row>
                <Col className="col-sm-2"></Col>
                <Col>
                    <label htmlFor="date-applied">
                        Date Applied
                    </label>
                </Col>
                <Col>
                    <label htmlFor="select-status">
                        Status
                    </label>
                </Col>
                <Col className="col-sm-2"></Col>
            </Row>
            <Row>
                <Col className="col-sm-2"></Col>
                <Col>
                    <DatePicker name="date-applied" dateFormat='dd-MM-YYYY' selected={dateApplied} onChange={(date) => { setDataApplied(moment(date).tz('Greenwich').toDate()) }} />
                </Col>
                <Col>
                    <Form.Select name="select-status" aria-label="Select Status" value={applicationStatus} onChange={(change) => { HandleChangedStatus(change.target.value) }}>
                        <option key="0" value={ApplicationStatus.Interview}>Interview</option>
                        <option key="1" value={ApplicationStatus.Rejected}>Rejected</option>
                        <option key="2" value={ApplicationStatus.Offer}>Offer</option>
                    </Form.Select>
                </Col>
                <Col className="col-sm-2"></Col>
            </Row>
            <Row>
                <Col>
                    <Button className="float-end" onClick={() => { CreateApplication() }}>Submit</Button>
                </Col>
            </Row>
        </Container>
    </>);
}


export default AddApplication;