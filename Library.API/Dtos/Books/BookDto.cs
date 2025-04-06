using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Dtos.Books
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public int Volume { get; set; }
        public string Publisher { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
