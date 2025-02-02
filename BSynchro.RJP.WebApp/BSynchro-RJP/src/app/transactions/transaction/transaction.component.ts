import { Component, Input } from '@angular/core';
import { Task } from './transaction.component.model';
import { CardComponent } from "../../shared/card/card.component";
import { DatePipe } from '@angular/common';
import { TransactionDTO } from '../../customer/customer.model';

@Component({
  selector: 'app-transaction',
  standalone: true,
  imports: [CardComponent, DatePipe],
  templateUrl: './transaction.component.html',
  styleUrl: './transaction.component.css',
})
export class TransactionComponent {
  @Input({ required: true }) transaction!: TransactionDTO;
}
