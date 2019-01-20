using Dreamlines.Data;
using Dreamlines.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Dreamlines.Service.Test
{
    public class SalesUnitServiceTest
    {
        [Fact]
        public void Check_GetByIdAsync()
        {
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseInMemoryDatabase(databaseName: "Dreamlines");
            var options = builder.Options;

            using (var context = new Context(options))
            {
                context.AddRange(GetAllData());
                context.SaveChanges();
            }

            using (var context = new Context(options))
            {
                var repository = new EfRepository<SalesUnit>(context);
                var service = new SalesUnitService(repository);
                var found = service.GetByIdAsync(2).Result;
                Assert.Equal(2, found.Id);
            }
        }
        private List<SalesUnit> GetAllData()
        {
            return new List<SalesUnit>
            {
                new SalesUnit{ Id=1,Country="Germany",Currency="€",Name="dreamlines.de"},
                new SalesUnit{ Id=2,Country="Brazil",Currency="R$",Name="dreamlines.com.br"},
                new SalesUnit{ Id=3,Country="Italy",Currency="€",Name="dreamlines.it"},
                new SalesUnit{ Id=4,Country="France",Currency="€",Name="dreamlines.fr"}
            };
        }
    }
}
