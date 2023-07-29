using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Neo.API.Models.Domain;

namespace Neo.API.Data
{
    public class NeoWalksDbContext : DbContext
    {
        public NeoWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

    } 
}
