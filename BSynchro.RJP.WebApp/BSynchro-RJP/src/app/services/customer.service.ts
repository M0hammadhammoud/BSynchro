import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetAllCustomersResponse, GetCustomerInformationRequest, GetCustomerInformationResponse, OpenAccountRequest } from '../customer/customer.model';
import { BaseResponse } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class CustomerService {
    private getAllCustomersUrl = 'https://localhost:7293/api/customers/GetAll';
    private getCustomerInformationUrl = 'https://localhost:7293/api/customers/GetCustomerInformation';
    private openAccountUrl = 'https://localhost:7293/api/accounts/Open';

    constructor(private http: HttpClient) { }

    getAllCustomers(): Observable<GetAllCustomersResponse> {
        return this.http.get<GetAllCustomersResponse>(this.getAllCustomersUrl);
    }

    getCustomerInformation(customerId: string): Observable<GetCustomerInformationResponse> {
        const request: GetCustomerInformationRequest = {
            customerId: customerId,
        };

        return this.http.post<GetCustomerInformationResponse>(this.getCustomerInformationUrl, request);
    }

    openAccount(customerId: string, initialCredit: number): Observable<BaseResponse> {
        const request: OpenAccountRequest = {
            customerId: customerId, 
            initialCredit: initialCredit
        };

        return this.http.post<BaseResponse>(this.openAccountUrl, request);
    }
}