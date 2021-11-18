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

        public int Delete(int id)
        {
            var sqlQuery = $"DELETE FROM public.Books WHERE id={id};";

            return StartSqlCommand(sqlQuery);
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

        public int Insert(Book book)
        {
            var sqlQuery = $"INSERT INTO public.Books (id, name, author) VALUES ({book.Id},'{book.BookName}','{book.Author}');";

            return StartSqlCommand(sqlQuery);
        }

        public int Update(Book book)
        {
            var sqlQuery = $"UPDATE public.Books SET name='{book.BookName}', author='{book.Author}' WHERE id = {book.Id};";

            return StartSqlCommand(sqlQuery);
        }

        private int StartSqlCommand(string sqlQuery)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetSection("Database:DbString").Value))
            {
                using (var command = new NpgsqlCommand(sqlQuery, connection))
                {
                    connection.Open();
                    var result = command.ExecuteNonQuery();

                    return 0;
                }
            }
        }
    }
}
