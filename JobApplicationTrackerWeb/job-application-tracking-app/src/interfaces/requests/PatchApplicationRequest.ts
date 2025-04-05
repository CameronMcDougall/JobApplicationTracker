import { ApplicationStatus } from "../../enums/ApplicationStatus.enum";

export interface PatchApplicationRequest {
    status: ApplicationStatus
}