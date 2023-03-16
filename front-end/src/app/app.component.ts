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
  columnsToDisplay = ['id', 'customer', 'orderNumber', 'preparationDate', 'bendingDate', 'assemblyDate'];

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
  order$ = combineLatest([this.customerFilter$, this.orderFilter$]).pipe(
    debounceTime(300),
    switchMap(([customer, order]) => this.orderClient.list(customer ?? undefined, order ?? undefined))
  )

  constructor(private orderClient: OrderClient) {}

  searchCustomerKeyUp() {
    this.customerFilterObserver.next(this.searchCustomer)
  }
  searchOrderKeyUp() {
    this.orderFilterObserver.next(this.searchOrder)
  }
}