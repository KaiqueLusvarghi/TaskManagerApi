using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using TaskManagerApi.DTOS;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Endpoint de login
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            // Validação de credenciais (apenas exemplo, substitua pela lógica real)
            // Aqui, você pode substituir pela lógica que autentica o usuário no seu banco de dados.
            if (loginDto.Username == "admin" && loginDto.Password == "123456")
            {
                var token = GenerateJwtToken(loginDto.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized("Credenciais inválidas.");
        }

        // Método para gerar o JWT Token
        private string GenerateJwtToken(string username)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

            // Claims do usuário
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username), // Nome do usuário
                new Claim("UniqueName", username), // Outro claim, você pode adicionar mais conforme necessário
                new Claim(ClaimTypes.Role, "Admin") // Exemplo de claim de role, altere conforme necessário
            };

            // Definições do token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // Claims que serão associadas ao token
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpirationMinutes"])), // Tempo de expiração configurado no appsettings.json
                Issuer = jwtSettings["Issuer"], // Emissor configurado no appsettings.json
                Audience = jwtSettings["Audience"], // Público configurado no appsettings.json
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature) // Assinatura do token com a chave secreta
            };

            // Criação do token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); // Retorna o token JWT gerado
        }
    }
}
