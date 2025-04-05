import { PagingInfo } from "../PagingInfo";
import { ApplicationDto } from "./ApplicationDto";

export interface GetApplicationsResponse {
    applications: ApplicationDto[],
    pagingInfo: PagingInfo
}