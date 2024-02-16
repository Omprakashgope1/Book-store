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
    public class UserBusiness : IUserBusiness
    {
        IUserRepo _userRepo;
        public UserBusiness(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public UserResponse Register(UserRequest user)
        {
            return _userRepo.Register(user);
        }
        public string Login(loginRequest user)
        {
            return _userRepo.Login(user);
        }
        
        public string ForgetPassword(ForgetPasswordRequest request)
        {
            return _userRepo.ForgetPassword(request);
        }
        public bool ResetPassword(ResetPasswordRequest request,long userId)
        {
            return _userRepo.ResetPassword(request,userId);
        }

        public UserResponse getUser(long userId)
        {
            return _userRepo.getUser(userId);
        }
        public UserResponse UpdateUserId(UserRequest req, long userId)
        {
            return _userRepo.UpdateUserId(req,userId);
        }
    }
}
