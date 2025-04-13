using GeminiApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using GeminiApi.Сonnection_key;

namespace GeminiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeminiController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly GeminiKey _geminiKey;
        public GeminiController(HttpClient httpClient,GeminiKey geminiKey)
        {
            _httpClient = httpClient;
            _geminiKey = geminiKey;

        }

        [HttpPost("ask")]
        public async Task<IActionResult> AskGemini([FromBody] UserPrompt request)
        {
            var geminiUrl = _geminiKey.BaseUrl;

            var requestData = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                        new { text = request.Question }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(geminiUrl, content);
            var result = await response.Content.ReadAsStringAsync();

            return Ok(result);
        }


    }
}
