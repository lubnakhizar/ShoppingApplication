using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ShoppingApplication.Models
{
    public class ShoppingContext:DbContext
    {
        //  1- base mai connection string pass ki....jo naam connection string
        //  mai name="ConnectionString" likha ha wohi base mai likhna ha without space...
        public ShoppingContext():base("ConnectionString")
        {

        }
        // 2- Dbset (tables Creation)
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductStatus> ProductStatuses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        // Error (multiple cycle cascade during update database
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}