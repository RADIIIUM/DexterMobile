using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DesktopProject_V3.DataBaseClass
{
    public partial class Model1 : DbContext
    {
        private string _databasePath;
        public Model1(string databasePath)
        {
            _databasePath = databasePath;
        }

        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Orders_Products> Orders_Products { get; set; }
        public virtual DbSet<Orders_Users> Orders_Users { get; set; }
        public virtual DbSet<Privilege> Privilege { get; set; }
        public virtual DbSet<Privilege_Users> Privilege_Users { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Types_Products> Types_Products { get; set; }
        public virtual DbSet<TypesOfProducts> TypesOfProducts { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Warehouses> Warehouses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
