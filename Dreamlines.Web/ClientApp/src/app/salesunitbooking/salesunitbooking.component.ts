import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-salesunitbooking',
  templateUrl: './salesunitbooking.component.html',
  styleUrls: ['./salesunitbooking.component.css']
})
export class SalesunitbookingComponent implements OnInit {

  selectedSaleUnitBookingResponse: Pagination<SaleUnitBookingResponse>;

  constructor() { }

  ngOnInit() {
  }
}
