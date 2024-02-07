using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserRepo
    {
        public UserResponse Register(UserRequest user);
        public string Login(loginRequest user);
        public void AddAddress(AddAddressRequest address);
        string ForgetPassword(ForgetPasswordRequest request);
        public bool ResetPassword(ResetPasswordRequest request,long userId);    
    }
}
