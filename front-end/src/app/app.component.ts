import { Component } from '@angular/core';
import { combineLatest, debounceTime, Observable, Observer, switchMap } from 'rxjs';
import { Order, OrderClient } from './api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  searchCustomer: string = "";
  searchOrder: string = "";
  columnsToDisplay = ['id', 'customer', 'orderNumber', 'cuttingDate', 'preparationDate', 'bendingDate', 'assemblyDate'];
  customer: string = "";
  orderNumber: string = "";
  cuttingDate!: Date;
  preparationDate!: Date;
  bendingDate!: Date;
  assemblyDate!: Date;

  addOrderObserver!: Observer<Order>;
  addOrder$: Observable<Order> = new Observable(observer => {
    this.addOrderObserver = observer;
    observer.next()
  });
  customerFilterObserver!: Observer<string>;
  customerFilter$: Observable<string> = new Observable(observer => {
    this.customerFilterObserver = observer;
    observer.next()
  })
  orderFilterObserver!: Observer<string>;
  orderFilter$: Observable<string> = new Observable(observer => {
    this.orderFilterObserver = observer;
    observer.next()
  })
  order$ = combineLatest([this.customerFilter$, this.orderFilter$, this.addOrder$]).pipe(
    debounceTime(200),
    switchMap(([customer, order]) => this.orderClient.list(customer ?? undefined, order ?? undefined))
  )

  constructor(private orderClient: OrderClient) { }

  addOrder() {
    let order = {
      customer: this.customer,
      orderNumber: this.orderNumber,
      cuttingDate: this.cuttingDate,
      preparationDate: this.preparationDate,
      bendingDate: this.bendingDate,
      assemblyDate: this.assemblyDate,
    }

    if (order.customer != "" &&
      order.orderNumber != "" &&
      order.cuttingDate != null &&
      order.preparationDate != null &&
      order.bendingDate != null &&
      order.assemblyDate != null) {
      this.orderClient.create(order).subscribe(() => {
        this.addOrderObserver.next(order);
      });
    }
  }
  searchCustomerKeyUp() {
    this.customerFilterObserver.next(this.searchCustomer)
  }
  searchOrderKeyUp() {
    this.orderFilterObserver.next(this.searchOrder)
  }
}