

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoTask.DTOs;

public record TodoDto
{
    [JsonPropertyName("id")]
    [Required]
    public int Id { get; set; }

    [JsonPropertyName("user_id")]
    [Required]
    public int UserId { get; set; }

    [JsonPropertyName("description")]
    [Required]
    public string Description { get; set; }

    [JsonPropertyName("is_completed")]
    [Required]
    public bool IsCompleted { get; set; }


}
public record CreateTodoDto
{


    [JsonPropertyName("description")]
    [Required]
    public string Description { get; set; }

    [JsonPropertyName("is_completed")]
    [Required]
    public bool IsCompleted { get; set; }


}

public record UpdateTodoDto
{


    [JsonPropertyName("iscompleted")]
    [Required]
    public bool IsCompleted { get; set; }


}