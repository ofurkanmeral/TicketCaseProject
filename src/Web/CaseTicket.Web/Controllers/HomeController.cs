using CaseTicket.Application.Interfaces;
using CaseTicket.Application.Services;
using CaseTicket.Domain.Entities;
using CaseTicket.Infrastructure.Interfaces;
using CaseTicket.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace CaseTicket.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IObiletService _obiletService;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IObiletService obiletService, IRedisCacheService redisCacheService, IHttpContextAccessor httpContextAccessor)
        {
            _obiletService = obiletService;
            _redisCacheService = redisCacheService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<getSessionResponse> CreateSessionOBilet()
        {
            var session = _redisCacheService.GetAsync<getSessionResponse>("session");
            if (session == null)
            {
                var createSession= await _obiletService.getSession(_obiletService.createSessionModel(_httpContextAccessor.HttpContext));
                return createSession;
            }
            return session;
        }
        public async Task<IActionResult> Index()
        {
            getSessionResponse session = await CreateSessionOBilet();
            getbuslocationsRequest getbuslocationsRequest = _obiletService.createBuslocationsModel(session);
            var model =await _obiletService.BusLocations(getbuslocationsRequest);
            var location = _obiletService.LocationCityFilter(model).Result;
            return View(location);
        }
        [HttpPost]
        public async Task<IActionResult> List(SearchParam param)
        {
            var session = CreateSessionOBilet().Result;
            ViewBag.OriginName = param.OriginName;
            ViewBag.DestinationName = param.DestinationName;
            ViewBag.TravelDate = param.TravelDate.ToString("d MMMM yyyy", new CultureInfo("tr-TR"));
            var request = _obiletService.createBusjourneysModel(param, session);
            var model = await _obiletService.GetBusjourneysList(request);
            return View(model);
        }

    }
}
