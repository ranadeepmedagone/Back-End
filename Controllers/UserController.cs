using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TodoTask.DTOs;
using TodoTask.Models;
using TodoTask.Repositories;

namespace TodoTask.Controllers;

[ApiController]
[Route("api/user")]
public class userController : ControllerBase
{
    private readonly ILogger<userController> _logger;
    private readonly IUserRepository _user;
    private IConfiguration _configuration;

    public userController(ILogger<userController> logger, IUserRepository user,IConfiguration configuration)
    {
        _logger = logger;
        _user = user;
        _configuration = configuration;
    }

    [HttpGet("{user_id}")]
    

    public async Task<ActionResult<UserDto>> GetById([FromRoute] int user_id)
    {
        var user = await _user.GetById(user_id);
        if (user is null)
            return NotFound("No Student found with given userid");
        var dto = user.asDto;


        return Ok(dto);
    }

    [HttpPost]

    public async Task<ActionResult<UserDto>> Createuser([FromBody] CreateUserDto Data)
    {

        var toCreateuser = new User
        {
            Username = Data.Username.Trim(),
            Name = Data.Name.Trim(),
            Email = Data.Email.Trim(),
            Password = Data.Password.Trim(),
        

        };
        var createduser = await _user.Create(toCreateuser);

        return StatusCode(StatusCodes.Status201Created, createduser.asDto);
        // return Createuser();


    }

    
    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    

    public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
    {
        var currentUser = await _user.GetByUsername(userLogin.Username);
        if (currentUser == null)
          return NotFound("user not found");

        if(currentUser.Password != userLogin.Password)
            return Unauthorized("Invalid password");
        var token = Generate(currentUser);
        return Ok(token);
    }

    

    private string Generate(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            // new Claim(ClaimTypes.GivenName, user.Password)
        };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);    
    }
}



