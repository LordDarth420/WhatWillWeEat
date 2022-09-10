using Microsoft.EntityFrameworkCore;
using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using WhatWillWeEat.Models.Entities;

namespace WhatWillWeEat.Services
{
    public class DatabaseContext : DbContext
    {
        private string _dbPath;
        public DatabaseContext()
        {
            this._dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath
                (System.Environment.SpecialFolder.Personal), "foodDB.db");
        }
        public DatabaseContext(string dbPath)
        {
            this._dbPath = dbPath;
        }

        public DbSet<Food> Foods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={this._dbPath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Food>().HasKey(food => food.ID);
        }
    }
}
