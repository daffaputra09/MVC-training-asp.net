
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebStatisMVC.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        { }
        public DbSet<Articles> Articles { get; set; }
    }
}
