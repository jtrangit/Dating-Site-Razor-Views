using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;

namespace Love_Calculator_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly RestClient _client;

        public CalculatorController()
        {
            _client = new RestClient("https://love-calculator.p.rapidapi.com");
            _client.AddDefaultHeader("X-RapidAPI-Key", "1aaea12668msh25d702e80d1c95cp14501fjsn28916c99fb9c");
            _client.AddDefaultHeader("X-RapidAPI-Host", "love-calculator.p.rapidapi.com");
        }

        public class LovePercent
        {
            public string fname { get; set; }
            public string sname { get; set; }
            public decimal percentage { get; set; }
            public string result { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> GetLovePercentage(string fname, string sname)
        {
            var request = new RestRequest("getPercentage");
            request.AddParameter("fname", fname);
            request.AddParameter("sname", sname);

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var responseContent = response.Content;
                var lovePercent = new LovePercent();

                // Using System.Text.Json to parse JSON response content
                using (JsonDocument doc = JsonDocument.Parse(responseContent))
                {
                    JsonElement root = doc.RootElement;
                    lovePercent.fname = root.GetProperty("fname").GetString();
                    lovePercent.sname = root.GetProperty("sname").GetString();
                    // Check if percentage value is a string
                    if (root.GetProperty("percentage").ValueKind == JsonValueKind.String)
                    {
                        // Parse the string value to decimal
                        lovePercent.percentage = decimal.Parse(root.GetProperty("percentage").GetString());
                    }
                    else
                    {
                        // Directly get the decimal value
                        lovePercent.percentage = root.GetProperty("percentage").GetDecimal();
                    }
                    lovePercent.result = root.GetProperty("result").GetString();
                }

                return Ok(lovePercent);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Failed to connect to the API.");
            }
        }
    }
}
