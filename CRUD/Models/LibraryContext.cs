using Microsoft.EntityFrameworkCore;

namespace CRUD.Models
{
    public class LibraryContext: DbContext
    {
        public LibraryContext(DbContextOptions options): base(options) { }

        public DbSet<Book> Book {  get; set; }
        public DbSet<Category> Category {  get; set; }
        public DbSet<CategoryBook> CategoryBook { get; set; }


    }
}
