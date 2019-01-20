using Dreamlines.Data;
using Dreamlines.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Dreamlines.Service.Test
{
    public class BookingServiceTest
    {
        [Fact]
        public void Check_Sum_Price_From_Bookings_By_SaleUnitsIds()
        {
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseInMemoryDatabase(databaseName: "Dreamlines");
            var options = builder.Options;

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
                var service = new BookingService(bookingRepository, saleUnitRepository);
                var found = service.GetTotalPriceBySalesUnitIds(new List<int> { 1 });
                Assert.Equal(76, found.FirstOrDefault().Value);
            }
        }

        [Fact]
        public void Check_Get_Bookings_BySalesUnitId_Paging()
        {
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseInMemoryDatabase(databaseName: "Dreamlines");
            var options = builder.Options;

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
                var service = new BookingService(bookingRepository, saleUnitRepository);
                var found = service.GetBookingsBySearch(new Common.Domain.Pagination<Domain.Search.BookingSearch>(1, 3, 10, new Domain.Search.BookingSearch { Id = 1 })).Result;
                Assert.Equal(4, found.Data.First().Id);
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
