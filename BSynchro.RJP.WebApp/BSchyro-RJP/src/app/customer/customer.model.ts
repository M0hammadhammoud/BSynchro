import { BaseResponse } from "../models/models";

export interface CustomerDTO {
  customerId: string;
  firstName: string;
  lastName: string;
  userId: string;
  accounts: AccountDTO[];
}

export interface AccountDTO {
  accountId: string;
  customerId: string;
  balance: number;
  transactions: TransactionDTO[];
}

export interface TransactionDTO {
  transactionId: string;
  accountId: string;
  amount: number;
  transactionType: number;
  transactedOn: string;
}

export interface GetAllCustomersResponse extends BaseResponse {
  customers: CustomerDTO[];
}

export interface GetCustomerInformationRequest {
  customerId: string;
}

export interface GetCustomerInformationResponse extends BaseResponse {
  customer: CustomerDTO;
}

export interface OpenAccountRequest {
  customerId: string;
  initialCredit: number;
}