using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using SuperBotManagerBackend.Configuration;
using SuperBotManagerBackend.DTOs;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.DB.Repositories;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace SuperBotManagerBackend.Services
{
    public interface IAuthService
    {
        Task<UserTokensDTO> SignIn(UserSignInRequestDTO loginReq);
        Task<UserTokensDTO> SignUp(UserSignUpRequestDTO signinReq);
        Task<UserTokensDTO> RefreshToken(RefreshTokenRequstDTO refreshToken);
        Task Logout();
    }

    public class AuthService : IAuthService
    {
        private readonly IAppUnitOfWork uow;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public AuthService(IAppUnitOfWork uow, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.uow = uow;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }

        public async Task<UserTokensDTO> CreateTokens(User user, string oldRefreshToken = null)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), /// for logout (revoke token)
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };
            if(user.Roles != null)
            {
                foreach(var role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
                }
            }
            var key = Encoding.UTF8.GetBytes(JWTConfig.Key);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = JWTConfig.Issuer,
                Audience = JWTConfig.Audience,
                Expires = DateTime.UtcNow.AddMinutes(JWTConfig.LifetimeInMinutes),
                SigningCredentials = signingCredentials,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);


            var newRefreshToken = await uow.RefreshTokenRepository.GenerateNewAndRemoveOld(user, oldRefreshToken);
            var userDTO = mapper.Map<UserDTO>(user);

            return new UserTokensDTO()
            {
                Token = tokenHandler.WriteToken(token),
                TokenExpiration = token.ValidTo,
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiration = newRefreshToken.ExpirationDate,
                User = userDTO,
            };
        }
        public async Task<UserTokensDTO> SignIn(UserSignInRequestDTO loginReq)
        {
            var user = await uow.UserRepository.GetByUserName(loginReq.UserName,
                                        a => a.Include(x => x.UserPasswords)
                                              .Include(x => x.Roles));
            if(user == null)
            {
                throw HttpUtilsService.BadRequest("Bad email or password");
            }
            var password = uow.UserRepository.GetCurrentUserPassword(user);
            var isCorrectPass = password != null && BC.Verify(loginReq.Password, password.PasswordHash);

            if(!isCorrectPass)
            {
                throw HttpUtilsService.BadRequest("Bad email or password");
            }

            var result = await CreateTokens(user);
            await uow.SaveChangesAsync();

            return result;
        }

        public async Task<UserTokensDTO> SignUp(UserSignUpRequestDTO signinReq)
        {
            var userExists = await uow.UserRepository.GetByUserName(signinReq.UserName) != null;
            /// TODO you can check if email exists in one db query
            if(userExists)
            {
                throw HttpUtilsService.BadRequest("User with that name already exists :(");
            }
            var user = new User()
            {
                UserEmail = signinReq.Email,
                UserName = signinReq.UserName,
                UserPasswords = new List<UserPassword>()
                {
                    new UserPassword()
                    {
                        PasswordDate = DateTime.UtcNow,
                        PasswordHash = BC.HashPassword(signinReq.Password),
                    }
                },
                Roles = new List<Role>()
                {
                }
            };
            await uow.UserRepository.Create(user);
            await uow.SaveChangesAsync();

            var result = await CreateTokens(user);
            await uow.SaveChangesAsync();

            return result;
        }
        public async Task Logout()
        {
            var userIdStr = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userId = int.Parse(userIdStr);
            var tokenGuid = httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Jti).Value;
            var user = await uow.UserRepository.GetById(userId, a => a.Include(x => x.RevokedTokens));
            if(user == null)
            {
                throw HttpUtilsService.BadRequest();
            }
            user.RevokedTokens.Add(new RevokedToken()
            {
                TokenGuid = tokenGuid,
                // in order to know when we can remove it from db. Its a little bit longer than real token lifetime
                ExpirationDate = DateTime.UtcNow.AddMinutes(JWTConfig.LifetimeInMinutes),
            });
            if(user.RevokedTokens.Count > 10)
            {
                /// todo sprawdzic czy sie usuwa z bazy
                user.RevokedTokens.Remove(user.RevokedTokens.OrderBy(a => a.ExpirationDate).FirstOrDefault());
            }
            await uow.UserRepository.Update(user);
            await uow.SaveChangesAsync();
        }
        private ClaimsPrincipal? _getPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTConfig.Key)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if(securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
        public async Task<UserTokensDTO> RefreshToken(RefreshTokenRequstDTO refreshToken)
        {
            var principal = _getPrincipalFromExpiredToken(refreshToken.Token);
            var userIdStr = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userId = int.Parse(userIdStr);
            var user = await uow.UserRepository.GetById(userId, a => a.Include(x => x.RefreshTokens).Include(x => x.Roles));
            if(user == null)
            {
                throw HttpUtilsService.BadRequest();
            }
            if(!user.RefreshTokens.Any(a => a.Token == refreshToken.RefreshToken))
            {
                throw HttpUtilsService.BadRequest("Refresh token is invalid or already expired");
            }
            var result = await CreateTokens(user, refreshToken.RefreshToken);
            await uow.SaveChangesAsync();
            return result;
        }
    }
}
