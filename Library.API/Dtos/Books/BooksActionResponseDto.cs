﻿namespace Library.API.Dtos.Books
{
    public class BooksActionResponseDto
    {
        public Guid Id { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public int Volume { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
    }
}
