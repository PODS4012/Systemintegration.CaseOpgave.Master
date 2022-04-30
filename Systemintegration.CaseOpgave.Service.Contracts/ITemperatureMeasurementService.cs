using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Systemintegration.CaseOpgave.Shared.DataTransferObjects;

namespace Systemintegration.CaseOpgave.Service.Contracts
{
    public interface ITemperatureMeasurementService
    {
        Task<IEnumerable<Temperatur>> ListAllTodayAsync();
    }
}
