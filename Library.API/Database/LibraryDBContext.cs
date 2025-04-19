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

        //Para la Tabla de Estanteria A
        public DbSet<BookshelfAEntity> BookshelfA { get; set; }

        //Para la Tabla de Estanteria B
        public DbSet<BookshelfBEntity> BookshelfB { get; set; }

        //Para la tabla de estanteria C
        public DbSet<BookshelfCEntity> BookshelfC { get; set; }
    }
}
