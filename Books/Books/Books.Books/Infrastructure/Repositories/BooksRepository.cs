using Books.Books.Domain.Entities;
using Books.Books.Domain.Interfaces;
using Books.Books.Infrastructure.DTO;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Books.Infrastructure.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        IConfiguration _configuration;
        public BooksRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Book[] GetBooks()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetSection("Database:DbString").Value))
            {
                using (var command = new NpgsqlCommand("SELECT * FROM public.Books", connection))
                {
                    connection.Open();
                    var result = command.ExecuteReader();

                    List<BookDTO> books = new List<BookDTO>();

                    while (result.Read())
                    {
                        books.Add(new BookDTO
                        {
                            id = (int)result["id"],
                            name = result["name"].ToString(),
                            author = result["author"].ToString()
                        });
                    }

                    return books.Select(x => x.GetBook()).ToArray();
                }
            }
        }
    }
}
