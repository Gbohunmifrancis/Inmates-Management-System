using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): DbContext(options)
        {
            public DbSet<Inmate> Inmates { get; set; }
            public DbSet<GeneralDepartment> GeneralDepartments { get; set; }
        }
    }
}
