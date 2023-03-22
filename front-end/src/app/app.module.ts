import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { API_BASE_URL } from './api.service';
import {MAT_DATE_LOCALE} from '@angular/material/core';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
//add angular material
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { AppOrderFormComponent } from './app-order-form/app-order-form.component';
import { AppOrderTableComponent } from './app-order-table/app-order-table.component';


@NgModule({
  declarations: [
    AppComponent,
    AppOrderFormComponent,
    AppOrderTableComponent,
  ],
  imports: [
    BrowserModule, HttpClientModule, BrowserAnimationsModule, MatSlideToggleModule, MatTableModule, MatPaginatorModule, MatInputModule, MatButtonModule, MatFormFieldModule, FormsModule, ReactiveFormsModule, MatDatepickerModule, MatNativeDateModule, MatNativeDateModule, MatIconModule
  ],
  providers: [
    { provide: API_BASE_URL, useValue: "http://localhost:5181" },
    { provide: MAT_DATE_LOCALE, useValue: 'af' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

