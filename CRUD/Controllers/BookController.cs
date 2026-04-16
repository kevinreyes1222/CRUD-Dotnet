using CRUD.DTOs;
using CRUD.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        //referencia al EF

        private LibraryContext _context;
        private IValidator<BookInsertDto> _bookInsertValidator;

        public BookController(LibraryContext context, IValidator<BookInsertDto> bookInsertValidator) 
        { 
            _context = context;
            _bookInsertValidator = bookInsertValidator; 
        }

        #region Get Requets
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

        #endregion

        #region Post Requests
        [HttpPost]

        public async Task<ActionResult<BookDto>> Add(BookInsertDto bookInsertDto) 
        {
            var validationResult = await _bookInsertValidator.ValidateAsync(bookInsertDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var book = new Book() {

                Title = bookInsertDto.Title,
                Description = bookInsertDto.Description,
                Page = bookInsertDto.Page

            };

            await _context.Book.AddAsync(book);
            await _context.SaveChangesAsync();

            var bookDto = new BookDto
            {
                IdBook = book.IdBook,
                Title = book.Title,
                Description = book.Description,
                Page = book.Page
            };

            return CreatedAtAction(nameof(GetById), new { id = book.IdBook }, bookDto);
        }

        #endregion

        #region Put Requests
        [HttpPut("{id}")]

        public async Task<ActionResult<BookDto>> Update(int id, BookInsertDto bookInsertDto)
        {
            var book = await _context.Book.FindAsync(id);

            if(book == null)
            {
                return NotFound();
            }

            book.Description = bookInsertDto.Description;
            book.Title = bookInsertDto.Title;
            book.Page = bookInsertDto.Page;
            await _context.SaveChangesAsync();


            var BookDto = new BookDto
            {
                IdBook = book.IdBook,
                Title = book.Title,
                Description = book.Description,
                Page = book.Page
            };

            return Ok(BookDto);


        }

        #endregion

        #region Delete Requests
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var book = await _context.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound();

            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return Ok();
        }

        #endregion
















    }


}
