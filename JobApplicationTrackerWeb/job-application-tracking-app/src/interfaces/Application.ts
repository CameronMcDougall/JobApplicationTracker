import { ApplicationStatus } from "../enums/ApplicationStatus.enum";

export interface Application {
    id: Number,
    companyName: string,
    position: string,
    status: ApplicationStatus,
    dateApplied: Date
}