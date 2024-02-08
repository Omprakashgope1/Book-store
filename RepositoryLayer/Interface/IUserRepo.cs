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
        string ForgetPassword(ForgetPasswordRequest request);
        public bool ResetPassword(ResetPasswordRequest request,long userId);
        UserResponse getUser(long userId);
    }
}
