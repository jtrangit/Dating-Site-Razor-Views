using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

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
            public string person1 { get; set; }
            public string person2 { get; set; }
            public decimal lovePercent { get; set; }
            public string description { get; set; }

        }

        [HttpGet]
        public IActionResult Index()
        {
            var url = "https://love-calculator.p.rapidapi.com/getPercentage?fname=John&sname=Alice";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            request.Headers["X-RapidAPI-Key"] = "1aaea12668msh25d702e80d1c95cp14501fjsn28916c99fb9c";
            request.Headers["X-RapidAPI-Host"] = "love-calculator.p.rapidapi.com";

            using var webResponse = request.GetResponse();

            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            var data = reader.ReadToEnd();

            return View();
        }
    }
}
