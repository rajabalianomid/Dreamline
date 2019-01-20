using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dreamlines.Web.Model
{
    public class BookingSearchViewModel
    {
        public int Id { get; set; }
        public string BookingId { get; set; }
        public List<int> BookingIds => string.IsNullOrEmpty(BookingId) ?
            new List<int>() :
            BookingId.Split(';').Select(s =>
            {
                int result;
                bool isNumeric = int.TryParse(s, out result);
                return result;
            }).ToList();
        public string ShipNames { get; set; }
    }
}
