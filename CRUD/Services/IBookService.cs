using CRUD.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Services
{
    public interface IBookService
    {

        Task<IEnumerable<BookDto>> Get();

        Task<BookDto> GetById(int id);

        Task<BookDto> Add(BookInsertDto bookInsertDto);

        Task<BookDto> Update(int id, BookUpdateDto bookUpdateDto);

        Task<BookDto> Delete(int id);

    }
}
