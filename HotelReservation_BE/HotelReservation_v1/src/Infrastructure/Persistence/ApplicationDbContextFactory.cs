using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<AplicationDbContext>
    {
        public AplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AplicationDbContext>();

            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS; DataBase=hotel_reservation; Integrated Security=True;TrustServerCertificate=True;");

            return new AplicationDbContext(optionsBuilder.Options);
        }
    }
}
