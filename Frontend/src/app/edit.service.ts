// import { Injectable } from '@angular/core';
// import { Observable, BehaviorSubject, observable } from 'rxjs';
// import { HttpClient, HttpHeaders } from '@angular/common/http';
// import {  Client } from './model';
// import { tap, map } from 'rxjs/operators';
// import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';


// const CREATE_ACTION = 'addClient';
// const UPDATE_ACTION = 'updateClient';

// @Injectable()
// export class EditService extends BehaviorSubject<Client[]> {
//     constructor(private http: HttpClient) {
//         super([]);
//     }

//     private data: Client[]= [];

//     public read(): void {
//         if (this.data.length) {
//             return super.next(this.data);
//         }

//         this.fetch()
//             .pipe(
//                 tap(data => {
//                     this.data = data;
//                 })
//             )
//             .subscribe(data => {
//                 super.next(data);
//             });
//     }

//     public save(data: Client[], isNew?: boolean): void {
//         const action = isNew ? CREATE_ACTION : UPDATE_ACTION;

//         this.reset();

//         this.update( data,action)
//             .subscribe(() => this.read(), () => this.read());
//     }

//     public remove(data: Client []): void {
//         this.reset();

//         this.delete(data)
//             .subscribe(() => this.read(), () => this.read());
//     }

//     public resetItem(dataItem: Client): void {
//         if (!dataItem) { return; }

//         // find orignal data item
//         const originalDataItem = this.data.find(item => item.id === dataItem.id);

//         // revert changes
//         Object.assign(originalDataItem, dataItem);

//         super.next(this.data);
//     }

//     private reset() {
//         this.data = [];
//     }

//     private fetch(action = "", data?: Client[]): Observable<Client[]> {
//         var resp= this.http
//           .get<Client[]>(
//                 `https://localhost:44353/stocknotifier/getClients`)

//           .pipe(map((res: Object) => <Client[]>res));
//           console.log(resp);
//           return resp;
//       }
    

//     private update(data?:Client[],action=""):Observable<Client[]> {
       
//         let body = JSON.stringify(data);
//          let headers = new HttpHeaders({'Content-Type': 'application/json'});
//         const  options ={
//                 headers:headers
//       }
//       const res=  this.http.post<any>(`https://localhost:44353/stocknotifier/${action}`,body   ,options)  
//         .pipe(map((res: Object) => <Client[]>res));
//         return res;
//     }


//     private delete(data?:Client[]):Observable<Client[]> {
        
//         console.log(data)
//         let body = JSON.stringify(data);
//          let headers = new HttpHeaders({'Content-Type': 'application/json'});
//         const  options ={
//                 headers:headers
//       }
    
   
//     const res=  this.http.post(`https://localhost:44353/stocknotifier/Deleteclient`,body   ,options)  
//     .pipe(map((res: Object) => <Client[]>res));
//         return res;
//     }
   
// }
