using Emedicine.DAL.DataAccess.Interface;
using Emedicine.DAL.model;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;


using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Emedicine.BAL.Services
{
    public class Service : IService
    {
        
            private readonly IDataAccess _da;
            private readonly IConfiguration _configuration;
            public Service(IDataAccess da, IConfiguration configuration)
            {
                _da = da;
                _configuration = configuration;
            }

            public async Task<User> Authenticate(string email, string password)
            {
                var user = await _da.user.GetFirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;
                if (user.Password != password) return null;

                var secKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Key"]));
                var cred=new SigningCredentials(secKey,SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    new Claim(ClaimTypes.Email,email),
                };
                var token = new JwtSecurityToken(
                    issuer: _configuration["AppSettings:Issuer"],
                    audience: _configuration["AppSettings:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(3),
                    signingCredentials: cred

                );
                var jwt=new JwtSecurityTokenHandler().WriteToken(token);
                user.Token = jwt;
                _da.save();
            
                return user;

            }
    }
}
