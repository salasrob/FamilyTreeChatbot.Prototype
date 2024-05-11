
using chatbot.data.Utilities;
using chatbot.entities.Config;
using chatbot.entities.Domain;
using chatbot.entities.Requests;
using chatbot.entities.Security;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace chatbot.data.Repositories
{
    public class UsersDataRepository : BaseDataRepository, IUsersDataRepository
    {
        private readonly ILogger<UsersDataRepository> _logger;
        private readonly AuthenticationUtil _authenticationUtil;

        public UsersDataRepository(ILogger<UsersDataRepository> logger, IOptions<AppSettings> appSettings) : base(appSettings, logger)
        {
            _logger = logger;
            _authenticationUtil = new AuthenticationUtil();
        }

        public async Task<IUserAuth> Authenticate(string username, string password)
        {
            User userDomainModel = new User();
            try
            {
                using (var conn = CreateSqlConnection())
                {
                    await OpenAsyncConnection(conn);

                    string query = $"[dbo].[Get_User_By_UserName]";
                    SqlCommand cmd = GetCommand(conn, query, paramMapper: delegate (SqlParameterCollection collection)
                    {
                        collection.AddWithValue("@UserName", username);
                    });

                    var dr = await cmd.ExecuteReaderAsync();
                    if (dr.HasRows)
                        userDomainModel = dr.MapToSingle<User>();

                    CloseConnection(conn, dr);
                }


                if (_authenticationUtil.VerifyPassword(password, userDomainModel.Password))
                {
                    IUserAuth user = new UserBase
                    {
                        Id = userDomainModel.Id
,
                        UserName = userDomainModel.UserName
,
                        Roles = userDomainModel.Roles
,
                        TenantId = "ChatbotApp-00.1.0"
                    };

                    return user;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Username: {username} LogInAsync failed: {ex}");
                throw;
            }
            return null;
        }

        public async Task<int> CreateUser(UserAddRequest user)
        {
            int userId = -1;
            try
            {
                string hashedPassword = _authenticationUtil.HashPassword(user.Password);

                using (var conn = CreateSqlConnection())
                {
                    await OpenAsyncConnection(conn);

                    string query = $"[dbo].[Create_User]";
                    SqlCommand cmd = GetCommand(conn, query, paramMapper: delegate (SqlParameterCollection collection)
                    {
                        collection.AddWithValue("@FirstName", user.FirstName);
                        collection.AddWithValue("@MiddleName", user.MiddleName);
                        collection.AddWithValue("@MiddleInitial", user.MiddleInitial);
                        collection.AddWithValue("@LastName", user.LastName);
                        collection.AddWithValue("@UserName", user.UserName);
                        collection.AddWithValue("@Password", hashedPassword);
                        collection.AddWithValue("@IsAccountActive", user.IsAccountActive);

                        SqlParameter idOut = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                        idOut.Direction = System.Data.ParameterDirection.Output;

                        collection.Add(idOut);
                    });

                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    if (rowsAffected > 0)
                    {
                        userId = (int)cmd.Parameters["@Id"].Value;

                        if (userId < 0)
                        {
                            _logger.LogWarning($"UserId: {user.UserName} failed to create");
                        }
                    }
                    else
                    {
                        _logger.LogWarning($"UserId: {user.UserName} failed to create");
                    }

                    CloseConnection(conn, null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserId: {user.UserName} RegisterUser failed: {ex}");
                throw;
            }
            return userId;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            User user = null;
            try
            {
                using (var conn = CreateSqlConnection())
                {
                    await OpenAsyncConnection(conn);

                    string query = $"[dbo].[Get_User_By_UserName]";
                    SqlCommand cmd = GetCommand(conn, query, paramMapper: delegate (SqlParameterCollection collection)
                    {
                        collection.AddWithValue("@UserName", userName);
                    });

                    var dr = await cmd.ExecuteReaderAsync();
                    if (dr.HasRows)
                        user = dr.MapToSingle<User>();

                    CloseConnection(conn, dr);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserName: {userName} GetUserByUserName failed: {ex}");
                throw;
            }
            return user;
        }

        public async Task<User> GetUserById(int userId)
        {
            User user = null;
            try
            {
                using (var conn = CreateSqlConnection())
                {
                    await OpenAsyncConnection(conn);

                    string query = $"[dbo].[Get_User_By_Id]";
                    SqlCommand cmd = GetCommand(conn, query, paramMapper: delegate (SqlParameterCollection collection)
                    {
                        collection.AddWithValue("@UserId", userId);
                    });

                    var dr = await cmd.ExecuteReaderAsync();
                    if (dr.HasRows)
                        user = dr.MapToSingle<User>();

                    CloseConnection(conn, dr);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserId: {userId} GetUserById failed: {ex}");
                throw;
            }
            return user;
        }
        public async Task<List<User>> GetUsers()
        {
            List<User> users = new List<User>();
            try
            {
                using (var conn = CreateSqlConnection())
                {
                    await OpenAsyncConnection(conn);

                    //TODO create stored procedure
                    string query = $"";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        var dr = await cmd.ExecuteReaderAsync();
                        if (dr.HasRows)
                            users = dr.MapToList<User>();
                    }
                    CloseConnection(conn, null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetUsers() failed: {ex}");
                throw;
            }
            return users;
        }
    }
}
