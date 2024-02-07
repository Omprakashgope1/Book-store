using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IReviewRepo
    {
        public AddReview AddReviews(long userId,AddReview reviews);
        public IEnumerable<ReviewResponse> GetAllReviews(long bookId);
    }
}
