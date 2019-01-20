using System;
using System.Collections.Generic;
using System.Text;

namespace Dreamlines.Domain
{
    public partial class SalesUnit : BaseEntity
    {
        private ICollection<Ship> _ships;

        public string Name { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }

        //public virtual Country Country { get; set; }
        public virtual ICollection<Ship> Ships
        {
            get => _ships ?? (_ships = new List<Ship>());
            protected set => _ships = value;
        }
    }
}
