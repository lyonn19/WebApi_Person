using APIPersons.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIPersons.Repository
{
    public class PersonContext : DbContext
    {
        public PersonContext(
              DbContextOptions<PersonContext> dbContextOptions)
              : base(dbContextOptions) { }


        public DbSet<PersonEntity> PersonEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonEntity>()
                .HasKey(x => x.Id);
        }
    }
}