using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using call_json_api.Models;
using Polly;

namespace call_json_api.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {            
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        private async Task<OpenWeathermap> GetOpenWeathermap()
        {
            // Get an instance of HttpClient from the factpry that we registered
            // in Startup.cs
            var client = _httpClientFactory.CreateClient("API Client");

            // Call the API & wait for response. 
            // If the API call fails, call it again according to the re-try policy
            // specified in Startup.cs
            var result = await client.GetAsync("/api/location/1103816/");

            if (result.IsSuccessStatusCode)
            {
                // Read all of the response and deserialise it into an instace of
                // WeatherForecast class
                var content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OpenWeathermap>(content);
            }
            return null;
        }

        public async Task<IActionResult> Index()
        {
            var model = await GetOpenWeathermap();
            // Pass the data into the View
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
