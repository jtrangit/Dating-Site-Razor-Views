using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Love_Calculator_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : Controller
    {
        private HttpClient _Client;

        public CalculatorController(HttpClient client)
        {
            _Client = client;
        }

        public class LovePercent
        {
            public string fname { get; set; }
            public string sname { get; set; }
            public decimal percentage { get; set; }
            public string result { get; set; }

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //API KEY: "X-RapidAPI-Key", "1aaea12668msh25d702e80d1c95cp14501fjsn28916c99fb9c"
            //API HOST "X-RapidAPI-Host", "love-calculator.p.rapidapi.com"
            //URL "https://love-calculator.p.rapidapi.com/getPercentage"
            var client = new RestClient($"https://love-calculator.p.rapidapi.com/getPercentage?sname=Alice&fname=John");
            var request = new RestRequest(Method.GET);
            
            IRestResponse response = await client.ExecuteAsync(request);

            return Ok(response);
        }
    }
}
