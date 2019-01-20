using AutoMapper;
using Dreamlines.Common.Domain;
using Dreamlines.Data;
using Dreamlines.Domain;
using Dreamlines.Service;
using Dreamlines.Web.Controllers;
using Dreamlines.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dreamlines.Web.Test
{
    public class BookingTest
    {
        [Fact]
        public void Check_GetAll_Pagination_Success()
        {
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseInMemoryDatabase(databaseName: "Dreamlines");
            var options = builder.Options;
            var mapObject = new Pagination<Domain.Search.BookingSearch>(0, 10, 10, new Domain.Search.BookingSearch() { Id = 1 });

            using (var context = new Context(options))
            {
                context.AddRange(GetAllSalesUnitsData());
                context.AddRange(GetAllShipData());
                context.AddRange(GetAllBookingData());
                context.SaveChanges();
            }

            using (var context = new Context(options))
            {
                var bookingRepository = new EfRepository<Booking>(context);
                var saleUnitRepository = new EfRepository<SalesUnit>(context);
                var bookingService = new BookingService(bookingRepository, saleUnitRepository);
                var saleUnitService = new SalesUnitService(saleUnitRepository);
                Mock<IMapper> mockMapper = new Mock<IMapper>();
                mockMapper.Setup(x => x.Map<Pagination<Domain.Search.BookingSearch>>(It.IsAny<Pagination<BookingSearchViewModel>>()))
                    .Returns(mapObject);

                IMapper mapper = mockMapper.Object;
                var controller = new BookingController(bookingService, saleUnitService, mapper);
                var actionResult = controller.GetAll(new Common.Domain.Pagination<Model.BookingSearchViewModel>(0, 10, 0, new Model.BookingSearchViewModel { Id = 1 })).Result;
                var viewResult = Assert.IsType<OkObjectResult>(actionResult);
                var model = Assert.IsAssignableFrom<Pagination<SaleUnitBookingViewModel>>(viewResult.Value);
                Assert.Equal(10, model.Count);
                Assert.True(model.Next);
                Assert.True(!model.Previous);
                Assert.Equal(10, model.PageSize);
                Assert.Equal(0, model.PageIndex);
            }
        }

        private List<SalesUnit> GetAllSalesUnitsData()
        {
            return new List<SalesUnit>
            {
                new SalesUnit
                {
                    Id =1,
                    Country ="Germany",
                    Currency ="€",
                    Name ="dreamlines.de",
                },
            };
        }
        private List<Ship> GetAllShipData()
        {
            return new List<Ship>
            {
                new Ship
                {
                    Id=1,
                    Name="Ship1",
                    SalesUnitId=1
                },
                new Ship
                {
                    Id=2,
                    Name="Ship1",
                    SalesUnitId=1
                },
                new Ship
                {
                    Id=3,
                    Name="Ship1",
                    SalesUnitId=1
                },
            };
        }
        private List<Booking> GetAllBookingData()
        {
            return new List<Booking>
            {
                new Booking
                {
                    Id=1,
                    BookingDate=DateTime.Now,
                    Price=10,
                    ShipId=1
                },
                new Booking
                {
                    Id=2,
                    BookingDate=DateTime.Now,
                    Price=15,
                    ShipId=1
                },
                new Booking
                {
                    Id=3,
                    BookingDate=DateTime.Now,
                    Price=4,
                    ShipId=2
                },
                new Booking
                {
                    Id=4,
                    BookingDate=DateTime.Now,
                    Price=5,
                    ShipId=2
                },
                new Booking
                {
                    Id=5,
                    BookingDate=DateTime.Now,
                    Price=8,
                    ShipId=2
                },
                new Booking
                {
                    Id=6,
                    BookingDate=DateTime.Now,
                    Price=1,
                    ShipId=2
                },
                new Booking
                {
                    Id=7,
                    BookingDate=DateTime.Now,
                    Price=10,
                    ShipId=3
                },
                new Booking
                {
                    Id=8,
                    BookingDate=DateTime.Now,
                    Price=10,
                    ShipId=3
                },
                new Booking
                {
                    Id=9,
                    BookingDate=DateTime.Now,
                    Price=4,
                    ShipId=3
                },
                new Booking
                {
                    Id=10,
                    BookingDate=DateTime.Now,
                    Price=9,
                    ShipId=3
                }
            };
        }
    }
}
