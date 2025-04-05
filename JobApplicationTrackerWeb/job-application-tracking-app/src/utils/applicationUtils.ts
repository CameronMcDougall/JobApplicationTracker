import moment from "moment";
import { ApplicationDto } from "../interfaces/responses/ApplicationDto";
import { ApplicationStatus } from "../enums/ApplicationStatus.enum";

export function ConvertToApplication(applicationDto: ApplicationDto) {
    return {
        id: applicationDto.id,
        companyName: applicationDto.companyName,
        dateApplied: moment(applicationDto.appliedDate, 'YYYY-MM-DD').utc().toDate(),
        position: applicationDto.position,
        status: ConvertApplicationStatus(applicationDto.status)
    }
}

export function ConvertApplicationStatus(value: string) {
    if (value == "1") {
        return ApplicationStatus.Offer;
    } else if (value == "2") {
        return ApplicationStatus.Rejected;
    }
    return ApplicationStatus.Interview;
}

export default ConvertToApplication;