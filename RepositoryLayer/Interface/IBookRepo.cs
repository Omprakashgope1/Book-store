using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IBookRepo
    {
        public BookResponse GetBook(int id);
        List<BookResponse> getAllBooks();
        public BookResponse bookDetailsByAuthor(BookAuthorRequest req);
    }
}
