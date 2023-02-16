using Microsoft.EntityFrameworkCore;

namespace Vms
{
    public class M3sDb: DbContext
    {
        public M3sDb(DbContextOptions<M3sDb> options):base(options){}

        public DbSet<User> Users => Set<User>();
    }

    public class User
    {
        public int Id { get; set; }
        public string Name {get; set;}

    }
}