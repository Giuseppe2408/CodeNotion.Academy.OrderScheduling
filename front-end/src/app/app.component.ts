import { Component } from '@angular/core';
import { Order, OrderClient } from './api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'order-scheduling-angular';
  order_list: Order[] = [];
  filter_list: Order[] = [];
  inputValueCustomer: string | undefined;
  inputValueOrder: string | undefined;
  data: Order[] = [];
  columnsToDisplay = ['id', 'customer', 'orderNumber', 'preparationDate', 'bendingDate', 'assemblyDate'];

  constructor(private orderClient: OrderClient) {
    orderClient.list().subscribe((list: Order[]) => this.order_list = list)
    orderClient.list().subscribe((list: Order[]) => this.filter_list = list)
  }

  applyFilterCustomer(event: Event) {
    this.orderClient
      .list(this.inputValueCustomer)
      .subscribe(res => this.filter_list = res)
  }

  applyFilterOrder(event: Event) {
    this.orderClient
      .list(undefined, this.inputValueOrder)
      .subscribe(res => this.filter_list = res);
  }
}