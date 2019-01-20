using Dreamlines.Common;
using Dreamlines.Common.Domain;
using Dreamlines.Domain;
using Dreamlines.Domain.Search;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dreamlines.Service
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> _repositoryBooking;
        private readonly IRepository<SalesUnit> _repositorySalesUnit;
        public BookingService(IRepository<Booking> repositoryBooking, IRepository<SalesUnit> repositorySalesUnit)
        {
            this._repositoryBooking = repositoryBooking;
            this._repositorySalesUnit = repositorySalesUnit;
        }

        public Dictionary<int, decimal> GetTotalPriceBySalesUnitIds(IEnumerable<int> ids)
        {
            var result = new Dictionary<int, decimal>();
            _repositoryBooking.Table.Select(s => new { s.Ship.SalesUnitId, s.Price }).Where(w => ids.Any(a => a == w.SalesUnitId))
                .GroupBy(g => g.SalesUnitId)
                .Select(s => new { Id = s.Key, Sum = s.Sum(su => su.Price) })
                .ToList()
                .ForEach(f => result.Add(f.Id, f.Sum));
            return result;
        }

        public async Task<int> CountBookingsBySearch(Pagination<BookingSearch> request)
        {
            return await QueryBookingSearch(request).CountAsync();
        }

        public async Task<Pagination<List<Booking>>> GetBookingsBySearch(Pagination<BookingSearch> request)
        {
            var result = await QueryBookingSearch(request).ToPaging(request.PageIndex, request.PageSize).ToListAsync();
            var count = await CountBookingsBySearch(request);
            return new Pagination<List<Booking>>(
                request.PageIndex, request.PageSize, count, result);
        }

        private IQueryable<Booking> QueryBookingSearch(Pagination<BookingSearch> request)
        {
            var query = _repositoryBooking.Table.Where(w => w.Ship.SalesUnitId == request.Data.Id);
            if (request.Data.BookingIds != null && request.Data.BookingIds.Any())
            {
                query = query.Where(w => request.Data.BookingIds.Any(a => a == w.Id));
            }
            if (request.Data.ShipNames != null)
            {
                query = query.Where(w => w.Ship.Name.Contains(request.Data.ShipNames));
            }
            return query;
        }
    }
}
