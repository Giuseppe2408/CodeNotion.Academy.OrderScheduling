import { Component, Input, } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { Order, OrderClient } from '../api.service';
import { serializeDateOnly } from '../dateonly.utils';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-order-form',
  templateUrl: './app-order-form.component.html',
  styleUrls: ['./app-order-form.component.scss']
})
export class AppOrderFormComponent {
  @Input() order! : Order;
  @Input() updateOrder$ = new BehaviorSubject<Order | null>(null);
  @Input() addOrder$ = new BehaviorSubject<Order | null>(null);

  constructor(private fb : FormBuilder,private orderClient: OrderClient) {
    this.clearOrderForm();
  }

  orderForm! : FormGroup;

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

  getOrder(order: Order) {
    if (!order.id) {
      return
    }
    this.orderForm.setValue({ ...order })
  }

  FormOrder() {
    const payload = Object.assign({}, this.orderForm.getRawValue()) as Order;
    payload.cuttingDate = serializeDateOnly(payload.cuttingDate);
    payload.preparationDate = serializeDateOnly(payload.preparationDate);
    payload.bendingDate = serializeDateOnly(payload.bendingDate);
    payload.assemblyDate = serializeDateOnly(payload.assemblyDate);

    if (payload.id) {
      if (!this.orderForm.valid) {
        this.clearOrderForm();
        return;
      }
      this.orderClient
      .update(payload)
      .subscribe(() => this.updateOrder$.next(payload))
      this.clearOrderForm();
      return;
    }

    this.orderClient
      .create(payload)
      .subscribe(() => this.addOrder$.next(payload));
    this.clearOrderForm();
  }
}
