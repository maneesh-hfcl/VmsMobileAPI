using Microsoft.EntityFrameworkCore;
using Vms.Model;

namespace Vms
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }

        public DbSet<TUser> Users => Set<TUser>(); 
        public DbSet<TNvr> Nvrs => Set<TNvr>();
        public DbSet<TDevice> Devices => Set<TDevice>();
        public DbSet<TDevcfg> Devcfgs => Set<TDevcfg>();
        public DbSet<TMap> Maps => Set<TMap>();
        public DbSet<TEvtd> Evtds => Set<TEvtd>();
        public DbSet<TEvtLog> EvtLogs => Set<TEvtLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TUser>().ToTable("tUsers");
            modelBuilder.Entity<TNvr>().ToTable("tNvr");    
            modelBuilder.Entity<TDevice>().ToTable("tDev");
            modelBuilder.Entity<TDevcfg>().ToTable("tDevCfg");
            modelBuilder.Entity<TMap>().ToTable("tMap");
            modelBuilder.Entity<TEvtd>().ToTable("tEvtd");
            modelBuilder.Entity<TEvtLog>().ToTable("tEvtLog");

        }
    }

}