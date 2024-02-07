using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IBookBusiness
    {
        List<BookResponse> getAllBooks();
        public BookResponse getBookById(int id);
        public BookResponse bookDetailsByAuthor(BookAuthorRequest req);
    }
}
