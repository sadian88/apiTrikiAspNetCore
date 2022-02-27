using System.Threading.Tasks;
using Triki.CI.Dto;
using Triki.Data.Mysql;
using Triki.Data.Sql.Operations;

namespace Triki.BL.Components
{
    public class TipoDocumentBL
    {
        private readonly TipoDocumentDB tipoDocumentoBD;

        public TipoDocumentBL(DbContextSqlTriki contex)
        {
            tipoDocumentoBD= new TipoDocumentDB(contex);
        }

        public async Task<ResponseBaseDto> GetAllTipo()
        {
            var result = await tipoDocumentoBD.All();
            return new ResponseBaseDto
            {
                sucess= true,
                message="Listado de tipos",
                data=result
            };
        }
    }
}
