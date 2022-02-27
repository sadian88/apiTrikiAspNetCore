using Microsoft.AspNetCore.Mvc;
using Triki.CI.Interfaces;
using Triki.CI.Dto;
using Triki.BL.Components;
using System.Threading.Tasks;
using Triki.Data.Mysql;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace ApiTriki.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserBL _userBl;

        public UserController(DbContextSqlTriki context, IOptions<AppSettingsDto> appSettings)
        {
            _userBl = new UserBL(context, appSettings);
        }
        [HttpPost]
        public async Task<ActionResult<ResponseBaseDto>> Authenticate(AuthDto data)
        {
            var result = await _userBl.Authenticate(data);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ResponseBaseDto>> Register(UserDto data)
        {
            var result = await _userBl.UserRegister(data);

            return Ok(result);
        }

        [HttpPost("Search")]
        public ActionResult<ResponseBaseDto> Search(UserDto data)
        {
            var result =  _userBl.UserSearch(data.Email);

            return Ok(result);
        }

        [HttpPut("Update")]
        public ActionResult<ResponseBaseDto> Update(UserUpdateDto data)
        {
            var result = _userBl.UpdateOneUser(data);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseBaseDto> Delete(int id)
        {
            var result = _userBl.DeleteUser(id);

            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ResponseBaseDto>> GetAll()
        {
            var result = await _userBl.GetAllUser();

            return Ok(result);
        }

        [HttpGet("RefreshToken")]
        public Task<ActionResult<LoginDto>> RefreshToken()
        {
            throw new System.NotImplementedException();
        }

    }
}
