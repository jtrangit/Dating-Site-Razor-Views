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
            var client = new RestClient($"https://love-calculator.p.rapidapi.com/getPercentage?sname=Alice&fname=John");
            var request = new RestRequest(Method.GET);
            
            IRestResponse response = await client.ExecuteAsync(request);

            return Ok(response);
        }
    }
}
