using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Repository_Layer.Entity
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int UserId { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

    }
}

