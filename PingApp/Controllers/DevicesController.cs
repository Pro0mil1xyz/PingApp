namespace PingApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PingApp.Repositories; // Namespace Twojego repozytorium
    using PingApp.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;

        public DevicesController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _deviceRepository.GetAllDevicesAsync();
            return Ok(devices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice(int id)
        {
            var device = await _deviceRepository.GetDeviceByIdAsync(id);
            if (device == null)
                return NotFound();
            return Ok(device);
        }

        [HttpPost]
        public async Task<IActionResult> AddDevice(Device device)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _deviceRepository.AddDeviceAsync(device);
            return CreatedAtAction(nameof(GetDevice), new { id = device.Id }, device);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevice(int id, Device device)
        {
            if (id != device.Id)
                return BadRequest("ID z URL i obiektu nie są zgodne.");

            await _deviceRepository.UpdateDeviceAsync(device);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            await _deviceRepository.DeleteDeviceAsync(id);
            return NoContent();
        }
    }
}
