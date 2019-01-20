using System;
using System.Collections.Generic;
using System.Text;

namespace Dreamlines.Domain
{
    public partial class Booking : BaseEntity
    {
        public int ShipId { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal Price { get; set; }

        public virtual Ship Ship { get; set; }
    }
}
