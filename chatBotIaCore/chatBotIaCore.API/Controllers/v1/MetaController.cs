using chatBotIaCore.Services.Interfaces.Meta;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace chatBotIaCore.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MetaController : ControllerBase
    {
        protected readonly IMessageProcessing _services;

        public MetaController(IMessageProcessing services)
        {
            _services = services;
        }

        [HttpPost("hook")]
        public async Task<IActionResult> HandleWebhookAsync(JsonDocument requestBody)
        {
            try
            {
                var response = await _services.ProcessingMessage(requestBody);
                if (response.MessagedSended == false)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    cod = 500,
                    message = ex.Message
                });
            }
        }
        [HttpGet("hook")]
        public IActionResult HandleWebhook([FromQuery(Name = "hub.challenge")] string hubChallenge) => Ok(hubChallenge);
    }
}
