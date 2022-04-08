
using Dapper;
using TodoTask.DTOs;
using TodoTask.Models;
using TodoTask.Utilities;

namespace TodoTask.Repositories;


public interface ITodoRepository
{
    Task<Todo> Create(Todo Item);
    Task<bool> Update(Todo Item);
    Task<bool> Delete(long Id);
    Task<Todo> GetById(long Id);
    Task<List<Todo>> GetList();
    Task<List<Todo>> GetMyTodo(long UserId);



}
public class TodoRepository : BaseRepository, ITodoRepository
{
    public TodoRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Todo> Create(Todo item)
    {
        var query = $@"INSERT INTO ""{TableNames.todos}""
        (userid, description, iscompleted)
        VALUES (@UserId, @Description, @IsCompleted) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Todo>(query, item);
            return res;
        }


    }


    public async Task<bool> Delete(long Id)
    {
        var query = $@"DELETE FROM ""{TableNames.todos}"" 
        WHERE id = @Id";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { Id });
            return res > 0;
        }
    }

    public async Task<Todo> GetById(long Id)
    {
        var query = $@"SELECT * FROM ""{TableNames.todos}""
        WHERE id = @Id";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Todo>(query,
            new
            {
                Id = Id
            });
    }

    public async Task<List<Todo>> GetList()
    {
        // Query
        var query = $@"SELECT * FROM ""{TableNames.todos}""";

        List<Todo> res;
        using (var con = NewConnection) // Open connection
            res = (await con.QueryAsync<Todo>(query)).AsList(); // Execute the query
        // Close the connection

        // Return the result
        return res;
    }



    public async Task<List<Todo>> GetMyTodo(long UserId)
    {
        var query = $@"SELECT * FROM ""{TableNames.todos}"" WHERE userid = @UserId;";
        using (var con = NewConnection)
            return (await con.QueryAsync<Todo>(query, new { UserId })).ToList();
    }



    public async Task<bool> Update(Todo Item)
    {
        var query = $@"UPDATE ""{TableNames.todos}"" SET  iscompleted = @IsCompleted 
        WHERE id = @Id";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, Item);
            return rowCount == 1;
        }
    }
}
