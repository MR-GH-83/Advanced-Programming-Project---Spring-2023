using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
namespace Football_Fantasy.SubClasses;




public class Token
{
    private static string Key = "Football_Fantasy_Shahed_Project_In_Year_2023_Advance_Programmin_With_Teacher_Haghighat_Doost";
    public static string? ReadToken(string Type,string Token)
    {

        
        string? Result = "";
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(Token);
             Result = jsonToken.Claims.FirstOrDefault(claim => claim.Type == Type).Value;
        }
        catch (Exception e)
        {
            Result = null;
        }
        return Result;
    }
    
    public static string GenerateToken(string Type,string Value)
    {
        
// Define the secret key used to sign the token
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

// Create the signing credentials using the secret key
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

// Create the claims for the JWT
        var claims = new[]
        {
            new Claim(Type, Value)
        };

// Create the JWT
        var token = new JwtSecurityToken(
            issuer: "FootballFantasy_Shahed",
            audience: "my_audience",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(72),
            signingCredentials: signingCredentials
        );

// Generate the token
        string encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return encodedToken;
    }
}