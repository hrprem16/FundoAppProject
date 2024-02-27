using System;
using Common_Layer.RequestModels;
using Repository_Layer.Entity;

namespace Manager_Layer.Interfaces
{
    public interface IUserManager
    {
        public UserEntity UserRegistration(RegisterModel model);

        public UserEntity UserLogin(LoginModel model);

    }
}

