using Books.Books.Domain.Entities;
using Books.Books.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Books.Books.Controllers
{
    [Route("api/v1/books")]
    public class BooksController : ControllerBase
    {
        private IBooksRepository _booksRepository;
        private ILogger<BooksController> _logger;

        public BooksController(IBooksRepository booksRepository, ILogger<BooksController> logger)
        {
            _booksRepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_booksRepository.GetBooks());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "We have a problem!");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            try
            {
                if (book == null)
                    return StatusCode(StatusCodes.Status400BadRequest);

                if (string.IsNullOrWhiteSpace(book.BookName))
                    return BadRequest("Book name is empty");

                return Ok(_booksRepository.Insert(book));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "We have a problem!");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Book book)
        {
            try
            {
                if (book == null)
                    return StatusCode(StatusCodes.Status400BadRequest);

                if (string.IsNullOrWhiteSpace(book.BookName))
                    return BadRequest("Book name is empty");

                return Ok(_booksRepository.Update(book));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "We have a problem!");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_booksRepository.Delete(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "We have a problem!");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        [HttpGet("forecast")]
        public async Task<IActionResult> GetAnother()
        {
            //weatherforecast

            using (HttpClient client = new HttpClient())
            {
                var message = await client.GetAsync("https://localhost:49157/weatherforecast");

                return Ok(await message.Content.ReadAsStringAsync());
            }
        }
    }
}
