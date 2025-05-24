using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkTracker.Models;

namespace WorkTracker.Data
{
    public class AddDbContext : DbContext
    {
        public DbSet<WorkStage> WorkStages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=worktracker.db");
        }
    }
}
