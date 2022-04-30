using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systemintegration.CaseOpgave.Service.Contracts
{
    public interface IElectricityPriceService
    {
        Task<double> GetElectricityPrice();
    }
}
