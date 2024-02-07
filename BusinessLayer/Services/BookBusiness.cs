using BusinessLayer.Interface;
using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class BookBusiness:IBookBusiness
    {
        private IBookRepo bookRepo;
        public BookBusiness(IBookRepo bookRepo) 
        {
            this.bookRepo = bookRepo;
        }

        public List<BookResponse> getAllBooks()
        {
            return bookRepo.getAllBooks();
        }

        public BookResponse getBookById(int id)
        {
            return bookRepo.GetBook(id);
        }
        public BookResponse bookDetailsByAuthor(BookAuthorRequest req)
        {
            return bookRepo.bookDetailsByAuthor(req);
        }
    }
}
