using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Softline_Test.Models
{
    public class ObjectivesContext : DbContext
    {
        public DbSet<Objective> Objectives { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public ObjectivesContext(DbContextOptions<ObjectivesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
