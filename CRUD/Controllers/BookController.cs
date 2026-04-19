using CRUD.DTOs;
using CRUD.Models;
using CRUD.Services;
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
        private IValidator<BookUpdateDto> _bookUpdateValidator;
        private IBookService _bookService;

        public BookController(LibraryContext context,IBookService bookService, IValidator<BookInsertDto> bookInsertValidator, IValidator<BookUpdateDto> bookUpdateValidator) 
        { 
            _context = context;
            _bookService = bookService;
            _bookInsertValidator = bookInsertValidator; 
            _bookUpdateValidator = bookUpdateValidator;
        }

        #region Get Requets
        [HttpGet]

        public async Task<IEnumerable<BookDto>> Get() =>
          await  _bookService.Get();
            

        [HttpGet("{Id}")]

        public async Task<ActionResult<BookDto>> GetById(int id)
        {
            var bookDto = await _bookService.GetById(id);

            return bookDto == null ? NotFound() : Ok(bookDto);
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

            var bookDto = await _bookService.Add(bookInsertDto);


            return CreatedAtAction(nameof(GetById), new { id = bookDto.IdBook }, bookDto);
        }

        #endregion

        #region Put Requests
        [HttpPut("{id}")]

        public async Task<ActionResult<BookDto>> Update(int id, BookUpdateDto bookUpdateDto)
        {

            var validationResult = await _bookUpdateValidator.ValidateAsync(bookUpdateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors); 
            }

            var bookDto = await _bookService.Update(id, bookUpdateDto);

            return bookDto == null ? NotFound() : Ok(bookDto);

        }

        #endregion

        #region Delete Requests
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookDto>> Delete(int id)
        {
           var bookDto = await _bookService.Delete(id);
            
            return bookDto == null ? NotFound(bookDto) : Ok(bookDto);
        }

        #endregion
















    }


}
