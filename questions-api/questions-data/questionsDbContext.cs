using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using questions_data.Entities;

namespace questions_data
{
  public class questionsDbContext : DbContext, IquestionsDbContext
  {
    public questionsDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      var userModel = modelBuilder.Entity<User>();

      userModel
        .Property(u => u.Login)
        .IsRequired();

      userModel
        .Property(u => u.Email)
        .IsRequired();

      userModel
        .Property(u => u.PasswordHash)
        .IsRequired();

      userModel
        .Property(u => u.Salt)
        .IsRequired();
    }

    public DbSet<User> Users { get; set; }
  }
}
