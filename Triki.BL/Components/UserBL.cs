using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Triki.CI.Dto;
using Triki.CI.Interfaces;
using Triki.CI.Models;
using Triki.Data.Mysql;
using Triki.Data.Mysql.Operations;

namespace Triki.BL.Components
{
    public class UserBL : IAuth
    {
        private readonly UserDB userDB;
        private readonly AppSettingsDto _appSettings;

        public UserBL(DbContextSqlTriki contex, IOptions<AppSettingsDto> appSettings)
        {
            userDB= new UserDB(contex);
            _appSettings = appSettings.Value;
        }


        public async Task<ResponseBaseDto> Authenticate(AuthDto data)
        {

            return await Login( data.user, data.password );
        }

        public async Task<ResponseBaseDto> UserRegister(UserDto data)
        {

            return await Register(data);
        }

        public async Task<ResponseBaseDto> GetAllUser()
        {
            var result = await userDB.All();
            return new ResponseBaseDto
            {
                sucess= true,
                message="Listado de usuarios",
                data=result
            };
        }

        public ResponseBaseDto UserSearch(string email)
        {
            try
            {

                var result = userDB.Search(email);
                return new ResponseBaseDto(
                     true,
                     null,
                     result
                    );
            }
            catch (Exception ex)
            {
                return new ResponseBaseDto(
                    false,
                    ex.Message,
                    null
                   );
            }
        }

        public ResponseBaseDto UpdateOneUser(UserUpdateDto data)
        {
            try
            {
                User user = new User();
                user.Name = data.Name;
                user.Email = data.Email;
                user.Password = data.Password;
                user.IndentityNumber = Convert.ToInt32(data.IndentityNumber);
                user.TipoIdenID =  Convert.ToInt32(data.TipoIdenID);
                user.LastName = data.LastName;
                user.Id = data.id;


                var result = userDB.UpdateOne( data.id, user);
                return new ResponseBaseDto(
                     true,
                     null,
                     result
                    );
            }
            catch (Exception ex)
            {
                return new ResponseBaseDto(
                    false,
                    ex.Message,
                    null
                   );
            }
        }

        public ResponseBaseDto DeleteUser(int id)
        {
            try
            {

                var result = userDB.Delete(id);
                return new ResponseBaseDto(
                     true,
                     null,
                     result
                    );
            }
            catch (Exception ex)
            {
                return new ResponseBaseDto(
                    false,
                    ex.Message,
                    null
                   );
            }
        }

        public Task<LoginDto> RefreshToken()
        {
            throw new NotImplementedException();
        }

        #region Private metho
        private async Task<ResponseBaseDto> Login(string username, string password)
        {

            try
            {
                if (await userDB.Auth(username, password))
                {
                    return new ResponseBaseDto
                    {
                        sucess = true,
                        message = "Ok!!",
                        data = new LoginDto()
                        {
                            AuthenticationType = "Bearer",
                            Token = BuildToken(1)
                        }
                    };
                }
                else
                {
                    return new ResponseBaseDto
                    {
                        sucess = false,
                        message = "Por favor verifiue sus datos"
                    };
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);

                return new ResponseBaseDto
                {
                    sucess = false,
                    message = "Falla al consultar el usuario"
                };
            }

        }
        private async Task<ResponseBaseDto> Register(UserDto data)
        {

            try
            {
                User user = new User();
                user.Name = data.Name;  
                user.Email = data.Email;    
                user.Password = data.Password;  
                user.IndentityNumber = Convert.ToInt32(data.IndentityNumber);    
                user.TipoIdenID =  Convert.ToInt32(data.TipoIdenID);
                user.LastName = data.LastName;  
                
                var result = await userDB.AddOne(user);
                if (result != null)
                {
                    return new ResponseBaseDto
                    {
                        sucess = true,
                        message = "Registro realizado !",
                        data = result
                    };
                }

                return new ResponseBaseDto
                {
                    sucess = false,
                    message = "Fal !",
                    data = null
                };
            }
            catch (Exception e)
            {
                Console.Write(e.Message);

                return new ResponseBaseDto
                {
                    sucess = false,
                    message = "Falla al consultar el usuario"
                };
            }

        }

        private string BuildToken(int id)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                SecurityTokenDescriptor tokenDecriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials =
                        new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _appSettings.Issuer,
                    Audience = _appSettings.Audience
                };

                SecurityToken token = tokenHandler.CreateToken(tokenDecriptor);
                string tokenString = tokenHandler.WriteToken(token);

                return tokenString;
            }
            catch (Exception e)
            {
                string msgError = $"Error al autenticar en la plataforma: {e.Message} ";
                Console.WriteLine(msgError);
                //throw new Exception(msgError);
                return "error";
            }
        }
        #endregion
    }
}
