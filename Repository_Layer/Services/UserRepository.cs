using System;
using Repository_Layer.Context;
using Repository_Layer.Interfaces;
using Repository_Layer.Entity;
using Common_Layer.RequestModels;

namespace Repository_Layer.Services
{
	public class UserRepository:IUserRepository
	{
		private readonly FundoContext context;

		public UserRepository(FundoContext context)
		{
			this.context = context;
		}

        public UserEntity UserRegistration(RegisterModel model)
		{
			//making object of UserEntity 
			UserEntity entity = new UserEntity();
			//set the value to entity that coming from User/Postman
			entity.FName = model.FName;
			entity.LName = model.LName;
            entity.UserEmail = model.UserEmail;
            entity.UserPassword = model.UserPassword;

			context.UserTable.Add(entity);
			context.SaveChanges();

            return entity;
		}

    }
}

