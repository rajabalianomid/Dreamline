using Dreamlines.Common;
using Dreamlines.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dreamlines.Data.Test
{
    public class EFRepositoryTest
    {
        [Fact]
        public void Check_GetById()
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
                var found = repository.GetById(2);
                Assert.Equal(2, found.Id);
            }
        }

        [Fact]
        public void Check_Manual_Query()
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
                var count = repository.Table.Where(w => w.Country.Contains("I")).Count(); ;
                Assert.Equal(1, count);
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
