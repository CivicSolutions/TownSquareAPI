using Microsoft.AspNetCore.Mvc;
using TownSquareAPI.Models;
using TownSquareAPI.Services;

namespace TownSquareAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PinController : ControllerBase
    {
        private readonly PinService _pinService;

        public PinController(PinService pinService)
        {
            _pinService = pinService;
        }

        [HttpGet("GetPins")]
        public IActionResult GetPins()
        {
            var pins = _pinService.GetPins();
            return Ok(pins);
        }

        [HttpPost("InsertPin")]
        public IActionResult InsertPin([FromBody] PinData pin)
        {
            _pinService.InsertPin(pin);
            return Ok("Pin inserted.");
        }
    }
}