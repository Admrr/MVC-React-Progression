using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace ReactProject.Model
{

    // creating the context.
    public class DBContext : DbContext
    {

        // The line below is needed to reference to the options from the Startup.cs file, so we can establish a connection to the database and reference it from the model.cs file.
        public DBContext() { }
        public DBContext(DbContextOptions<DBContext> options) : base(options) { } // "base" inherits the constructor of the class, so in this case it references to the DbContext class.
        // assigns the entity to a method, so the developer can easily use this to reference it.
        public DbSet<Company> Companies { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<StaffTask> StaffTasks { get; set; }

        // Implementing the mapping rules by using the OnModelCreating(ModelBuilder modelBuilder) method, with the intention of establishing a Many-to-Many relationship.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StaffTask>().HasKey(st => new { st.StaffId, st.TaskId }); // establishing Keys from the two entities.
            modelBuilder.Entity<StaffTask>().HasOne(s => s.Staff).WithMany(t => t.StaffTask).HasForeignKey(st => st.StaffId);
            modelBuilder.Entity<StaffTask>().HasOne(t => t.Task).WithMany(s => s.StaffTask).HasForeignKey(st => st.TaskId);
            modelBuilder.Entity<Company>().HasMany(c => c.Staff).WithOne(s => s.Company).HasForeignKey(c => c.CompanyId);
        }



        // establishing a connection with our specific database (not needed anymore due to startup.cs file.)
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        //     if(!optionsBuilder.IsConfigured) {
        //     optionsBuilder.UseNpgsql("User ID=postgres;Password=;Host=localhost;Port=5432;Database=ReactProject;Pooling=true;"); // Remember this (Npgsql credentials) and study the SQLite credentials too.
        //     }
        // }
    }

    public class Company
    {
        // defining the attributes of the entity.
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual List<Staff> Staff { get; set; }

    }

    public class Staff
    {
        // defining the attributes of the entity.
        public int Id { get; set; }
        public int SSN { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public int Phone { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public ICollection<StaffTask> StaffTask { get; set; }

    }

    public class Task
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public ICollection<StaffTask> StaffTask { get; set; }


    }

    public class StaffTask
    {
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
    }

}