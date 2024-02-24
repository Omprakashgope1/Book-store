using BusinessLayer.Interface;
using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ReviewBusiness : IReviewBusiness
    {
        private IReviewRepo reviewRepo;
        public ReviewBusiness(IReviewRepo reviewRepo)
        {
            this.reviewRepo = reviewRepo;
        }
        public IEnumerable<ReviewResponse> AddReviews(long userId,AddReview reviews)
        {
            return reviewRepo.AddReviews(userId,reviews);
        }
        public IEnumerable<ReviewResponse> GetAllReviews(long bookId)
        {
            return reviewRepo.GetAllReviews(bookId);
        }
        public IEnumerable<ReviewResponse> GetReviews()
        {
            return reviewRepo.GetReviews();
        }
    }
}
