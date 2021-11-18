using Books.Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Books.Domain.Interfaces
{
    public interface IBooksRepository
    {
        Book[] GetBooks();
        int Insert(Book book);
        int Update(Book book);
        int Delete(int id);
    }
}
