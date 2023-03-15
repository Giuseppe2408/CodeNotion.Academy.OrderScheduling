import { HttpClient } from '@angular/common/http';
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
  inputValueCustomer: any;
  inputValueOrder: any;
  data: Order[] = [];
  constructor(private orderClient: OrderClient, private httpClient: HttpClient) {
    orderClient.list().subscribe((list: Order[]) => this.order_list = list)
    orderClient.list().subscribe((list: Order[]) => this.filter_list = list)
  }

  columnsToDisplay = ['id', 'customer', 'orderNumber', 'preparationDate', 'bendingDate', 'assemblyDate'];

  applyFilterCustomer(event: Event) {

    this.httpClient.get("http://localhost:5181/api/Order/List", {
      params: {
        customer: this.inputValueCustomer
      },
    }).subscribe(res => {

      this.filter_list = Object.values(res)
      this.filter_list = this.order_list.filter(or => or.customer?.includes(this.inputValueCustomer));

    })

  }

  applyFilterOrder(event: Event) {

    this.httpClient.get("http://localhost:5181/api/Order/List", {
      params: {
        orderNumber: this.inputValueOrder
      },
    }).subscribe(res => {

      this.filter_list = Object.values(res)
      this.filter_list = this.order_list.filter(or => or.orderNumber?.includes(this.inputValueOrder));

    })

  } 

}







