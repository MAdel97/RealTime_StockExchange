import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClient, HttpClientModule, HttpClientJsonpModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { GridModule } from '@progress/kendo-angular-grid';

import { AppComponent } from './app.component';
import { EditService } from './edit.service';
import { SignalRService } from './_services/signalr.service';
import { AppRoutingModule } from './app-routing.module';
// import { LoginComponent } from './login/login.component';
// import { RegisterComponent } from './register/register.component';
// import { ProfileComponent } from './profile/profile.component';
import { OrderComponent } from './OrdersForm/orders.component';
// import { EditService } from './edit.service';

@NgModule({
    declarations: [
        AppComponent,
        // LoginComponent,
        // RegisterComponent,
        // ProfileComponent,
        OrderComponent
    ],
    imports: [
        FormsModule,
        HttpClientModule,
        HttpClientJsonpModule,
        AppRoutingModule,
        BrowserModule,
        BrowserAnimationsModule,
        ReactiveFormsModule,
        GridModule
    ],
    providers: [
        {   
            deps: [HttpClient],
            provide: EditService,
            useFactory: (jsonp: HttpClient) => () => new EditService(jsonp)
        }
    ],
    bootstrap: [AppComponent]
})
export class AppModule {}

