import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { Product } from '../Product';
// import { Todo } from '../models/todo.model';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private connection: HubConnection;
  itemUpdated: Subject<Product> = new Subject<Product>();
  itemAdded: Subject<Product> = new Subject<Product>();

  constructor() {
    this.connection = new HubConnectionBuilder()
      .withUrl('https://localhost:44353/hub/getstocks')
      .build();
    this.registerOnEvents();
    this.connection.start().catch(err => console.log(err.toString()));
  }

  registerOnEvents() {
    this.connection.on('SendStocksToUser', item => {
      console.log('SendStocksToUser');
      console.log(item);
      this.itemAdded.next(item);
    });

    // this.connection.on('itemUpdated', item => {
    //   console.log('itemUpdated');
    //   this.itemUpdated.next(item);
    // });
  }
}
