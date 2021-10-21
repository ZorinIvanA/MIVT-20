using Books.Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Books.Domain.Interfaces
{
    public interface IBooksRepository
    {
        public Book[] GetBooks();
    }
}
