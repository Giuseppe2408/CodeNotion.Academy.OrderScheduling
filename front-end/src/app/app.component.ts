import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject, combineLatest, debounceTime, switchMap } from 'rxjs';
import { Order, OrderClient } from './api.service';
import { serializeDateOnly } from './dateonly.utils';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  searchCustomer: string = "";
  searchOrder: string = "";
  columnsToDisplay = ['id', 'customer', 'orderNumber', 'cuttingDate', 'preparationDate', 'bendingDate', 'assemblyDate', 'action'];
  orderForm!: FormGroup;
  orderId!: number;

  searchFilter$ = new BehaviorSubject<{ customer?: string; orderNumber?: string }>({});
  addOrder$ = new BehaviorSubject<Order | null>(null);
  updateOrder$ = new BehaviorSubject<Order | null>(null);
  deleteOrder$ = new BehaviorSubject<Order | null>(null);
  order$ = combineLatest([this.searchFilter$, this.addOrder$, this.updateOrder$, this.deleteOrder$]).pipe(
    debounceTime(200),
    switchMap(([filter]) => this.orderClient.list(filter.customer, filter.orderNumber))
  )

  constructor(private fb: FormBuilder, private orderClient: OrderClient) {
    this.clearOrderForm()
  }

  clearOrderForm() {
    this.orderForm = this.fb.group({
      id: [0, Validators.required],
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
    payload.cuttingDate = serializeDateOnly(payload.cuttingDate);
    payload.preparationDate = serializeDateOnly(payload.preparationDate);
    payload.bendingDate = serializeDateOnly(payload.bendingDate);
    payload.assemblyDate = serializeDateOnly(payload.assemblyDate);
    this.orderClient
      .create(payload)
      .subscribe(() => this.addOrder$.next(payload));

    this.clearOrderForm();
  }

  formFill(order: Order) {
    this.orderForm.setValue({ ...order })
    this.orderId = order.id ?? 0;
  }

  updateOrder() {
    if (!this.orderForm.valid) {
      this.clearOrderForm();
    }

    const payload = Object.assign({}, this.orderForm.getRawValue()) as Order;
    payload.cuttingDate = serializeDateOnly(payload.cuttingDate);
    payload.preparationDate = serializeDateOnly(payload.preparationDate);
    payload.bendingDate = serializeDateOnly(payload.bendingDate);
    payload.assemblyDate = serializeDateOnly(payload.assemblyDate);
    this.orderClient
      .update(payload)
      .subscribe(() => this.updateOrder$.next(payload))
  }

  deleteOrder(order: Order) {
    if (!order?.id || order?.id === 0) {
      return;
    }

    this.orderClient
      .delete(order.id)
      .subscribe(() => this.deleteOrder$.next(order))
  }

  onSubmit() {
    if (this.orderId) {
      this.updateOrder()
      this.clearOrderForm()
      this.orderId = 0;
      return;
    }

    this.addOrder()
    this.clearOrderForm()
  }

  searchCustomerKeyUp() {
    this.searchFilter$.next({ ...this.searchFilter$.value, customer: this.searchCustomer })
  }

  searchOrderKeyUp() {
    this.searchFilter$.next({ ...this.searchFilter$.value, orderNumber: this.searchOrder })
  }
}