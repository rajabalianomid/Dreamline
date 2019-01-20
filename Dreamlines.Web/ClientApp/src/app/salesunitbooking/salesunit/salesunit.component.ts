import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { SalesunitService } from '../../services/salesunit.service';
import { Paging } from '../../models/paging.model';
import { BookingSearch } from '../../models/bookingsearch.model';


@Component({
  selector: 'app-salesunit',
  templateUrl: './salesunit.component.html',
  styleUrls: ['./salesunit.component.css']
})
export class SalesunitComponent implements OnInit {

  public salesunitsResponse: SalesUnitResponse[];
  private saleUnitBookingResponse: Pagination<SaleUnitBookingResponse>;
  @Output() bookingEvent = new EventEmitter<Pagination<SaleUnitBookingResponse>>();

  constructor(private salesUnitService: SalesunitService) {

  }

  ngOnInit() {
    this.salesUnitService.SalesunitServiceHttp().subscribe(result => {
      this.salesunitsResponse = result;
    }, error => console.error(error));
  }

  onShowBookings(id: number) {
    this.salesUnitService.BookingBySaleUnitIdHttp(new Paging<BookingSearch>(new BookingSearch(id))).subscribe(result => {
      this.saleUnitBookingResponse = result;
      this.salesUnitService.paging.preparePageSize(this.saleUnitBookingResponse, this.salesUnitService.paging);
      this.bookingEvent.emit(this.saleUnitBookingResponse);
    }, error => console.error(error));
  }
}
