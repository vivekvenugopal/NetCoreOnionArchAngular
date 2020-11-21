using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Demo.Common;
using Demo.Model;

namespace Demo.DAL {
  public partial class DemoDbContext : DbContext {
    private IConfiguration _configuration;
    public virtual void Commit () {
      base.SaveChanges ();
    }
    public DemoDbContext () {

    }

    public DemoDbContext (DbContextOptions<DemoDbContext> options) : base (options) { }
    protected override void OnModelCreating (ModelBuilder modelBuilder) {
      base.OnModelCreating (modelBuilder);

      modelBuilder.Entity<User> ();
      modelBuilder.Entity<Employee> ();
      modelBuilder.Entity<Skill> ();
      modelBuilder.Entity<UserPassword> ();
      modelBuilder.Entity<UserRegistration> ();

      Mappings (modelBuilder);
      SeedData (modelBuilder);
    }
    private void SeedData (ModelBuilder modelBuilder) {
      //Seeding super admin data
      modelBuilder.Entity<User> ().HasData (new User {
        Id = 1, FirstName = "SuperAdmin", LastName = "SuperAdmin", CreatedBy = 1, IsSuperAdmin = true,
          CreatedDate = DateTime.Now, Email = "", UserName = "SuperAdmin", IsActive = true
      });
      modelBuilder.Entity<UserPassword> ().HasData (new UserPassword {
        Id = 1,  CreatedBy = 1, CreatedDate = DateTime.Now, UserId = 1,
        Password = "2fb98c46650e0684addfdd1684376af49cf7103836397c0cc688075bd7dc4b74", 
        HashSalt ="b773faade04c9d5ef57ba05c67d728ec"
      });
    }
    private void Mappings (ModelBuilder modelBuilder) {

      modelBuilder.Entity<Employee> ()
        .HasMany (c => c.Skills)
        .WithOne (e => e.Employee)
        .OnDelete (DeleteBehavior.Cascade);

      modelBuilder.Entity<User> ()
        .Ignore (b => b.IsSuperAdmin);

      modelBuilder.Entity<User> ()
        .Ignore (b => b.IsInternalUser);

      modelBuilder.Entity<User> ()
        .Ignore (b => b.IsActive);

      modelBuilder.Entity<UserRegistration> ()
        .Ignore (b => b.IsInternalUser);

      modelBuilder.Entity<UserRegistration> ()
        .Ignore (b => b.IsActivated);

      modelBuilder.Entity<UserPassword> ()
        .HasOne (s => s.User)
        .WithMany ()
        .HasForeignKey (e => e.UserId);

      modelBuilder.Entity<UserPassword> ()
        .HasOne (s => s.User)
        .WithMany ()
        .HasForeignKey (e => e.UserId);

    }
    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
      string connectionString = ApplicationManagement.ConnectionString;
      optionsBuilder.UseMySQL (connectionString);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Skill> Skill { get; set; }
    public DbSet<UserPassword> UserPassword { get; set; }
    public DbSet<UserRegistration> UserRegistration { get; set; }
  }
}