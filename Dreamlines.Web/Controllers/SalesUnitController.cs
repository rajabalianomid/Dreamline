using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Dreamlines.Domain;
using Dreamlines.Service;
using Dreamlines.Web.Mapping;
using Dreamlines.Web.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dreamlines.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SalesUnitController : ControllerBase
    {
        private readonly ISalesUnitService _salesUnitService;
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        public SalesUnitController(ISalesUnitService salesUnitService, IBookingService bookingService, IMapper mapper)
        {
            this._salesUnitService = salesUnitService;
            this._bookingService = bookingService;
            this._mapper = mapper;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IEnumerable<SalesUnitViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var model = PrepareViewModel(await this._salesUnitService.GetAllAsync());
            return Ok(model);
        }
        private IEnumerable<SalesUnitViewModel> PrepareViewModel(IEnumerable<SalesUnit> salesUnits)
        {
            var salesUnitTotalPrice = _bookingService.GetTotalPriceBySalesUnitIds(salesUnits.Select(s => s.Id));
            var result = salesUnits.Select(su =>
              _mapper.Map<SalesUnit, SalesUnitViewModel>(su, c => c.ConfigureMap().Map()
                 .ForMember(s => s.Price, d => d.MapFrom(f =>
                    salesUnitTotalPrice.Where(w => w.Key == su.Id).Select(sp => sp.Value).DefaultIfEmpty(0).FirstOrDefault())
                 )));
            return result;
        }
    }
}