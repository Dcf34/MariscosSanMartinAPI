using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MariscosSanMartinAPI.Middleware
{
    public class Auth
    {
        internal static void ValidaJWT(IServiceCollection services)
        {
            string secretKey = JsonConfiguration.AppSetting.GetValue<string>("SecretKey") ?? "";

            services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ValidIssuer = "Mariscos_API",
                    ValidAudience = "USERS",
                    RequireExpirationTime = true
                };
            });
        }
    }
}
