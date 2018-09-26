using Microsoft.EntityFrameworkCore;
using System.Collections.Generic; 
using System;

namespace Practice1.Model {
    
    // creating the context.
    public class MovieContext: DbContext {

        // assigns the entity to a method, so the developer can easily use this to reference it.
        public DbSet<Movie> Movies {get; set;} 
        public DbSet<Actor> Actors {get; set;}

        // Implementing the mapping rules by using the OnModelCreating(ModelBuilder modelBuilder) method, with the intention of establishing a Many-to-Many relationship.
            // protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //     modelBuilder.Entity<MovieActor>().HasKey( k => new {k.ActorId, k.MovieId } ); // establishing Keys from the two entities.
            //     modelBuilder.Entity<MovieActor>().HasOne( m => m.Movie ).WithMany( a => a.Actors).HasForeignKey( m => m.MovieId); // One movie has many actors with foreign key from Movie (MovieId)
            //     modelBuilder.Entity<MovieActor>().HasOne( a => a.Actor ).WithMany( m => m.Movies).HasForeignKey( a => a.ActorId); // One actor has many movies with foreign key from Actor (ActorId)
            // }

        // Implementing One-to-One relationship. (One movie with one actor.)
            // protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // modelBuilder.Entity<MovieActor>().HasOne( m => m.Movie).WithOne( a => a.Actors).HasForeignKey( a => a.ActorId);
            // }

        // Implementing One-to-Many relationship. (One movie with many actors.)
            // protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //     modelBuilder.Entity<MovieActor>().HasOne( m => m.Movie).WithMany(a => a.Actors).HasForeignKey(m => m.MovieId);
            // }
    
        // establishing a connection with our specific database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=;Host=localhost;Port=5432;Database=Practice1;Pooling=true;"); // Remember this (Npgsql credentials) and study the SQLite credentials too.
        
        }
    }
        
        // (IMPORTANT!) This is where we show that we have an "interest" to map the two entities together to establish an intention for the relationship with the two entities.
        // public class MovieActor {
        //     public int MovieId {get; set;}
        //     public Movie Movie {get; set;}
        //     public int ActorId {get; set;}
        //     public Actor Actor {get; set;}
        // }
    
        // creating the Movie class which is also an entity.
        public class Movie {
            // defining the attributes of the entity.
            public int Id {get; set;}
            public string Title {get; set;}
            public string ReleaseYear {get; set;}
            public virtual List<Actor> Actors {get; set;} 
            // public Actor Actor {get; set;} // We add this attribute when we want to establish an One-to-Many and One-to-One relationships.
        }

        // creating the Actor class which is also an entity.
        public class Actor {
            // defining the attributes of the entity.
            public int Id {get; set;}
            public string Name {get; set;}
            public string Gender {get; set;}
            public string Birthdate {get; set;}
            // public virtual List<MovieActor> Movies {get; set;} // We add this so we can accomodate the Many-to-Many intention.
            public Movie Movie {get; set;} // We add this attribute when we want to establish an One-to-Many and One-to-One relationships.
            public int MovieId {get; set;}
        }

    }