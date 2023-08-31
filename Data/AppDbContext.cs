using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PFMBackend.Models;

namespace PFMBackend.Data
{
    public class AppDbContext :  IdentityDbContext<ApplicationUser> 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }


        // DbSet


        public DbSet<AccountModel> AccountModels {get; set;}

        public DbSet<ApplicationUser> applicationUsers {get; set;}

        public DbSet<CategoryModel> CategoryModels {get; set;}

        public DbSet<BudgetModel> BudgetModels {get; set;}

        public DbSet<TransactionModel> TransactionModels {get; set;}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AccountModel>()
            .Property(a => a.Balance)
            .HasColumnType("decimal(18,2)"); 

        modelBuilder.Entity<ApplicationUser>()
            .Property(u => u.TotalBalance)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<BudgetModel>()
            .Property(b => b.Amount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<FinanceGoal>()
            .Property(f => f.TargetAmount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<TransactionModel>()
            .Property(t => t.Amount)
            .HasColumnType("decimal(18,2)");
    }


    }
}