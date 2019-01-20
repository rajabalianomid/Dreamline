using Dreamlines.Common.Domain;
using Dreamlines.Domain;
using Dreamlines.Domain.Search;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dreamlines.Service
{
    public interface IBookingService
    {
        Task<int> CountBookingsBySearch(Pagination<BookingSearch> request);
        Dictionary<int, decimal> GetTotalPriceBySalesUnitIds(IEnumerable<int> ids);
        Task<Pagination<List<Booking>>> GetBookingsBySearch(Pagination<BookingSearch> request);
    }
}
