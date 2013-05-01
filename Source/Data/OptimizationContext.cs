using System.Data.Entity;

using Data.Models;

namespace Data
{
    public class OptimizationContext : DbContext
    {
        static OptimizationContext()
        {
            Database.SetInitializer(new ContextInitializer());
        }

        public OptimizationContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public OptimizationContext()
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Publisher>().HasMany(publisher => publisher.Books).WithRequired(book => book.Publisher);

            modelBuilder.Entity<Book>().HasMany(book => book.Authors).WithMany(author => author.Books);
        }
    }
}
