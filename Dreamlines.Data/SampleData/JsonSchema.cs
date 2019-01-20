using Dreamlines.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dreamlines.Data.SampleData
{
    internal class JsonSchema
    {
        public SalesUnit[] SalesUnits { get; set; }
        public Ship[] Ships { get; set; }
        public Booking[] Bookings { get; set; }
    }
}
