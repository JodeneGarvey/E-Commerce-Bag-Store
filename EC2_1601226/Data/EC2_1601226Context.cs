using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EC2_1601226.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EC2_1601226.Data
{
    public class EC2_1601226Context : IdentityDbContext<ApplicationUser>
    {
        public EC2_1601226Context(DbContextOptions<EC2_1601226Context> options)
            : base(options)
        {
        }


        public DbSet<EC2_1601226.Models.Bag> Bag { get; set; }

        public DbSet<EC2_1601226.Models.ShoppingCartItems> ShoppingCartItems { get; set; }

        public DbSet<EC2_1601226.Models.Order> Order { get; set; }

        public DbSet<EC2_1601226.Models.OrderDetail> OrderDetails { get; set; }

        


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           

            foreach (var foreignKey in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
