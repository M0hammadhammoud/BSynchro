import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CustomerService } from '../../services/customer.service';
import { BaseResponse } from '../../models/models';

@Component({
  selector: 'app-new-account',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './new-account.component.html',
  styleUrl: './new-account.component.css'
})
export class NewAccountComponent {
  @Input({ required: true }) customerId!: string;
  @Output() close = new EventEmitter<void>();
  initialAmount = 0;
  constructor(private customerService: CustomerService) {
  }
  onCancel() {
    this.close.emit();
  }

  onSubmit() {
    this.customerService.openAccount(this.customerId, this.initialAmount).subscribe({
      next: (response: BaseResponse) => {
        if (response.httpStatusCode === 200) {
          this.close.emit();
        } else {
          alert("Unable to create account console logs.");
          console.error('Error:', response.message);
        }
      },
      error: (err) => { console.error('HTTP Error:', err); alert("Unable to create account console logs."); }
    });
  }
}
