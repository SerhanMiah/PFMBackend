using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PFMBackend.Models;

namespace PFMBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        public DbSet<AccountModel> AccountModels {get; set;}

        public DbSet<ApplicationUser> applicationUsers {get; set;}

        public DbSet<CategoryModel> CategoryModels {get; set;}

        public DbSet<BudgetModel> BudgetModels {get; set;}

        public DbSet<TransactionModel> TransactionModels {get; set;}




    }
}