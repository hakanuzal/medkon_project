using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MedkonTestProject.Models;

namespace MedkonTestProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Log> _logs;

        public AuthController(IMongoDatabase database)
        {
            _users = database.GetCollection<User>("Users");
            _logs = database.GetCollection<Log>("Logs");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _users.Find(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();

            if (user == null) return Unauthorized("Invalid credentials");

            // Giriş logunu kaydet
            var log = new Log
            {
                UserId = user.Id,
                LoginTime = DateTime.Now
            };
            await _logs.InsertOneAsync(log);

            // Kullanıcı rolüne göre mesaj döndür
            if (user.Role == "Admin")
                return Ok("Admin: Hoş geldiniz, kullanıcıları yönetebilirsiniz.");
            else if (user.Role == "Manager")
                return Ok("Yönetici: Logları görüntüleyebilirsiniz.");
            else
                return Ok("Medkon Test Uygulamasına hoş geldiniz.");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(string userId)
        {
            var log = await _logs.Find(l => l.UserId == userId && l.LogoutTime == null).FirstOrDefaultAsync();
            if (log == null) return BadRequest("Aktif oturum bulunamadı.");

            log.LogoutTime = DateTime.Now;
            await _logs.ReplaceOneAsync(l => l.Id == log.Id, log);

            return Ok("Başarıyla çıkış yapıldı.");
        }
    }
    }
}
