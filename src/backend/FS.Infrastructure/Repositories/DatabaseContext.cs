﻿using FS.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FS.Infrastructure.Repositories
{
    public class DatabaseContext : DbContext, IUnitOfWork
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Share configuration
            modelBuilder.Entity<Account>().Property(a => a.CreatedOn).IsRequired();
            modelBuilder.Entity<User>().Property(a => a.CreatedOn).IsRequired();
            modelBuilder.Entity<Account>().Property(a => a.CreatedOn).IsRequired();

            //Account
            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<Account>().Property(a => a.Id).IsRequired();
            modelBuilder.Entity<Account>().HasKey(a => a.Id);
            modelBuilder.Entity<Account>().HasOne(a => a.User);
            modelBuilder.Entity<Account>().Property(a => a.UserId).IsRequired();

            //User
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().Property(a => a.Id).IsRequired();
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(20);

            //Expense
            modelBuilder.Entity<Expense>().ToTable("Expenses");
            modelBuilder.Entity<Expense>().Property(e => e.Id).IsRequired();
            modelBuilder.Entity<Expense>().HasKey(e => e.Id);
            modelBuilder.Entity<Expense>().Property(e => e.AccountId).IsRequired();
            modelBuilder.Entity<Expense>().Property(e => e.Value).IsRequired();
            modelBuilder.Entity<Expense>().Property(e => e.Description).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Expense>().HasOne(e => e.Account).WithMany(e => e.Expenses);
            modelBuilder.Entity<Expense>()
                .HasIndex(e => e.AccountId)
                .IncludeProperties(e => e.Id);
        }
    }
}