import { useNavigate, useParams } from "react-router-dom";
import ApplicationClient from "../rest-client/applicationClient";
import { ApplicationDto } from "../interfaces/responses/ApplicationDto";
import { ConvertToApplication, ConvertApplicationStatus } from "../utils/applicationUtils";
import { Container, Row, Col, Form, Button } from "react-bootstrap";
import { useEffect, useState } from "react";
import { ApplicationStatus } from "../enums/ApplicationStatus.enum";
import { ToastContainer, toast } from 'react-toastify';
import "../css/application.css";

function Application() {
    const [id, setId] = useState(-1);
    const [companyName, setCompanyName] = useState('');
    const [position, setPosition] = useState('');
    const [dateApplied, setDateApplied] = useState(new Date());
    const [applicationStatus, setApplicationStatus] = useState<ApplicationStatus>(ApplicationStatus.Interview);
    const navigate = useNavigate();

    function UpdateStatus(update: string) {
        if (update === applicationStatus.toString()) {
            return;
        }

        var status = ConvertApplicationStatus(update);
        ApplicationClient.updateApplication(id, {
            status: status
        }).then(response => {
            if (response.statusCode == 204) {
                toast.success('Successfully updated status');
            } else if (response.statusCode >= 400) {
                toast.error('Failed to update status');
            }
        }).catch(error => {
            toast.error('Failed to update status');
        });

        setApplicationStatus(status);
    }

    useEffect(() => {
        if (applicationId === undefined) {
            return;
        }

        if (id !== -1) {
            return;
        }

        let parsedId = parseInt(applicationId);
        setId(parsedId)
        ApplicationClient.getApplication(parsedId).then(data => {
            let dto = data.body.application as ApplicationDto;
            let application = ConvertToApplication(dto);

            setCompanyName(application.companyName);
            setApplicationStatus(application.status);
            setDateApplied(application.dateApplied);
            setPosition(application.position);
        });

    })
    let { applicationId } = useParams();

    return (
        <Container>
            <Row>
                <ToastContainer />
                <Col>
                    <Button className="float-end" onClick={() => navigate('/')}>Go Back</Button>
                </Col>
            </Row>
            <Row>
                <Col className="col-sm-2"></Col>
                <Col>
                    <h2>Application {applicationId}</h2>
                </Col>
            </Row>

            <Row>
                <Col className="col-sm-2">
                </Col>
                <Col>
                    <label>
                        Company Name
                    </label>
                </Col>
                <Col>
                    <label>
                        Position
                    </label>
                </Col>
                <Col className="col-sm-2"></Col>
            </Row>
            <Row>
                <Col className="col-sm-2"></Col>
                <Col>
                    <h4>{companyName}</h4>
                </Col>
                <Col>
                    <h4>{position}</h4>
                </Col>
                <Col className="col-sm-2"></Col>
            </Row>
            <Row>
                <Col className="col-sm-2"></Col>
                <Col>
                    <label>
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
                    <h4>{dateApplied.toDateString()}</h4>
                </Col>
                <Col>
                    <Form.Select name="select-status" id="status-selecter" aria-label="Select Status" value={applicationStatus} onChange={(change) => { UpdateStatus(change.target.value) }}>
                        <option key="0" value={ApplicationStatus.Interview}>Interview</option>
                        <option key="1" value={ApplicationStatus.Rejected}>Rejected</option>
                        <option key="2" value={ApplicationStatus.Offer}>Offer</option>
                    </Form.Select>
                </Col>
                <Col className="col-sm-2"></Col>
            </Row>
        </Container>)
}

export default Application;
