using TodoTask.DTOs;

namespace TodoTask.Models;

public record Todo
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Description { get; set; }

    public bool IsCompleted { get; set; }



    public TodoDto asDto => new TodoDto
        {
            // Id = Id,
            UserId = UserId,
            Description = Description,
            IsCompleted = IsCompleted,
        };
}
