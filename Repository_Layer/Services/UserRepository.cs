using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common_Layer.RequestModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Repository_Layer.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly FundoContext context;
        private readonly IConfiguration _config;

        public UserRepository(FundoContext context, IConfiguration config)
        {
            this.context = context;
            this._config = config;
        }

        public UserEntity UserRegistration(RegisterModel model)
        {
            if (context.UserTable.FirstOrDefault(a => a.UserEmail == model.UserEmail) == null)
            {
                //making object of UserEntity 
                UserEntity entity = new UserEntity();
                //set the value to entity that coming from User(Postman or Swagger)
                entity.FName = model.FName;
                entity.LName = model.LName;
                entity.UserEmail = model.UserEmail;
                entity.UserPassword = BCrypt.Net.BCrypt.HashPassword(model.UserPassword);

                context.UserTable.Add(entity);
                context.SaveChanges();

                return entity;
            }

            else
            {

                throw new Exception("User Already Exists ,Enter another id for Registration");
            }
        }


        public UserEntity UserLogin(LoginModel model)
        {
            var user = context.UserTable.FirstOrDefault(u => u.UserEmail == model.UserEmail);
            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(model.UserPassword, user.UserPassword))
                {
                    return user;
                }
                else
                {
                    //return null;
                    throw new Exception("Invalid Password");
                }
            }
            else
            {
                // if User not found 
                throw new Exception("User not Found");

            }


        }

        public string GenerateToken(string UserEmail, int UserId)
        {
            //Defining a Security Key 
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",UserEmail),
                new Claim("UserId", UserId.ToString())
            };
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Token expiration time
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;

        }

        public ForgetPasswordModel ForgetPassword(string UserEmail)
        {
            UserEntity User = context.UserTable.FirstOrDefault(x => x.UserEmail == UserEmail);
            ForgetPasswordModel forgetPassword = new ForgetPasswordModel();
            forgetPassword.Ema = User.UserEmail;
            forgetPassword.UserId = User.UserId;
            forgetPassword.Token = GenerateToken(Email, User.UserId);
            return forgetPassword;
        }

        public bool CheckUser(string Email)
        {
            if (context.UserTable.FirstOrDefault(a => a.UserEmail == Email) == null) return false;
            return true;
        }

    }
}

