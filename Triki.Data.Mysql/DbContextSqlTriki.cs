using Microsoft.EntityFrameworkCore;
using Triki.CI.Models;

namespace Triki.Data.Mysql
{
    public class DbContextSqlTriki : DbContext
    {
        public DbContextSqlTriki(DbContextOptions<DbContextSqlTriki> options) : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<TipoDocument> TipoDocumento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
