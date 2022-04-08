using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoTask.DTOs;

public record UserDto
{

    [JsonPropertyName("user_id")]
    [Required]
    public int UserId { get; set; }

    [JsonPropertyName("user_name")]
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }
    [JsonPropertyName("name")]
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [JsonPropertyName("email")]
    [Required]
    [MaxLength(255)]
    public string Email { get; set; }
    [JsonPropertyName("password")]
    [Required]
    public string Password { get; set; }
}

public record CreateUserDto
{

    [JsonPropertyName("user_name")]
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }
    [JsonPropertyName("name")]
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [JsonPropertyName("email")]
    [Required]
    [MaxLength(255)]
    public string Email { get; set; }
    [JsonPropertyName("password")]
    [Required]
    public string Password { get; set; }
}