using API_CRUD.Infrastructure.Context;
using API_CRUD.Infrastructure.Repositories.Interfaces;
using API_CRUD.Infrastructure.Settings;
using API_CRUD.Model.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API_CRUD.Infrastructure.Repositories.Concrete
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        public AuthRepository(ApplicationDbContext applicationDbContext,
                              IOptions<AppSettings> appSettings)
        {
            _context = applicationDbContext;
            _appSettings = appSettings.Value;
        }

        public async Task<AppUser> Authentication(string userName, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == userName && x.Password == password);

            if (user == null)
            {
                return null;
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                return user;
            }
        }
    }
}
