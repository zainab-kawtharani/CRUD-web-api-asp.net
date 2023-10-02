using Microsoft.EntityFrameworkCore;
using WebApi.Models.Domain;

namespace WebApi.Data
//A DbContext instance represents a session with the database
//and can be used to query and save instances of your entities.
//DbContext is a combination of the Unit Of Work and Repository patterns
{
    public class WalksDbContext: DbContext
    {   // dbcontextoptions: Initializes a new instance of the DbContext class using the specified options. 
        // we want to send our connections through program.cs
        public WalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {
        }

        // db set is a property of db context class that repersent a collection of entities in db   
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
         
        //DI is a design pattern use to increase maintainablity and testability
    }
}


// endpoint====controller
