using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Api.Mappings;
using TaskManager.Application.Services;
using TaskManager.Contract.Request;
using TaskManager.Contract.Requests;

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

        var response = member.MapToResponse();

        return Ok(response);
    }

    [HttpPost(ApiEndpoints.Auth.TrustedMemberToken)]
    public IActionResult CreateTrustedMemberToken(
        [FromBody] TokenGenerationRequest request)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, request.Email),
            new(JwtRegisteredClaimNames.Email, request.Email),
            new ("userid", request.UserId.ToString())
        };

        foreach (var claimPair in request.CustomClaims)
        {
            var jsonElement = (JsonElement)claimPair.Value;
            var valueType = jsonElement.ValueKind switch
            {
                JsonValueKind.True => ClaimValueTypes.Boolean,
                JsonValueKind.False => ClaimValueTypes.Boolean,
                JsonValueKind.Number => ClaimValueTypes.Double,
                _ => ClaimValueTypes.String
            };

            var claim = new Claim(claimPair.Key,
                claimPair.Value.ToString()!,
                valueType);

            claims.Add(claim);

        }

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
        return Ok(jwt);
    }
}