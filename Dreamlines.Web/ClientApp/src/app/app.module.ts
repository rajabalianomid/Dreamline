import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { SalesunitComponent } from './salesunitbooking/salesunit/salesunit.component';
import { BookingComponent } from './salesunitbooking/booking/booking.component';
import { SalesunitbookingComponent } from './salesunitbooking/salesunitbooking.component';
import { AboutmeComponent } from './aboutme/aboutme.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    SalesunitComponent,
    BookingComponent,
    SalesunitbookingComponent,
    AboutmeComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: SalesunitbookingComponent, pathMatch: 'full' },
      { path: 'aboutme', component: AboutmeComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
