using MedkonTestProject.Models;
using MongoDB.Driver;
using System;

namespace MedkonTestProject.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IMongoDatabase database)
        {
            // Kullanıcı koleksiyonunu MongoDB'den al
            _users = database.GetCollection<User>("Users");
        }
        public async Task<List<User>> GetAllUsers()
        {
            // Tüm kullanıcıları çek
            var users = await _users.Find(Builders<User>.Filter.Empty).ToListAsync();

            // Hata ayıklama (debugging) kodu ekleniyor:
            if (users.Count == 0)
            {
                Console.WriteLine("Kullanıcı bulunamadı");
            }
            else
            {
                Console.WriteLine("Kullanıcılar bulundu: " + users.Count);
            }

            return users; // Kullanıcı listesini döndür
        }

        // Kullanıcı adı ve parola ile kullanıcıyı al
        public async Task<User> GetUserByUsernameAndPassword(string username, string password)
        {
            var users = await GetAllUsers();

            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

            

            // Hata ayıklama (debugging) kodu ekleniyor:
            if (user == null)
            {
                Console.WriteLine("Kullanıcı bulunamadı");
            }
            else
            {
                Console.WriteLine("Kullanıcı bulundu: " + user.Username);
            }

            return user;
        }
         
    }

}
