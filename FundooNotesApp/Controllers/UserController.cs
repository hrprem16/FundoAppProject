using System;
using Microsoft.AspNetCore.Mvc;
using Manager_Layer.Interfaces;
using Common_Layer.RequestModels;
using Common_Layer.ResponseModel;
using Repository_Layer.Entity;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
	{
		private readonly IUserManager userManager;

		public UserController(IUserManager userManager)
		{
			this.userManager = userManager;
		}

		[HttpPost]
		[Route("Reg")]

		public ActionResult Register(RegisterModel model)
		{
			var response = userManager.UserRegistration(model);
			if (response != null)
			{
                return Ok(new ResModel<UserEntity> { Success = true, Message = "Registered Successfully", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<UserEntity> { Success = false, Message = "Registration Failed", Data = response });
            }

        }
	}
}

