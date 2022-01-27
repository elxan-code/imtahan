using imtahan.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace imtahan.Data
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions <AppDbContext> options):base(options)
        {

        }
        public DbSet<CustomUser> CustomUsers  { get; set; }
        public DbSet<Teams> Teams { get; set; }




    }
}
