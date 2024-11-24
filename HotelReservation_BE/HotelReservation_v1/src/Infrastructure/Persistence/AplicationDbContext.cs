using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        { }

        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<DetailReservation> DetailsReservations => Set<DetailReservation>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<User> Users => Set<User>();
    }
}
