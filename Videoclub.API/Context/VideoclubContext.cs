

using Microsoft.EntityFrameworkCore;
using Videoclub.API.Model;

namespace Videoclub.API.Context;
public class VideoclubContext : DbContext
{

    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RentHistory> RentHistories { get; set; }

    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseMySQL("server=localhost;database=videoclub;user=root;password=Clarcat_2023");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>(c =>
        {
            c.HasKey(c => c.Id);
            c.Property(c => c.Name).IsRequired();
        });

        modelBuilder.Entity<Movie>(m =>
        {
            m.HasKey(m => m.Id);
            m.Property(m => m.Title).IsRequired().HasMaxLength(50);
            m.Property(m => m.Description).IsRequired();
            m.Property(m => m.PhotoURL).IsRequired();
            m.Property(m => m.Available).IsRequired();
            m.HasOne(m => m.Category);

        });

        modelBuilder.Entity<User>(u =>
        {
            u.HasKey(m => m.Id);
            u.Property(m => m.Username).IsRequired().HasMaxLength(50);
            u.Property(m => m.Name).IsRequired();
            u.Property(m => m.LastName).IsRequired();
            u.Property(m => m.PasswordHash).IsRequired();
            u.Property(m => m.PasswordSalt).IsRequired();
        });

        modelBuilder.Entity<RentHistory>(h =>
        {
            h.HasKey(m => new { m.UserId, m.MovieId, m.RentDate });
            h.HasOne(m => m.Movie).WithMany(m => m.RentHistories).HasForeignKey(m => m.MovieId);
            h.HasOne(m => m.User).WithMany(m => m.RentHistories).HasForeignKey(m => m.UserId); ;
        });


        //seed data

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Action" },
            new Category { Id = 2, Name = "Comedy" },
            new Category { Id = 3, Name = "Drama" },
            new Category { Id = 4, Name = "Horror" },
            new Category { Id = 5, Name = "Thriller" }
        );

        modelBuilder.Entity<Movie>().HasData(
        new Movie
        {
            Id = 1,
            Title = "The Shawshank Redemption",
            Description = "Some people are in prison and they try to scape",
            PhotoURL = "https://m.media-amazon.com/images/M/MV5BNDE3ODcxYzMtY2YzZC00NmNlLWJiNDMtZDViZWM2MzIxZDYwXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_FMjpg_UX1000_.jpg",
            Available = true,
            CategoryId = 3
        },
        new Movie
        {
            Id = 2,
            Title = "The Revenant",
            Description = "Oscar DiCaprio is in the woods and he is cold",
            PhotoURL = "https://m.media-amazon.com/images/M/MV5BMDE5OWMzM2QtOTU2ZS00NzAyLWI2MDEtOTRlYjIxZGM0OWRjXkEyXkFqcGdeQXVyODE5NzE3OTE@._V1_.jpg",
            Available = true,
            CategoryId = 1
        },
        new Movie 
        {
            Id = 3,
            Title = "The Godfather",
            Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
            PhotoURL = "https://m.media-amazon.com/images/M/MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_FMjpg_UX1000_.jpg",
            Available = true,
            CategoryId = 3

        },
        new Movie 
        {
            Id = 4,
            Title = "Fight Club",
            Description = "Some guys fight",
            PhotoURL = "https://m.media-amazon.com/images/I/51v5ZpFyaFL._AC_.jpg",
            Available = true,
            CategoryId = 1
        }
        );




    }

}
