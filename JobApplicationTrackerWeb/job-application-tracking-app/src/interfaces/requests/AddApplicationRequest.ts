import { ApplicationStatus } from "../../enums/ApplicationStatus.enum";

export interface AddApplicationRequest {
    companyName: string,
    position: string,
    status: ApplicationStatus,
    dateApplied: Date
}