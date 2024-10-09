using MedkonTestProject.Models;
using MongoDB.Driver;

namespace MedkonTestProject.Services
{
    public class LogService : ILogService
    {
        private readonly IMongoCollection<Log> _logs;

        public LogService(IMongoDatabase database)
        {
            _logs = database.GetCollection<Log>("Logs");
        }

        public async Task<List<Log>> GetLogsByUserId(string userId)
        {
            return await _logs.Find(l => l.UserId == userId).ToListAsync();
        }

        public async Task AddLog(Log log)
        {
            await _logs.InsertOneAsync(log);
        }

        // LogUserAction metodunu implemente ediyoruz
        public async Task LogUserAction(string userId, string action, DateTime actionTime)
        {
            var log = new Log
            {
                UserId = userId,
                LoginTime = actionTime, //action'a bağlı olarak giriş/çıkış zamanını güncelle
                // action burada giriş mi çıkış mı olduğunu ifade edebilir
                LogoutTime = action == "logout" ? (DateTime?)actionTime : null
            };

            await _logs.InsertOneAsync(log);
        }
    }
}
