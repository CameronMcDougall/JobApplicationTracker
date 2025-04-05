import ApplicationClient from "../rest-client/applicationClient";
import { useNavigate } from 'react-router-dom';
import { useState, useEffect } from "react";
import { PageOrder } from "../enums/PagingOrder.enum";
import { Application } from "../interfaces/Application"
import { PagingInfo } from "../interfaces/PagingInfo";
import { Button, Col, Container, Form, Row, Table } from "react-bootstrap";
import { ApplicationDto } from "../interfaces/responses/ApplicationDto";
import ConvertToApplication from "../utils/applicationUtils";
import ApplicationTableRow from "../components/ApplicationTableRow";
import { ToastContainer, toast } from 'react-toastify';

import "../css/home.css";

function Home() {
    const [pageNumber, setPageNumber] = useState(0);
    const [pageSize, setPageSize] = useState(2);
    const [applications, setApplications] = useState<Application[]>([]);
    const [shouldReload, setShouldReloadPage] = useState(true);
    const [pagingInfo, setPagingInfo] = useState<PagingInfo>({
        totalItems: 0,
        totalPages: 0,
        current: 0
    });
    const pageOptions = [2, 5, 10, 50]

    function getApplications() {
        if (!shouldReload) {
            return;
        }

        setShouldReloadPage(false);
        ApplicationClient.getApplications(pageSize, pageNumber, PageOrder.Desending)
            .then(response => {
                if (response.statusCode == 200) {
                    let responseBody = response.body;
                    let applicationsResponse = responseBody.applications as ApplicationDto[];
                    let applications = applicationsResponse.map(e => {
                        return ConvertToApplication(e);
                    });

                    setApplications(applications);
                    setPagingInfo(responseBody.pagingInfo);
                }
            })
            .catch(error => {
                toast.error('Failed to get applications');
            });
    }

    useEffect(() => {
        getApplications();
    })

    function CanGoToPreviousPage() {
        return pageNumber > 0;
    }

    function CanGoToNextPage() {
        return pageNumber + 1 < +pagingInfo.totalPages;
    }

    function HandlePageSizeChange(newPageSize: number) {
        setPageSize(newPageSize);
        setPageNumber(0);
        setShouldReloadPage(true);
        getApplications();
    }

    function HandlePageNumberChange(newPageNumber: number) {
        setPageNumber(newPageNumber);
        setShouldReloadPage(true);
        getApplications();
    }

    const navigate = useNavigate();
    return (<>
        <Container>
            <ToastContainer />
            <Row>
                <Col>
                    <Button id="add-button" className="float-end" onClick={() => navigate('/add')}>Add</Button>
                </Col>
            </Row>
            <Row>
                <Col>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>
                                    Id
                                </th>
                                <th>
                                    Position
                                </th>
                                <th>
                                    Company
                                </th>
                                <th>
                                    Date Applied
                                </th>
                                <th>
                                    Status
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            {applications.map(app => {
                                return <ApplicationTableRow
                                    key={app.id.toString()}
                                    id={app.id}
                                    companyName={app.companyName}
                                    dateApplied={app.dateApplied}
                                    position={app.position}
                                    status={app.status} />
                            })}
                        </tbody>
                    </Table>
                </Col>
            </Row>
            <Row>
                <Col className="col-sm-3">
                </Col>
                <Col className="col-sm-7">
                    <Button className="float-end pagination" id="next-button" disabled={!CanGoToNextPage()} onClick={() => {
                        HandlePageNumberChange(pageNumber + 1);
                    }}>Next</Button>
                    <p className="float-end pagination" id="current-page">{pagingInfo.totalItems === 0 ? "0" : (+pagingInfo.current + 1).toString()} of {pagingInfo.totalPages.toString()}</p>
                    <Button className="float-end pagination" id="previous-button" disabled={!CanGoToPreviousPage()} onClick={() => {
                        HandlePageNumberChange(pageNumber - 1);
                    }}>Previous</Button>

                </Col>
                <Col className="col-sm-2">
                    <Form.Select id="page-size" className="float-end pagination" aria-label="Select Status" value={pageSize} onChange={(change) => { HandlePageSizeChange(parseInt(change.target.value)) }}>
                        {
                            pageOptions.map(option => {
                                return <option key={option} value={option}>{option}</option>
                            })
                        }
                    </Form.Select>
                </Col>
            </Row>
        </Container>
    </>);
}

export default Home;