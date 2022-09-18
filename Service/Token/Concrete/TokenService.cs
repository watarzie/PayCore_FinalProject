using Base.Jwt;
using Base.Response;
using Base.Token;
using Data.Model;
using Data.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NHibernate;
using Service.Token.Abstract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Token.Concrete
{
    public class TokenService :ITokenService
    {
        protected readonly ISession session;
        protected readonly IHibernateRepository<User> hibernateRepository;
        private readonly JwtConfig jwtConfig;

        public TokenService(ISession session, IOptionsMonitor<JwtConfig> jwtConfig)
        {
            this.session = session;
            this.jwtConfig = jwtConfig.CurrentValue;
            hibernateRepository = new HibernateRepository<User>(session);
        }



        public BaseResponse<TokenResponse> GenerateToken(TokenRequest tokenRequest)
        {
            try
            {
                if (tokenRequest is null)
                {
                    return new BaseResponse<TokenResponse>("Please enter valid informations.");
                }

                var account = hibernateRepository.Where(x => x.Email.Equals(tokenRequest.Email)).FirstOrDefault();
                if (account is null)
                {
                    return new BaseResponse<TokenResponse>("Please validate your informations that you provided.");
                }

                if (!account.Password.Equals(tokenRequest.Password))
                {
                    return new BaseResponse<TokenResponse>("Please validate your informations that you provided.");
                }

                DateTime now = DateTime.UtcNow;
                string token = GetToken(account, now);

                try
                {
                    account.LastActivity = now;

                    hibernateRepository.BeginTransaction();
                    hibernateRepository.Update(account);
                    hibernateRepository.Commit();
                    hibernateRepository.CloseTransaction();
                }
                catch (Exception ex)
                {
                    
                    hibernateRepository.Rollback();
                    hibernateRepository.CloseTransaction();
                }

                TokenResponse tokenResponse = new TokenResponse
                {
                    AccessToken = token,
                    ExpireTime = now.AddMinutes(jwtConfig.AccessTokenExpiration),
                    Email = account.Email,
                    SessionTimeInSecond = jwtConfig.AccessTokenExpiration * 60
                };

                return new BaseResponse<TokenResponse>(tokenResponse);
            }
            catch (Exception ex)
            {
                
                return new BaseResponse<TokenResponse>("GenerateToken Error");
            }
        }


        private string GetToken(User user, DateTime date)
        {
            Claim[] claims = GetClaims(user);
            byte[] secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);

            var jwtToken = new JwtSecurityToken(
                jwtConfig.Issuer,
                shouldAddAudienceClaim ? jwtConfig.Audience : string.Empty,
                claims,
                expires: date.AddMinutes(jwtConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return accessToken;
        }

        private Claim[] GetClaims(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("AccountId", user.Id.ToString()),
                new Claim("Email",user.Email)
            };

            return claims;
        }
    }
}
