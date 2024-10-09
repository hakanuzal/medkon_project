using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MedkonTestProject.Models;

namespace MedkonTestProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagerController : Controller // ControllerBase yerine Controller
    {
        private readonly IMongoCollection<Log> _logs;

        public ManagerController(IMongoDatabase database)
        {
            _logs = database.GetCollection<Log>("Logs");
        }

        // Kullanıcının loglarını listele
        [HttpGet("user-logs/{userId}")]
        public async Task<IActionResult> GetUserLogs(string userId)
        {
            var logs = await _logs.Find(l => l.UserId == userId).ToListAsync();
            if (logs.Count == 0) return NotFound("Bu kullanıcıya ait log kaydı bulunamadı.");

            return Ok(logs);
        }

        // Yönetici paneli için view döndür
        [HttpGet("index")]
        public IActionResult Index()
        {
            var logs = _logs.Find(_ => true).ToList(); // Tüm logları al
            return View(logs); // Logları model olarak geç
        }

    }
}
