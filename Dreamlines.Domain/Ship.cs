using System;
using System.Collections.Generic;
using System.Text;

namespace Dreamlines.Domain
{
    public partial class Ship : BaseEntity
    {
        private ICollection<Booking> _bookings;

        public int SalesUnitId { get; set; }
        public string Name { get; set; }

        public virtual SalesUnit SalesUnit { get; set; }
        public virtual ICollection<Booking> Bookings
        {
            get => _bookings ?? (_bookings = new List<Booking>());
            protected set => _bookings = value;
        }
    }
}
