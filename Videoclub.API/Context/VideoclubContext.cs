

using Microsoft.EntityFrameworkCore;
using Videoclub.API.Model;

namespace Videoclub.API.Context;
public class VideoclubContext : DbContext
{

    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users {  get; set; }
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
            u.Property(m => m.passwordHash).IsRequired();
            u.Property(m => m.passwordSalt).IsRequired();
        });

        modelBuilder.Entity<RentHistory>(h =>
        {
            h.HasKey(m => new { m.user_id, m.movie_id, m.RentDate });
            h.HasOne(m => m.Movie).WithMany(m => m.RentHistories).HasForeignKey(m => m.movie_id);
            h.HasOne(m => m.User).WithMany(m => m.RentHistories).HasForeignKey(m => m.user_id); ;
        });

       


    }

}
