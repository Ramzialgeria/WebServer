using ClientMonitoring.Web.Data; 
using ClientMonitoring.Web.Models; 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims; 
using System.Text;

namespace ClientMonitoring.Web.Controllers.Api;
[ApiController]
[Route("api/auth")] 
public class AuthController:ControllerBase
{
    ApplicationDbContext _db;
    readonly IPasswordHasher<Client> _hasher;
    readonly IConfiguration _cfg;
    public AuthController(ApplicationDbContext d,IPasswordHasher<Client> h,IConfiguration c)
    {
        _db=d;
        _hasher=h;
        _cfg=c;
    } 
    public record LoginRequest(string ClientCode,string ClientSecret);
    [HttpPost("client")]
    public async Task<IActionResult> Client(LoginRequest r)
    {
        var c = await _db.Clients.FirstOrDefaultAsync(x => x.ClientCode == r.ClientCode && x.IsEnabled);

        if (c == null || _hasher.VerifyHashedPassword(c, c.ClientSecretHash, r.ClientSecret) == PasswordVerificationResult.Failed)
            return Unauthorized();
        var j = _cfg.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(j["Key"]!));
        var token = new JwtSecurityToken(
            j["Issuer"],
            j["Audience"],
            new[] { new Claim("client_code", c.ClientCode) },
            expires: DateTime.UtcNow.AddHours(12),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
        return Ok(new { accessToken = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}
