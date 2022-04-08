using TodoTask.DTOs;

namespace TodoTask.Models;

public record User
{
    public int UserId { get; set; }

    public string Username { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }



    public UserDto asDto => new UserDto
        {
            UserId = UserId,
            Username = Username,
            Name = Name,
            Email = Email,
            Password = Password,
        };
}