using Dreamlines.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dreamlines.Service
{
    public interface ISalesUnitService
    {
        Task<IList<SalesUnit>> GetAllAsync();
        Task<SalesUnit> GetByIdAsync(int id);
    }
}
