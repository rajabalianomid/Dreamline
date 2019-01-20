interface Pagination<T> {
  pageIndex: number;
  pageSize: number;
  count: number;
  pageCount: number;
  next: boolean;
  previous: boolean;
  data: T;
}
