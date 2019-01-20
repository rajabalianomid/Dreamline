using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Dreamlines.Common.Domain;
using Dreamlines.Domain;
using Dreamlines.Service;
using Dreamlines.Web.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dreamlines.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ISalesUnitService _salesUnitService;
        private readonly IMapper _mapper;
        public BookingController(IBookingService bookingService, ISalesUnitService salesUnitService, IMapper mapper)
        {
            this._bookingService = bookingService;
            this._salesUnitService = salesUnitService;
            this._mapper = mapper;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Pagination<SaleUnitBookingViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<SaleUnitBookingViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(Pagination<BookingSearchViewModel> request)
        {
            var model = _mapper.Map<Pagination<Domain.Search.BookingSearch>>(request);
            Pagination<List<Booking>> bookings = await this._bookingService.GetBookingsBySearch(model);
            var result = await PrepareViewModel(bookings, request.Data.Id);
            return Ok(result);
        }
        private async Task<Pagination<SaleUnitBookingViewModel>> PrepareViewModel(Pagination<List<Booking>> bookings, int saleUnitId)
        {
            var foundSaleUnit = await _salesUnitService.GetByIdAsync(saleUnitId);
            var bookingViewModel = bookings.Data.Select(booking =>
            _mapper.Map<Booking, BookingViewModel>(booking, c => c.ConfigureMap()
                .ForMember(s => s.ShipName, d => d.MapFrom(f => f.Ship.Name))
                .ForMember(s => s.Currency, d => d.MapFrom(f => f.Ship.SalesUnit.Currency))));
            var saleUnitViewModel = _mapper.Map<SalesUnit, SalesUnitViewModel>(foundSaleUnit, c => c.ConfigureMap()
                .ForMember(s => s.Currency, d => d.MapFrom(f => f.Currency))
                .ForMember(s => s.Name, d => d.MapFrom(f => f.Name))
                .ForMember(s => s.Id, d => d.MapFrom(f => f.Id))
                .ForMember(s => s.Price, d => d.Ignore()));
            var saleUnitBookingViewModel = new SaleUnitBookingViewModel { Bookings = bookingViewModel, SalesUnit = saleUnitViewModel };
            var model = new Pagination<SaleUnitBookingViewModel>(bookings.PageIndex, bookings.PageSize, bookings.Count, saleUnitBookingViewModel);
            return model;
        }
    }
}