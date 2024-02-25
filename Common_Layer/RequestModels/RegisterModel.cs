using System;
namespace Common_Layer.RequestModels
{
	public class RegisterModel
	{
        public string FName { get; set; }

        public string LName { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }
    }
}

