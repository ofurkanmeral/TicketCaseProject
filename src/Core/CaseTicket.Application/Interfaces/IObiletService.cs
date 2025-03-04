using CaseTicket.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTicket.Application.Interfaces
{
    public interface IObiletService
    {
        Task<getSessionResponse>getSession(getSessionRequest session);
        Task<GetBusLocations> BusLocations(getbuslocationsRequest request);
        Task<List<CityList>> LocationCityFilter(GetBusLocations model);
        Task<getbusjourneysResponse> GetBusjourneysList(getbusjourneys request);
        getSessionRequest createSessionModel(HttpContext context);
        getbuslocationsRequest createBuslocationsModel(getSessionResponse session);
        getbusjourneys createBusjourneysModel(SearchParam param, getSessionResponse session);
    }
}
