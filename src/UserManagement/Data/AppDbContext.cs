﻿using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Data
{
    public class AppDbContext : DbContext 
    {
        public DbSet<User> Users { get; set; } 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 
    }
}
