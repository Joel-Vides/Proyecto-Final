using System.ComponentModel.DataAnnotations;

namespace Library.API.Dtos.BookshelfA
{
    public class BookshelfACreateDto
    {
        [Display(Name = "Libro")]
        [Required(ErrorMessage = "El {0} es Requerido.")]
        public Guid? BookId { get; set; } = null;

        [Display(Name = "Nombre_del_Libro")]
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "El Campo {0} debe Tener un Minimo de {2} y un Máximo de {1} Caracteres")]
        public string BooksName { get; set; }
    }
}
