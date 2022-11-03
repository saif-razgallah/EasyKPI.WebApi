using EasyKPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyKPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<DataWH> DataWH { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportAccess> ReportsAccess { get; set; }
        public DbSet<DataWHAccess> DataWHAccess { get; set; }
        public DbSet<Dashboard> Dashboard { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=Easy-KPI;Integrated Security=True");
        }
    }
}
