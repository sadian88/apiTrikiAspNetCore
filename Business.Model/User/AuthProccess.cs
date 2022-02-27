using ApiTriki.Models.Db;
using ApiTriki.Models.Request.User;
using ApiTriki.Models.Response.User;
using ApiTriki.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Business.Model.User
{
    public class AuthProccess
    {
        private readonly IConfiguration _configuration;
        public ResponseLoginUser Login(RequestLogin requestLogin)
        {
            var ResponseLoginUser = new ResponseLoginUser();

            using (var db = new TrikiDbContext())
            {
                var existingUser = db.User.SingleOrDefault(x => x.Email == requestLogin.Email);
                if (existingUser != null)
                {
                    var isPasswordVerified = EncypTool.VerifyPassword(requestLogin.Password, existingUser.Password);
                    if (isPasswordVerified)
                    {
                        var claimList = new List<Claim>();
                        claimList.Add(new Claim(ClaimTypes.Email, existingUser.Email));
                        claimList.Add(new Claim(ClaimTypes.Name, existingUser.Name));

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var expireDate = DateTime.UtcNow.AddDays(1);
                        var timeStamp = TimeStampTool.ConvertToTimeStamp(expireDate);
                        var token = new JwtSecurityToken(
                            claims: claimList,
                            notBefore: DateTime.UtcNow,
                            expires: expireDate,
                            signingCredentials: creds);
                        ResponseLoginUser.Success = true;
                        ResponseLoginUser.Token = new JwtSecurityTokenHandler().WriteToken(token);
                        ResponseLoginUser.ExpireDate = timeStamp;
                    }
                    else
                    {
                        ResponseLoginUser.MessageList.Add("Password is wrong");
                    }
                }
                else
                {
                    ResponseLoginUser.MessageList.Add("Email is wrong");
                }
            }
            return ResponseLoginUser;

        }
    }
}
