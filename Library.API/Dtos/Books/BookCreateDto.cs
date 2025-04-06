using System.ComponentModel.DataAnnotations;

namespace Library.API.Dtos.Books
{
    public class BookCreateDto
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El Campo {0} debe Tener un Minimo de {2} y un Máximo de {1} Caracteres")]
        public string BookName { get; set; }

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El Campo {0} debe Tener un Minimo de {2} y un Máximo de {1} Caracteres")]
        public string Author { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "El Campo {0} debe Tener un Minimo de {2} y un Máximo de {1} Caracteres")]
        public string Type { get; set; }

        [Display(Name = "Volumen")]
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        public int Volume { get; set; }

        [Display(Name = "Editorial")]
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "El Campo {0} debe Tener un Minimo de {2} y un Máximo de {1} Caracteres")]
        public string Publisher { get; set; }

        [Display(Name = "Fecha_de_Publicacion")]
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        public DateTime PublicationDate { get; set; }
    }
}
