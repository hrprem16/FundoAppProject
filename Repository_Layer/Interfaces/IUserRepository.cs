using System;
using Repository_Layer.Entity;
using Common_Layer.RequestModels;
namespace Repository_Layer.Interfaces
{
    public interface IUserRepository
    {
        public UserEntity UserRegistration(RegisterModel model);

        public UserEntity UserLogin(LoginModel model);


    }
}

