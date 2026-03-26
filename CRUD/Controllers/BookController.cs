using CRUD.DTOs;
using CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        //referencia al EF

        private LibraryContext _context;

        public BookController(LibraryContext context) { _context = context; }


        [HttpGet]

        public async Task<IEnumerable<BookDto>> Get() =>

             await _context.Book.Select(book => new BookDto
             {
                 IdBook = book.IdBook,
                 Title = book.Title,
                 Description = book.Description,
                 Page = book.Page
             }).ToListAsync();

        [HttpGet("{Id}")]

        public async Task<ActionResult<BookDto>> GetById(int id)
        {
            var book = await _context.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var bookDto = new BookDto
            {
                IdBook = book.IdBook,
                Title = book.Title,
                Description = book.Description,
                Page = book.Page
            };

            return Ok(bookDto);
        }

        [HttpPost]

        public async  Task 

             
}
}
