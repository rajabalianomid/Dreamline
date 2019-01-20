import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Paging } from '../models/paging.model';
import { BookingSearch } from '../models/bookingsearch.model';

@Injectable({
  providedIn: 'root'
})
export class SalesunitService {
  private salesUnitObserve: Observable<SalesUnitResponse[]>;
  public paging: Paging<BookingSearch>;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.salesUnitObserve = http.get<SalesUnitResponse[]>(baseUrl + 'api/v1/SalesUnit/GetAll');
  }

  SalesunitServiceHttp() {
    console.log('http');
    return this.salesUnitObserve;
  }

  BookingBySaleUnitIdHttp(item: Paging<BookingSearch>) {
    this.paging = item;
    return this.http.post<Pagination<SaleUnitBookingResponse>>(this.baseUrl + 'api/v1/Booking/GetAll', { 'data': item.data, 'pageIndex': item.pageIndex, 'pageSize': item.pageSize });
  }
}
