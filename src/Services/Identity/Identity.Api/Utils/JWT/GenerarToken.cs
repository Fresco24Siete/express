using System.Security.Claims;
using System.Text;
using Identity.Api.Models.Entities;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

public class TokenService(IConfiguration config)
{

    private readonly IConfigurationSection jwtConfig =
        config.GetSection("Jwt");

    public string GenerarToken(UsuarioEntity usuario)
    {
        var credenciales = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]!)),
            SecurityAlgorithms.HmacSha256);

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer             = jwtConfig["Issuer"],
            Audience           = jwtConfig["Audience"],
            Expires            = DateTime.UtcNow.AddHours(1),
            SigningCredentials = credenciales,
            Claims = new Dictionary<string, object>
            {
                [JwtRegisteredClaimNames.Sub]   = usuario.IdUsuario.ToString(),
                [JwtRegisteredClaimNames.Email] = usuario.Correo,
                [JwtRegisteredClaimNames.Jti]   = Guid.NewGuid().ToString(),
                ["role"] = usuario.RolActual
            }
        };

        return new JsonWebTokenHandler().CreateToken(descriptor);
    }
    
}