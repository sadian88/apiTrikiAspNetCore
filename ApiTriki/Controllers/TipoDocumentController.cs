using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Triki.BL.Components;
using Triki.CI.Dto;
using Triki.Data.Mysql;

namespace ApiTriki.Controllers
{
    [Route("api/Doc")]
    [ApiController]
    public class TipoDocumentController : ControllerBase
    {
        private readonly TipoDocumentBL _tipoDocumentBl;
        public TipoDocumentController(DbContextSqlTriki context)
        {
            _tipoDocumentBl = new TipoDocumentBL(context);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ResponseBaseDto>> GetAll()
        {
            var result = await _tipoDocumentBl.GetAllTipo();

            return Ok(result);
        }
    }
}
