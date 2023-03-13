import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule, //HttpClientModule
  ],
  providers: [//{ provide: API_BASE_URL, useValue: "http://localhost:5181" }
],
  bootstrap: [AppComponent]
})
export class AppModule { }
