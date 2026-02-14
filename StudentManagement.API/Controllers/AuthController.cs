using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentManagement.API.Data;
using StudentManagement.API.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    /// <summary>
    /// Autentica um usuário e retorna um token JWT
    /// </summary>
    /// <param name="loginDto">Credenciais do usuário</param>
    /// <returns>Token JWT</returns>
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto loginDto)
    {
        // Validação básica
        if (string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password))
        {
            return BadRequest(new { message = "Usuário e senha são obrigatórios" });
        }

        // Verificar se o usuário existe
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.Password == loginDto.Password);

        if (user == null)
        {
            return Unauthorized(new { message = "Usuário ou senha inválidos" });
        }

        // Gerar token JWT
        var token = GenerateJwtToken(user.Username);
        var expiresInMinutes = _configuration.GetValue<int>("Jwt:ExpiresInMinutes", 120);
        
        var response = new LoginResponseDto
        {
            Token = token,
            Username = user.Username,
            ExpiresAt = DateTime.UtcNow.AddMinutes(expiresInMinutes)
        };

        return Ok(response);
    }

    private string GenerateJwtToken(string username)
    {
        var jwtKey = _configuration["Jwt:Key"] ?? "ChaveSecretaSuperSegura123456789012345678901234567890";
        var jwtIssuer = _configuration["Jwt:Issuer"] ?? "StudentManagementAPI";
        var jwtAudience = _configuration["Jwt:Audience"] ?? "StudentManagementClient";
        var expiresInMinutes = _configuration.GetValue<int>("Jwt:ExpiresInMinutes", 120);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
