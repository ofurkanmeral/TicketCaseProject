using CaseTicket.Application.Interfaces;
using CaseTicket.Application.Wrappers;
using CaseTicket.Domain.Entities;
using CaseTicket.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CaseTicket.Application.Services
{
    public class OBiletServices : IObiletService
    {
        private readonly IRedisCacheService _redisCacheService;
        private readonly IHttpClientWrapper _client;
        private readonly IConfiguration _configuration;


        public OBiletServices(IRedisCacheService redisCacheService, IHttpClientWrapper httpClientWrapper, IConfiguration configuration)
        {
            _redisCacheService = redisCacheService;
            _client = httpClientWrapper;
            _configuration = configuration;
        }

        public getSessionRequest createSessionModel(HttpContext context)
        {
            return new getSessionRequest() { browser = new Browser() { name = context.Request.Headers["User-Agent"].ToString().Substring(0,10), version = "1.0" }, type = 1, connection = new Connection() { ipaddress = context.Connection.RemoteIpAddress.ToString(), port = context.Connection.RemotePort.ToString() } };
        }

        public getbuslocationsRequest createBuslocationsModel(getSessionResponse session)
        {
            return new getbuslocationsRequest()
            {
                data = null,
                date = DateTime.Now,
                language = "tr-TR",
                devicesession = new getbuslocationsDeviceSession
                {
                    deviceid = session.data.deviceid,
                    sessionid = session.data.sessionid
                }
            };
        }

        public getbusjourneys createBusjourneysModel(SearchParam param, getSessionResponse session)
        {
            return new getbusjourneys()
            {
                devicesession = new DeviceSession
                {
                    deviceid = session.data.deviceid,
                    sessionid = session.data.sessionid
                },
                date = param.TravelDate.ToString("yyyy-MM-dd"),
                language = "tr-TR",
                data = new getbusjourneysData()
                {
                    departuredate = param.TravelDate.ToString("yyyy-MM-dd"),
                    destinationid = Convert.ToInt32(param.Destination),
                    originid = Convert.ToInt32(param.Origin)
                }
            };
        }

        public async Task<getSessionResponse> getSession(getSessionRequest session)
        {
            _client.AddBasicAuthentication();
            string result =await _client.PostAsyncWrapper(_configuration["OBiletAPI:BaseUrl"]+"client/getsession", JsonConvert.SerializeObject(session));
            var model = JsonConvert.DeserializeObject<getSessionResponse>(result);
            _redisCacheService.SetAsync("session", model);
            return model;
        }

        public async Task<GetBusLocations> BusLocations(getbuslocationsRequest request)
        {
            _client.AddBasicAuthentication();
            string result= await _client.PostAsyncWrapper(_configuration["OBiletAPI:BaseUrl"] + "location/getbuslocations",JsonConvert.SerializeObject(request));
            GetBusLocations model =JsonConvert.DeserializeObject<GetBusLocations>(result);
            return model;
        }

        public async Task<getbusjourneysResponse> GetBusjourneysList(getbusjourneys request)
        {
            _client.AddBasicAuthentication();
            string result =await _client.PostAsyncWrapper(_configuration["OBiletAPI:BaseUrl"] + "journey/getbusjourneys", JsonConvert.SerializeObject(request));
            getbusjourneysResponse model =JsonConvert.DeserializeObject<getbusjourneysResponse>(result);
            return model;
        }

        

        public Task<List<CityList>> LocationCityFilter(GetBusLocations model)
        {
            var cityList = model.data
                .Select(y => new CityList
                {
                    CityId = y.id,        
                    CityName = y.name     
                })
                .ToList();
            return Task.FromResult(cityList);
        }

    }
}
