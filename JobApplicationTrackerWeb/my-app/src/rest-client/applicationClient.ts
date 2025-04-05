import axios, { InternalAxiosRequestConfig, AxiosError, AxiosResponse } from "axios";
import { PageOrder } from "../enums/PagingOrder.enum";
import { PatchApplicationRequest } from "../interfaces/requests/PatchApplicationRequest";
import { AddApplicationRequest } from "../interfaces/requests/AddApplicationRequest";
import AppSettings from '../appsettings.json';

export default class ApplicationClient {
  static jobTrackingApi = axios.create({
    baseURL: AppSettings.APP_JOB_TRACK_SYS_URL,
    // have full status in the response body instead of throw exceptions
    validateStatus: () => true
  });
  static applicationsUrl = "applications";

  static async getApplication(id: number) {
    const response = await this.jobTrackingApi.get(`${this.applicationsUrl}/${id}`);
    return {
      statusCode: response.status,
      body: response.data
    }
  }

  static async getApplications(pageSize: number, pageNumber: number, pageOrder: PageOrder) {
    const response = await this.jobTrackingApi.get(`${this.applicationsUrl}?pageNumber=${pageNumber}&pageSize=${pageSize}&pageOrder=${pageOrder}`);
    return {
      statusCode: response.status,
      body: response.data
    }
  }

  static async addApplication(request: AddApplicationRequest) {
    this.jobTrackingApi.interceptors.request.clear();
    const response = await this.jobTrackingApi.post(`${this.applicationsUrl}`, request);
    return {
      statusCode: response.status,
      body: response.data
    }
  }

  static async updateApplication(id: number, request: PatchApplicationRequest) {
    const response = await this.jobTrackingApi.patch(`${this.applicationsUrl}/${id}`, request);
    return {
      statusCode: response.status,
      body: response.data
    }
  }
}