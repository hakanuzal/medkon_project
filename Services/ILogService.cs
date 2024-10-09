using MedkonTestProject.Models;

namespace MedkonTestProject.Services
{
    public interface ILogService
    {
        Task<List<Log>> GetLogsByUserId(string userId);
        Task AddLog(Log log);

        // LogUserAction metodunu tanımlıyoruz
        Task LogUserAction(string userId, string action, DateTime actionTime);
    }
}
