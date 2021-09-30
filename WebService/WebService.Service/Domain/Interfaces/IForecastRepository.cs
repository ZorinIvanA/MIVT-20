using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Domain.Entities.Service;

namespace WebService.Service.Domain.Interfaces
{
    public interface IForecastRepository
    {
        WeatherForecast[] GetForecast();
    }
}
