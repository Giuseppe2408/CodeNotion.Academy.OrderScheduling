import { Component, ViewChild } from '@angular/core';
import { BehaviorSubject, combineLatest, debounceTime, switchMap } from 'rxjs';
import { Order, OrderClient } from './api.service';
import { AppOrderFormComponent } from './app-order-form/app-order-form.component';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})

export class AppComponent {
  searchCustomer: string = "";
  searchOrder: string = "";
  columnsToDisplay = ['id', 'customer', 'orderNumber', 'cuttingDate', 'preparationDate', 'bendingDate', 'assemblyDate', 'action'];

  searchFilter$ = new BehaviorSubject<{ customer?: string; orderNumber?: string }>({});
  addOrder$ = new BehaviorSubject<Order | null>(null);
  updateOrder$ = new BehaviorSubject<Order | null>(null);
  deleteOrder$ = new BehaviorSubject<Order | null>(null);
  order$ = combineLatest([this.searchFilter$, this.addOrder$, this.updateOrder$, this.deleteOrder$]).pipe(
    debounceTime(200),
    switchMap(([filter]) => this.orderClient.list(filter.customer, filter.orderNumber))
  )

  constructor(private orderClient: OrderClient) { }

  @ViewChild(AppOrderFormComponent) childComponent!: AppOrderFormComponent;
  triggerChildFunction(row: Order) {
    this.childComponent.getOrder(row);
  }

  deleteOrder(event : Event, order: Order) {
    if (!order?.id || order?.id === 0) {
      return;
    }
    event.stopPropagation();
    this.orderClient
      .delete(order.id)
      .subscribe(() => this.deleteOrder$.next(order))
    
    this.childComponent.clearOrderForm();
  }

  searchCustomerKeyUp() {
    this.searchFilter$.next({ ...this.searchFilter$.value, customer: this.searchCustomer })
  }

  searchOrderKeyUp() {
    this.searchFilter$.next({ ...this.searchFilter$.value, orderNumber: this.searchOrder })
  }
}