using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Triki.CI.Models;
using Triki.Data.Mysql;

namespace Triki.Data.Sql.Operations
{
    public class TipoDocumentDB
    {
        private readonly DbContextSqlTriki db;

        public TipoDocumentDB(DbContextSqlTriki context)
        {
            db = context;
        }

        public async Task<List<TipoDocument>> All()
        {
            return await db.TipoDocumento.ToListAsync();
        }
    }
}
