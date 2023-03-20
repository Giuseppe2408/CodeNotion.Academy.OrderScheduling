import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject, combineLatest, debounceTime, Observable, Observer, switchMap } from 'rxjs';
import { Order, OrderClient } from './api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  searchCustomer: string = "";
  searchOrder: string = "";
  inputType = ["text", "text", "date", "date", "date", "date"]
  columnsToDisplay = ['id', 'customer', 'orderNumber', 'cuttingDate', 'preparationDate', 'bendingDate', 'assemblyDate'];
  orderForm!: FormGroup;
  
  searchFilter$ = new BehaviorSubject<{ customer?: string; orderNumber?: string }>({});
  addOrder$ = new BehaviorSubject<Order | null>(null);
  order$ = combineLatest([this.searchFilter$, this.addOrder$]).pipe(
    debounceTime(200),
    switchMap(([filter]) => this.orderClient.list(filter.customer, filter.orderNumber))
  )

  constructor(private fb: FormBuilder, private orderClient: OrderClient) {
    this.clearOrderForm()
  }

  clearOrderForm() {
    this.orderForm = this.fb.group({
      customer: [null, Validators.required],
      orderNumber: [null, Validators.required],
      cuttingDate: [null],
      preparationDate: [null],
      bendingDate: [null],
      assemblyDate: [null],
    });
  }

  addOrder() {
    if (!this.orderForm.valid) {
      this.clearOrderForm();
    }

    const payload = Object.assign({}, this.orderForm.getRawValue()) as Order;
    this.orderClient
      .create(payload)
      .subscribe(() => this.addOrder$.next(payload));

    this.clearOrderForm();
  }

  searchCustomerKeyUp() {
    this.searchFilter$.next({ ...this.searchFilter$.value, customer: this.searchCustomer })
  }

  searchOrderKeyUp() {
    this.searchFilter$.next({ ...this.searchFilter$.value, orderNumber: this.searchOrder })
  }
}