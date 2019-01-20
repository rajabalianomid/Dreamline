using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dreamlines.Web.Model
{
    public class SaleUnitBookingViewModel
    {
        public IEnumerable<BookingViewModel> Bookings { get; set; }
        public SalesUnitViewModel SalesUnit { get; set; }
    }
}
