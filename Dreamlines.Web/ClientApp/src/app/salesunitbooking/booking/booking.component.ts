import { Component, OnInit, Input } from '@angular/core';
import { SalesunitService } from '../../services/salesunit.service';
import { BookingSearch } from '../../models/bookingsearch.model';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent implements OnInit {

  @Input() private saleUnitbookingResponse: Pagination<SaleUnitBookingResponse>;
  bookingSearch = new BookingSearch();

  constructor(private salesunitService: SalesunitService) {
  }

  ngOnInit() {
  }

  onClickNext() {
    var paging = this.salesunitService.paging;
    if (paging.next) {
      paging.pageIndex = paging.pageIndex + 1;
      this.sendHttpRequest();
    }
  }

  onClickPrevoius() {
    var paging = this.salesunitService.paging;
    if (paging.previous) {
      paging.pageIndex = paging.pageIndex - 1;
      this.sendHttpRequest();
    }
  }

  onClickSearch() {
    var paging = this.salesunitService.paging;
    paging.pageIndex = 0;
    paging.data.bookingId = this.bookingSearch.bookingId == "" ? null : this.bookingSearch.bookingId;
    paging.data.shipNames = this.bookingSearch.shipNames == "" ? null : this.bookingSearch.shipNames;
    this.sendHttpRequest();
  }

  sendHttpRequest() {
    var paging = this.salesunitService.paging;
    this.salesunitService.BookingBySaleUnitIdHttp(paging).subscribe(result => {
      this.saleUnitbookingResponse = result;
      paging.preparePageSize(this.saleUnitbookingResponse, paging);
    }, error => console.error(error));
  }
}
