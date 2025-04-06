using Library.API.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Database
{
    public class LibraryDBContext : DbContext
    {
        public LibraryDBContext(DbContextOptions options) : base(options)
        {
            
        }
        //Para la Tabla de los Libros
        public DbSet<LibraryEntity> Library { get; set; }
    }
}
