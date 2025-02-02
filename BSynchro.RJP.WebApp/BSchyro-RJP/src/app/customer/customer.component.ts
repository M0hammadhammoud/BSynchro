import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CardComponent } from "../shared/card/card.component";
import { CustomerDTO } from './customer.model';

@Component({
  selector: 'app-customer',
  standalone: true,
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.css',
  imports: [CardComponent],
})
export class CustomerComponent {
  @Input({ required: true }) customer!: CustomerDTO;
  @Input({ required: true }) selected!: boolean;
  @Output() select = new EventEmitter();

  onSelectCustomer() {
    this.select.emit(this.customer.customerId);
  }
}
