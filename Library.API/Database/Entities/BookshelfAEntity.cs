using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library.API.Database.Entities.Common;

namespace Library.API.Database.Entities
{
    [Table("Bookshelf_A")]
    public class BookshelfAEntity : BaseEntity
    {
        // Relación con Tabla de Estanteria A
        [Column("book_id")]
        public Guid? BookId { get; set; }

        [Column("book_name")]
        public string BooksName { get; set; }

        [ForeignKey(nameof(BookId))]
        public virtual LibraryEntity BookshelfA { get; set; }

    }
}
