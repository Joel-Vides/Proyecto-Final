using Library.API.Database.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.API.Database.Entities
{
        [Table("Bookshelf_B")]
        public class BookshelfBEntity : BaseEntity
        {
            // Relación con Tabla de Estanteria B
            [Column("book_id")]
            public Guid? BookId { get; set; }

            [Column("book_name")]
            public string BooksName { get; set; }

            [ForeignKey(nameof(BookId))]
            public virtual LibraryEntity BookshelfB { get; set; }

        }
}
