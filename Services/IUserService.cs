using MedkonTestProject.Models;

namespace MedkonTestProject.Services
{
    public interface IUserService
    {
        Task<User> GetUserByUsernameAndPassword(string username, string password);
    }
}
