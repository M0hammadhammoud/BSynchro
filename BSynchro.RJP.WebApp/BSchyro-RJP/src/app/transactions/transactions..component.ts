import { Component, Input, OnInit } from '@angular/core';
import { TransactionComponent } from './transaction/transaction.component';
import { NewAccountComponent } from './new-account/new-account.component';
import { CustomerDTO, GetCustomerInformationResponse } from '../customer/customer.model';

@Component({
  selector: 'app-transactions',
  standalone: true,
  imports: [TransactionComponent, NewAccountComponent],
  templateUrl: './transactions.component.html',
  styleUrl: './transactions..component.css',
})
export class Transactionsomponent{
  @Input({ required: true }) customer!: CustomerDTO;
  isAddingAccount = false;

  constructor() {
  }

  onStartAddAccount() {
    this.isAddingAccount = true;
  }

  onCloseAddAccount() {
    this.isAddingAccount = false;
  }
}
