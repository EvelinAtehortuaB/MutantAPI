using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<ADN> ADN { get; set; }
    }
}
