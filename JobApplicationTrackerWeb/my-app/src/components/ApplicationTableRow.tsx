import { Link } from "react-router-dom";
import { ApplicationStatus } from "../enums/ApplicationStatus.enum";
import { Application } from "../interfaces/Application";
import { JSX } from "react";

function ApplicationTableRow(app: Application): JSX.Element {
    return <>
        {
            <tr>
                <td>
                    <Link to={`/application/${app.id}`}>{app.id.toString()}</Link>
                </td>
                <td>{app.position}</td>
                <td>{app.companyName}</td>
                <td>{app.dateApplied?.toDateString()}</td>
                <td>{ApplicationStatus[app.status]}</td>
            </tr>
        }
    </>
}

export default ApplicationTableRow;