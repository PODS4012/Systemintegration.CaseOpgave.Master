using Systemintegration.CaseOpgave.Shared.DataTransferObjects;

namespace Systemintegration.CaseOpgave.Service.Contracts
{
    public interface IWeatherService
    {
        IEnumerable<WeatherParam> GetWeatherParams();
    }
}
