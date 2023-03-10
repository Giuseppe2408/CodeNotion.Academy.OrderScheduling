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
  constructor(private orderClient: OrderClient) {
    orderClient.list().subscribe((list: Order[]) => this.order_list = list)
  }
  columnsToDisplay = ['id', 'customer', 'orderNumber', 'preparationDate', 'bendingDate', 'assemblyDate'];
}
