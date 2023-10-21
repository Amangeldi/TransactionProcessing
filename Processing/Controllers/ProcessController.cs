using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Processing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        [HttpPost("Queue")]
        public async Task<IActionResult> Queue([FromBody] RequestDTO request)
        {
            ProcessExecutor.Requests.Add(request);
            return Ok(ProcessExecutor.Requests.Count);
        }
    }
}
