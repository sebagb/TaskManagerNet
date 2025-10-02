using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Api.Mappings;
using TaskManager.Application.Models;
using TaskManager.Application.Services;
using TaskManager.Contract.Requests;
using TaskManager.Contract.Responses;

namespace TaskManager.Api.Controllers;

public class AuthController
    (IMemberService service) : ControllerBase
{
    private readonly IMemberService service = service;

    private const string TokenSecret = "PleasePleaseStoreAndLoadSecurelyTheTokenSecret";
    private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);

    [HttpPost(ApiEndpoints.Auth.CreateTrustedMember)]
    public IActionResult CreateTrustedMember(
        [FromBody] CreateMemberRequest request)
    {
        var member = request.MapToMember();
        service.CreateMember(member);
        var response = member.MapToResponse();

        return CreatedAtAction(
            nameof(GetTrustedMember),
            response);
    }

    [HttpPost(ApiEndpoints.Auth.GetTrustedMember)]
    public IActionResult GetTrustedMember(
        [FromBody] GetTrustedMemberRequest request)
    {
        var member = service.GetByCredentials(request.Username, request.Password);

        if (member == null)
        {
            return NotFound();
        }

        var jwt = CreateTrustedMemberToken(member);

        return Ok(new JwtResponse() { Access_token = jwt });
    }

    [HttpPut(ApiEndpoints.Auth.UpdateTrustedMember)]
    [Authorize]
    public IActionResult UpdateTrustedMember(
        [FromBody] UpdateTrustedMemberRequest request)
    {
        var memberId = HttpContext.User.Claims
            .First(x => x.Type.Equals("memberId"))
            .Value;
        var parsed = Guid.TryParse(memberId, out var id);

        if (!parsed)
        {
            return BadRequest("Invalid memberId in claims");
        }

        var member = request.MapToMember(id);

        var result = service.Update(member);

        if (!result)
        {
            return NotFound();
        }

        var jwt = CreateTrustedMemberToken(member);

        return Ok(new JwtResponse() { Access_token = jwt });
    }

    private static string CreateTrustedMemberToken(Member member)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, member.Username),
            new(JwtRegisteredClaimNames.Email, member.Username),
            new("memberId", member.MemberId.ToString()),
            new("admin", member.IsAdmin ? "true" : "false")
        };

        var key = Encoding.UTF8.GetBytes(TokenSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TokenLifetime),
            Issuer = "https://id.taskmanager.com",
            Audience = "https://taskmanager.com",
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        var jwt = tokenHandler.WriteToken(token);

        return jwt;
    }
}