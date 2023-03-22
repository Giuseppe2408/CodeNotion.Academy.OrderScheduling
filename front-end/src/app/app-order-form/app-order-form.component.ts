import { Component, Input, } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { Order, OrderClient } from '../api.service';
import { serializeDateOnly } from '../dateonly.utils';
import {  Observable, BehaviorSubject, map, Subject, } from 'rxjs';

@Component({
  selector: 'app-order-form',
  templateUrl: './app-order-form.component.html',
  styleUrls: ['./app-order-form.component.scss']
})
export class AppOrderFormComponent {
  @Input() order!: Subject<Order | null>;
  @Input() addOrder$ = new BehaviorSubject<Order | null>(null);
  @Input() updateOrder$ = new BehaviorSubject<Order | null>(null);
  // @Output() onOrderUpdated = new EventEmitter<Order>();
  orderForm: Observable<FormGroup | null> | null = null;

  constructor(private fb: FormBuilder, private orderClient: OrderClient) {
  }

  ngOnChanges(): void {
    this.orderForm = this.order.pipe(
      map(order => this.buildForm(order!)));
  }

  buildForm(order: Order | null) : FormGroup {
    return this.fb.group({
      id: [order?.id ?? 0, Validators.required],
      customer: [order?.customer, Validators.required],
      orderNumber: [order?.orderNumber, Validators.required],
      cuttingDate: [order?.cuttingDate],
      preparationDate: [order?.preparationDate],
      bendingDate: [order?.bendingDate],
      assemblyDate: [order?.assemblyDate],
    });
  }

  FormOrder(orderForm: FormGroup) {
    if (!orderForm.valid) {
      return;
    }

    const payload = Object.assign({}, orderForm.getRawValue()) as Order;
    payload.cuttingDate = serializeDateOnly(payload.cuttingDate);
    payload.preparationDate = serializeDateOnly(payload.preparationDate);
    payload.bendingDate = serializeDateOnly(payload.bendingDate);
    payload.assemblyDate = serializeDateOnly(payload.assemblyDate);

    if (payload.id) {
      this.orderClient
        .update(payload)
        .subscribe(() => this.updateOrder$.next(payload));     
        this.order.next(null); 
      return;
    }

    this.orderClient
      .create(payload)
      .subscribe(() => this.addOrder$.next(payload));   
      this.order.next(null);
  }
}
