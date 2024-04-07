import { Observable } from 'rxjs';
import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { GridComponent, GridDataResult, CancelEvent, EditEvent, RemoveEvent, SaveEvent, AddEvent } from '@progress/kendo-angular-grid';
import { State, process } from '@progress/kendo-data-query';
import { Client } from './model';
// import { EditService } from './edit.service';
import { map } from 'rxjs/operators';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

@Component({
    selector: 'my-app',
    template: `
             
        <kendo-grid
            [filterable]="true"
            [data]="view | async"
            [pageSize]="gridState.take"
            [skip]="gridState.skip"
            [sort]="gridState.sort"
            [pageable]="true"
            [sortable]="true"
            (dataStateChange)="onStateChange($event)"
            (edit)="editHandler($event)"
            (cancel)="cancelHandler($event)"
            (save)="saveHandler($event)"
            (remove)="removeHandler($event)"
            (add)="addHandler($event)"
            [navigable]="true"
        >
         
            <kendo-grid-column field="id" title="Client Id"></kendo-grid-column>
            <kendo-grid-column  [filterable]="true" field="v" title=""></kendo-grid-column>
            <kendo-grid-column  [filterable]="true"field=""  title=""></kendo-grid-column>
            <kendo-grid-column  [filterable]="true"field="" title=""></kendo-grid-column>
            <kendo-grid-column  [filterable]="true"  filter="boolean" field=""  title=""></kendo-grid-column>
            <kendo-grid-column  [filterable]="true" filter= "date"  format= "d" field=""  title=" "></kendo-grid-column>
            <kendo-grid-command-column title="Actions" [width]="220">
                <ng-template kendoGridCellTemplate let-isNew="isNew">
                    <button kendoGridEditCommand [primary]="true">Edit</button>
                    <button kendoGridRemoveCommand>Remove</button>
                    <button kendoGridSaveCommand [disabled]="formGroup?.invalid">{{ isNew ? 'Add' : 'Update' }}</button>
                    <button kendoGridCancelCommand>{{ isNew ? 'Discard changes' : 'Cancel' }}</button>
                </ng-template>
            </kendo-grid-command-column>
        </kendo-grid>
    `
})
export class AppComponent implements OnInit {
    public view: Observable<GridDataResult> | undefined;
    public gridState: State = {
        sort: [],
        skip: 0,
        take: 5
    };
    public formGroup: FormGroup;
    private hubConnectionBuilder!: HubConnection;
    stocks  : Client[] = [];

    private editedRowIndex: number;

    constructor() {
    }
    public ngOnInit(): void {
        this.hubConnectionBuilder = new HubConnectionBuilder().withUrl('https://localhost:44353/GetStocks').configureLogging(LogLevel.Information).build();
        this.hubConnectionBuilder.start().then(() => console.log('Connection started.......!')).catch(err => console.log('Error while connect with server'));
        this.hubConnectionBuilder.on('SendStocksToUser', (result: any) => {
        this.stocks.push(result);
          
        this.view=result;
        // this.view = this.editService.pipe(map((data) => process(data, this.gridState)));
     
        // this.editService.read();
     })
     ;
     console.log(this.stocks);

    }

    public onStateChange(state: State): void {
        this.gridState = state;
        this.hubConnectionBuilder = new HubConnectionBuilder().withUrl('https://localhost:44353/GetStocks').configureLogging(LogLevel.Information).build();
        this.hubConnectionBuilder.start().then(() => console.log('Connection started.......!')).catch(err => console.log('Error while connect with server'));
        this.hubConnectionBuilder.on('SendStocksToUser', (result: any) => {
            this.stocks.push(result);
            console.log(this.stocks);

        this.gridState=result
        this.view=result

        // this.view = this.editService.pipe(map((data) => process(data, this.gridState)));
     
        // this.editService.read();
     });
     console.log(this.stocks);

        // this.editService.read();
    }

    public addHandler(args: AddEvent): void {
        this.closeEditor(args.sender);
        this.formGroup = new FormGroup({
            id: new FormControl('', Validators.compose([Validators.required])),
            v    : new FormControl('',Validators.compose([Validators.required])),
            vw: new FormControl('',Validators.compose([Validators.required])),
            h: new FormControl('',),
            ticker: new FormControl('',),
          
        });
        args.sender.addRow(this.formGroup);
    }

    public editHandler(args: EditEvent): void {
        
        const { dataItem } = args;  
        this.closeEditor(args.sender);

        this.formGroup = new FormGroup({
            id: new FormControl(dataItem.id),
            v: new FormControl(dataItem.v, Validators.compose([Validators.required])),
            vw    : new FormControl(dataItem.vw,Validators.compose([Validators.required])),
            h: new FormControl(dataItem.h,Validators.compose([Validators.required])),
            ticker: new FormControl(dataItem.ticker,),
            0: new FormControl(dataItem.o,),

          
        });

        this.editedRowIndex = args.rowIndex;
        args.sender.editRow(args.rowIndex, this.formGroup);
    }

    public cancelHandler(args: CancelEvent): void {
        this.closeEditor(args.sender, args.rowIndex);
    }

    public saveHandler({sender, rowIndex, formGroup, isNew}: SaveEvent): void {
        const product: Client[] = formGroup.value;

        // this.editService.save(product, isNew);

        sender.closeRow(rowIndex);
    }

    public removeHandler(args: RemoveEvent): void {
      
        // this.editService.remove(args.dataItem);
    }

    private closeEditor(grid: GridComponent, rowIndex = this.editedRowIndex) {
        // close the editor
        grid.closeRow(rowIndex);
        // reset the helpers
        this.editedRowIndex = undefined;
        this.formGroup = undefined;
    }
}