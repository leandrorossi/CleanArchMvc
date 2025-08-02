using CleanArchMvc.API.DTO;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthenticate authenticate, IConfiguration configuration) : ControllerBase
{
    private readonly IAuthenticate _authenticate = authenticate;
    private readonly IConfiguration _configuration = configuration;

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var result = await _authenticate.Authenticate(loginDTO.Email, loginDTO.Password);

        if (!result)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return BadRequest(ModelState);
        }

        return Ok(GenerateToken(loginDTO));
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        var result = await _authenticate.Register(registerDTO.Email, registerDTO.Password);

        if (!result)
        {
            ModelState.AddModelError(string.Empty, "Invalid register attempt");
            return BadRequest(ModelState);
        }

        return Ok($"User {registerDTO.Email} was created succesccfully");
    }

    private UserToken GenerateToken(LoginDTO loginDTO)
    {
        var claims = new[]
        {
            new Claim("email", loginDTO.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddMinutes(30);

        JwtSecurityToken token = new(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        return new UserToken
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
        };
    }
}
