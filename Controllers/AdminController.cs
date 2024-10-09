using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MedkonTestProject.Models;

namespace MedkonTestProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : Controller // ControllerBase yerine Controller
    {
        private readonly IMongoCollection<User> _users;

        public AdminController(IMongoDatabase database)
        {
            _users = database.GetCollection<User>("Users");
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Kullanıcı bilgileri eksik.");
            }

            await _users.InsertOneAsync(user);
            return Ok("Kullanıcı başarıyla oluşturuldu.");
        }

        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _users.DeleteOneAsync(u => u.Id == id);
            if (result.DeletedCount == 0) return NotFound("Kullanıcı bulunamadı.");

            return Ok("Kullanıcı başarıyla silindi.");
        }

        // Kullanıcıları listele
        [HttpGet("list-users")]
        public async Task<IActionResult> ListUsers()
        {
            var users = await _users.Find(_ => true).ToListAsync();
            return Ok(users);
        }

        // Kullanıcı yönetim sayfası için view döndür
        [HttpGet("index")]
        public IActionResult Index()
        {
            var users = _users.Find(_ => true).ToList(); // Tüm kullanıcıları al
            return View(users); // Kullanıcıları model olarak geç
        }

    }
}
