using Books.Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Books.Infrastructure.DTO
{
    public class BookDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string author { get; set; }

        public Book GetBook()
        {
            return new Book 
            {
                Author = this.author,
                BookName = this.name,
                Id = this.id
            };
        }
    }
}
