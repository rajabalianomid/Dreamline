using System;
using System.Collections.Generic;
using System.Text;

namespace Dreamlines.Domain.Search
{
    public class BookingSearch
    {
        public int Id { get; set; }
        public List<int> BookingIds { get; set; }
        public string ShipNames { get; set; }
    }
}
