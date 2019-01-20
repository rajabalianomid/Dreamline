interface BookingResponse {
  id: number;
  shipName: string;
  price: number;
  currency: string;
}
interface SaleUnitBookingResponse {
  bookings: BookingResponse[];
  salesUnit: SalesUnitResponse;
}
