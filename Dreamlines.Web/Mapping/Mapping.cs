using AutoMapper;
using Dreamlines.Domain;
using Dreamlines.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dreamlines.Web.Mapping
{
    public static class Mapping
    {
        #region Booking

        public static IMappingExpression<Booking, BookingViewModel> Map(this IMappingExpression<Booking, BookingViewModel> config)
        {
            return config.ForMember(s => s.ShipName, d => d.MapFrom(f => f.Ship.Name))
                  .ForMember(s => s.Currency, d => d.MapFrom(f => f.Ship.SalesUnit.Currency));
        }

        #endregion

        #region SalesUnit

        public static IMappingExpression<SalesUnit, SalesUnitViewModel> Map(this IMappingExpression<SalesUnit, SalesUnitViewModel> config)
        {
            return config.ForMember(s => s.Currency, d => d.MapFrom(f => f.Currency))
                .ForMember(s => s.Name, d => d.MapFrom(f => f.Name))
                .ForMember(s => s.Id, d => d.MapFrom(f => f.Id))
                .ForMember(s => s.Price, d => d.Ignore());
        }

        #endregion
    }
}
