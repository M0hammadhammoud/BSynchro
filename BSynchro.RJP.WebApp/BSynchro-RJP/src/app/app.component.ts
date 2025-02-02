import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from './header/header.component';
import { CustomerService } from './services/customer.service';
import { CustomerDTO, GetAllCustomersResponse, GetCustomerInformationResponse } from './customer/customer.model';
import { CustomerComponent } from './customer/customer.component';
import { Transactionsomponent } from './transactions/transactions..component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HeaderComponent, CustomerComponent, Transactionsomponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  selectedCustomerId?: string;
  selectedCustomer: CustomerDTO | undefined;

  customers: CustomerDTO[] = [];

  constructor(private customerService: CustomerService) { }

  ngOnInit(): void {
    this.customerService.getAllCustomers().subscribe({
      next: (response: GetAllCustomersResponse) => {
        if (response.httpStatusCode === 200) {
          this.customers = response.customers;
        } else {
          console.error('Error:', response.message);
        }
      },
      error: (err) => console.error('HTTP Error:', err)
    });
  }

  onSelectCustomer(id: string) {
    this.selectedCustomerId = id;
    this.customerService.getCustomerInformation(this.selectedCustomerId).subscribe({
      next: (response: GetCustomerInformationResponse) => {
        if (response.httpStatusCode === 200) {
          this.selectedCustomer = response.customer;
        } else {
          console.error('Error:', response.message);
        }
      },
      error: (err) => console.error('HTTP Error:', err)
    });
  }
}
