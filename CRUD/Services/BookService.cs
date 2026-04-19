using CRUD.DTOs;
using CRUD.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CRUD.Services
{
    public class BookService : IBookService
    {
        LibraryContext _libraryContext;

        public BookService(LibraryContext libraryContext)
        { 

            _libraryContext = libraryContext;
        }

        #region get request
        public async Task<IEnumerable<BookDto>> Get() => await _libraryContext.Book.Select(book => new BookDto
        {
            IdBook = book.IdBook,
            Title = book.Title,
            Description = book.Description,
            Page = book.Page
        }).ToListAsync();

        public async Task<BookDto> GetById(int id)
        {
            var book = await _libraryContext.Book.FindAsync(id);

            if (book != null)
            {
                var bookDto = new BookDto
                {
                    IdBook = book.IdBook,
                    Title = book.Title,
                    Description = book.Description,
                    Page = book.Page
                    
                };
                return bookDto;
            }
            return null;
        }

        #endregion

        #region Post requests
        public async Task<BookDto> Add(BookInsertDto bookInsertDto)
        {
            var book = new Book()
            {

                Title = bookInsertDto.Title,
                Description = bookInsertDto.Description,
                Page = bookInsertDto.Page

            };

            await _libraryContext.Book.AddAsync(book);
            await _libraryContext.SaveChangesAsync();


            var bookDto = new BookDto
            {
                IdBook = book.IdBook,
                Title = book.Title,
                Description = book.Description,
                Page = book.Page
            };

            return bookDto;
        }

        #endregion

        public async Task<BookDto> Update(int id, BookUpdateDto bookUpdateDto)
        {
            var book = await _libraryContext.Book.FindAsync(id);

            if (book != null)
            {
                book.Description = bookUpdateDto.Description;
                book.Title = bookUpdateDto.Title;
                book.Page = bookUpdateDto.Page;

                await _libraryContext.SaveChangesAsync();

                var BookDto = new BookDto
                {
                    IdBook = book.IdBook,
                    Title = book.Title,
                    Description = book.Description,
                    Page = book.Page
                };

                return BookDto;
            }

            return null;

        }

        public async Task<BookDto> Delete(int id)
        {
            var book = await _libraryContext.Book.FindAsync(id);

            if (book != null)
            {
                var BookDto = new BookDto
                {
                    IdBook = book.IdBook,
                    Title = book.Title,
                    Description = book.Description,
                    Page = book.Page
                };


                _libraryContext.Book.Remove(book);
                await _libraryContext.SaveChangesAsync();

                return BookDto;
            }

            return null;
        }

        

      
    }
}
