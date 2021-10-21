using Books.Books.Domain.Entities;
using Books.Books.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Books.Controllers
{
    [Route("api/v1/books")]
    public class BooksController : ControllerBase
    {
        private IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_booksRepository.GetBooks());
        }
    }
}
