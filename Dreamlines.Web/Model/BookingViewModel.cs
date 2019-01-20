using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dreamlines.Web.Model
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public string ShipName { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}
