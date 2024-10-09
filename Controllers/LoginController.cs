using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MedkonTestProject.Models; // User modeli ve MongoDB bağlantısı
using MedkonTestProject.Services; // Kullanıcı ve log servisleri
using System;

namespace MedkonTestProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogService _logService;

        public LoginController(IUserService userService, ILogService logService)
        {
            _userService = userService;
            _logService = logService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(); // Giriş sayfasını döndür
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Kullanıcıyı veritabanından bul
            var user = await _userService.GetUserByUsernameAndPassword(username, password);
            if (user == null)
            {
                // Kullanıcı bulunamadığında hata mesajı döndür
                ViewBag.ErrorMessage = "Geçersiz kullanıcı adı veya şifre.";
                return View("Index"); // Hata mesajıyla birlikte giriş sayfasını döndür
            }

            // Kullanıcı giriş işlemini log kaydına ekleyelim
            await _logService.LogUserAction(user.Id, "Login", DateTime.Now);

            // Kullanıcı rolüne göre yönlendirme
            if (user.Role == "Admin")
            {
                return RedirectToAction("Index", "Admin"); // Admin paneline yönlendirme
            }
            else if (user.Role == "Manager")
            {
                return RedirectToAction("Index", "Manager"); // Manager paneline yönlendirme
            }
            else if (user.Role == "Normal User")
            {
                return Content("Medkon Test Uygulamasına Hoş Geldiniz"); // Normal kullanıcıya hoş geldiniz mesajı
            }

            return Unauthorized(); // Eğer rol tanımlı değilse
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string userId)
        {
            // Kullanıcı çıkış işlemini log kaydına ekleyelim
            await _logService.LogUserAction(userId, "Logout", DateTime.Now);
            return RedirectToAction("Index"); // Giriş sayfasına döner
        }
    }
}
