using System;
using Future.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Future.Api.Controllers
{
    [Route("[controller]")]
    public class SyncController : Controller
    {
        private readonly ISyncService _syncService;

        public SyncController(ISyncService syncService)
        {
            _syncService = syncService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _syncService.Fetch(id);

            if (result == null)
                return NotFound();

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _syncService.FetchAll();

            if (results == null)
                return NotFound();

            return Json(results);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SyncRequest requestBody)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            await _syncService.Add(requestBody.Id, requestBody.Data);
            return NoContent();
        }

        [HttpPost("alt")]
        public IActionResult PostAlternative([FromBody] SyncRequest requestBody)
        {
            new Task(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(10));
                await _syncService.Add(requestBody.Id, requestBody.Data);
            }).Start();

            return NoContent();
        }
    }

    public class SyncRequest
    {
        public int Id { get; set; }

        public string Data { get; set; }
    }
}