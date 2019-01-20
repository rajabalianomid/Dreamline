using Dreamlines.Common;
using Dreamlines.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dreamlines.Service
{
    public class SalesUnitService : ISalesUnitService
    {
        private readonly IRepository<SalesUnit> _repositorySalesUnit;
        public SalesUnitService(IRepository<SalesUnit> repositorySalesUnit)
        {
            this._repositorySalesUnit = repositorySalesUnit;
        }
        public async Task<IList<SalesUnit>> GetAllAsync()
        {
            return await _repositorySalesUnit.Table.ToListAsync();
        }
        public async Task<SalesUnit> GetByIdAsync(int id)
        {
            return await _repositorySalesUnit.GetByIdAsync(id);
        }
    }
}
