using Microsoft.AspNetCore.Mvc;
using RollercoasterCreditTracker.Interfaces;
using System.Text.Json;

namespace RollercoasterCreditTracker.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RollercoasterController : ControllerBase
    {
        private readonly IScraperConfiguration _scraperConfig; 

        public RollercoasterController(IScraperConfiguration scraperConfig)
        {
            _scraperConfig = scraperConfig ?? throw new ArgumentNullException(nameof(scraperConfig));            
        }

        [HttpGet]
        [Route("/GetCoasters", Name = "GetCoasters")]        
        public async Task<ActionResult<CoasterData>> GetCoasters()
        {           
            using (HttpClient client = new HttpClient())
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                client.BaseAddress = new Uri(_scraperConfig.ScraperApiUrl);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/coasters?offset=0&limit=300");

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                                                         
                    var coasterData = JsonSerializer.Deserialize<CoasterData>(json, options);

                    return Ok(coasterData);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }

        [HttpGet]
        [Route("/GetCoasterById", Name = "GetCoasterById")]
        public async Task<ActionResult<Data>> GetCoasterById(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                client.BaseAddress = new Uri(_scraperConfig.ScraperApiUrl);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/coasters/" + Id);

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<Data>(json, options);
                    return Ok(data);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }

        [HttpGet]
        [Route("/GetCoasterRandom", Name = "GetCoasterRandom")]
        public async Task<ActionResult<Data>> GetCoasterRandom()
        {
            using (HttpClient client = new HttpClient())
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                client.BaseAddress = new Uri(_scraperConfig.ScraperApiUrl);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/coasters/random");

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<Data>(json, options);
                    return Ok(data);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }

        [HttpGet]
        [Route("/GetCoasterSearch", Name = "GetCoasterSearch")]
        public async Task<ActionResult<CoasterData>> GetCoasterSearch(string query)
        {
            using (HttpClient client = new HttpClient())
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                client.BaseAddress = new Uri(_scraperConfig.ScraperApiUrl);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/coasters/search?q=" + query.Replace("\"", ""));

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<CoasterData>(json, options);
                    
                    return Ok(data);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }

        public class Park
        {
            public string Name { get; set; } = string.Empty;
            public int Id { get; set; }

        }
        public class Status
        {
            public string State { get; set; } = string.Empty;
            public string Date { get; set; } = string.Empty;
        }
        public class Stats
        {
            public dynamic? Length { get; set; } 
            public string UphillLength { get; set; } = string.Empty;
            public string DownhillLength { get; set; } = string.Empty;
            public dynamic? Drop { get; set; } 
            public dynamic? Height { get; set; } 
            public dynamic? Speed { get; set; }
            public string Inversions { get; set; } = string.Empty;
            public string Duration { get; set; } = string.Empty;
            public IEnumerable<string>? Elements { get; set; }
            public string Arrangement { get; set; } = string.Empty;
            public string Manufactured { get; set; } = string.Empty;
            public string Capacity { get; set; } = string.Empty;
            public dynamic? Dimensions { get; set; }

        }
        public class MainPicture
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Url { get; set; } = string.Empty;
            public string CopyName { get; set; } = string.Empty;
            public string CopyDate { get; set; } = string.Empty;

        }
        public class Pictures
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Url { get; set; } = string.Empty;
            public string CopyName { get; set; } = string.Empty;
            public string CopyDate { get; set; } = string.Empty;

        }
        public class Coords
        {
            public string Lat { get; set; } = string.Empty;
            public string Lng { get; set; } = string.Empty;

        }

        public class Data
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public Park? Park { get; set; }
            public string City { get; set; } = string.Empty;
            public string State { get; set; } = string.Empty;
            public string Country { get; set; } = string.Empty;
            public string Region { get; set; } = string.Empty;
            public string Link { get; set; } = string.Empty;
            public Status? Status { get; set; }
            public string Make { get; set; } = string.Empty;
            public string Model { get; set; } = string.Empty;
            public string Type { get; set; } = string.Empty;
            public string Design { get; set; } = string.Empty;
            public Stats? Stats { get; set; }
            public MainPicture? MainPicture { get; set; }
            public IEnumerable<Pictures>? Pictures { get; set; }
            public Coords? Coords { get; set; }

        }

        public class Pagination
        {
            public int Count { get; set; }
            public int Total { get; set; }
            public int Offset { get; set; }
            public int Limit { get; set; }

        }

        public class CoasterData
        {
            public IEnumerable<Data>? Data { get; set; }
            public Pagination? Pagination { get; set; }
            public IEnumerable<Data>? Coasters { get; set; }
            public int TotalMatch { get; set; }

        }
    }
}