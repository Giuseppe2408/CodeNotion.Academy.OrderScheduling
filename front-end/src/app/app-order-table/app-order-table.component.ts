import { Component, Input } from '@angular/core';
import { BehaviorSubject, combineLatest, debounceTime, Observable, switchMap } from 'rxjs';
import { Order, OrderClient } from '../api.service';


@Component({
  selector: 'app-order-table',
  templateUrl: './app-order-table.component.html',
  styleUrls: ['./app-order-table.component.scss']
})
export class AppOrderTableComponent {
  @Input() focusedOrder!: BehaviorSubject<Order | null>;
  @Input() addOrder$!: BehaviorSubject<Order | null>;
  @Input() updateOrder$!: BehaviorSubject<Order | null>;

  deleteOrder$ = new BehaviorSubject<Order | null>(null);
  searchCustomer: string = "";
  searchOrder: string = "";
  columnsToDisplay = ['id', 'customer', 'orderNumber', 'cuttingDate', 'preparationDate', 'bendingDate', 'assemblyDate', 'action'];
  order$!: Observable<Order[]>;
  searchFilter$ = new BehaviorSubject<{ customer?: string; orderNumber?: string }>({});

  ngOnChanges() {
    this.order$ = combineLatest([this.searchFilter$, this.addOrder$, this.updateOrder$, this.deleteOrder$]).pipe(
      debounceTime(200),
      switchMap(([filter]) => this.orderClient.list(filter.customer, filter.orderNumber))
    )
  }

  constructor(private orderClient: OrderClient) { }

  deleteOrder(event: Event, order: Order) {
    event.stopPropagation();
    if (!order?.id || order?.id === 0) {
      return;
    }

    this.orderClient
      .delete(order.id)
      .subscribe(() => this.deleteOrder$.next(order))

    this.focusedOrder.next(null);
  }

  searchCustomerKeyUp() {
    this.searchFilter$.next({ ...this.searchFilter$.value, customer: this.searchCustomer })
  }

  searchOrderKeyUp() {
    this.searchFilter$.next({ ...this.searchFilter$.value, orderNumber: this.searchOrder })
  }
}
