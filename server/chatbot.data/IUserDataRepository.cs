
using chatbot.entities.Domain;
using chatbot.entities.Requests;
using chatbot.entities.Security;

namespace chatbot.data
{
    public interface IUsersDataRepository
    {
        public Task<IUserAuth> Authenticate(string username, string password);
        public Task<int> CreateUser(UserAddRequest user);
        public Task<User> GetUserById(int userId);
        public Task<User> GetUserByUserName(string userName);
        public Task<List<User>> GetUsers();
    }
}
