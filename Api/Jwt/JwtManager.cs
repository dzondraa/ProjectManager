using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static EFDataAccess.ProjectManagementContext;
using Api.Jwt;
using Implementation.Core;

namespace SocialNetwork.API.Jwt
{
    public class JwtManager
    {
        private readonly ProjectManagementContextFactory _contextFactory;
        private readonly string _issuer;
        private readonly int _seconds;
        private readonly ITokenStorage _storage;
        private readonly string _secretKey;

        public JwtManager(
            ProjectManagementContextFactory contextFactory, 
            string issuer, 
            string secretKey, 
            int seconds,
            ITokenStorage storage)
        {
            _contextFactory = contextFactory;
            _issuer = issuer;
            _secretKey = secretKey;
            _seconds = seconds;
            _storage = storage;
        }

        public string MakeToken(string email, string password)
        {
            var dbContext = _contextFactory.CreateDbContext(null);
            var user = dbContext.Users
                               .Include(x => x.UserRole)
                               .ThenInclude(x => x.Role)
                               .FirstOrDefault(x => x.Email == email && 
                                                    x.Password == Helper.EncodePasswordToBase64(password));

            if (user == null ||  !user.UserRole.Any())
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            int id = user.Id;
            string username = user.Username;
            List<string> roles = user.UserRole.Select(x => x.Role.Name.ToString()).ToList();

            //Header.Payload.Signature

            //Podaci se nalaze u sekciji Payload

            var tokenId = Guid.NewGuid().ToString();

            _storage.AddToken(tokenId);

            var claims = new List<Claim> // Jti : "",
            {
                new Claim(JwtRegisteredClaimNames.Jti, tokenId, ClaimValueTypes.String, _issuer),
                new Claim(JwtRegisteredClaimNames.Iss, _issuer, ClaimValueTypes.String, _issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _issuer),
                new Claim("Id", id.ToString()),
                new Claim("Username", username),
                new Claim("Email", user.Email),
            };

            claims.AddRange(roles.Select(x => new Claim("role", x)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddSeconds(_seconds),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}
