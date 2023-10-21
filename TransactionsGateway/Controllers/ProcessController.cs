using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Text.Json;

namespace TransactionsGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        [HttpPost("Queue")]
        public async Task<IActionResult> Queue([FromBody] RequestDTO request)
        {
            // Что-то вроде балансировки нагрузки
            var requestUri = request.TransactionsCount < 100
                ? $"http://instance1:81/api/Process/Queue"
                : $"http://instance2:81/api/Process/Queue";

            HttpClient client = new HttpClient();
            var result = await client.PostAsJsonAsync(requestUri, request); 
            var content = await result.Content.ReadAsStringAsync();
            return Ok(content);
        }
    }
}
