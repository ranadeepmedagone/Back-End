
using Dapper;
using TodoTask.Models;
using TodoTask.Utilities;

namespace TodoTask.Repositories;


public interface IUserRepository
{
    Task<User> Create(User Item);
    Task<bool> Update(User item);
    Task<bool> Delete(int Id);
    Task<User> GetById(int UserId);
    Task<List<User>> GetList();

    Task<User> GetByUsername(string Username);
    // Task<List<UserDTO>> GetList(long Id);



}
public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<User> Create(User item)
    {
        var query = $@"INSERT INTO ""{TableNames.users}""
        (username, name, email, password)
        VALUES (@Username, @Name, @Email, @Password) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<User>(query, item);
            return res;
        }


    }
    

    public Task<bool> Delete(int Id)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetById(int UserId)
    {
        var query = $@"SELECT * FROM ""{TableNames.users}""
        WHERE userid = @UserId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<User>(query,
            new
            {
                UserId = UserId
            });
    }

    public async Task<User> GetByUsername(string Username)
    {
        var query = $@"SELECT * FROM ""{TableNames.users}""
        WHERE username = @UserName";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<User>(query,
            new
            {
                Username = Username
            });
    }

    public Task<List<User>> GetList()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(User item)
    {
        throw new NotImplementedException();
    }
}
