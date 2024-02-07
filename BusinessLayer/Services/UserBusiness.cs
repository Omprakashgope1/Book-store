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
        public void AddAddress(AddAddressRequest address)
        {
           _userRepo.AddAddress(address);
        }
        public string ForgetPassword(ForgetPasswordRequest request)
        {
            return _userRepo.ForgetPassword(request);
        }
        public bool ResetPassword(ResetPasswordRequest request,long userId)
        {
            return _userRepo.ResetPassword(request,userId);
        }

    }
}
