using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NuocMamBaHai.Models;

namespace NuocMamBaHai.Data
{
    public class NuocMamBaHaiContext : DbContext
    {
        public NuocMamBaHaiContext (DbContextOptions<NuocMamBaHaiContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<NuocMamBaHai.Models.Products> Products { get; set; } = default!;
    }
}
