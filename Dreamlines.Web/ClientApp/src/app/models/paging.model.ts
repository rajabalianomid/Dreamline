export class Paging<T> {
  public next: boolean;
  public previous: boolean;
  public total: number;

  constructor(public data: T, public pageIndex: number = 0, public pageSize: number = 10) { }

  public preparePageSize(newItem: Pagination<SaleUnitBookingResponse>, oldItem: Paging<T>) {
    oldItem.previous = newItem.previous;
    oldItem.next = newItem.next;
    oldItem.total = newItem.count;
    oldItem.pageIndex = newItem.pageIndex;
  }
}
