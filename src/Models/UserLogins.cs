using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class User
    {       
       
            [Required]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }

            public User()
            {

            }
       


        //[Required]
        //public string UserName { get; set; }
        //[Required]
        //public string PasswordHash { get; set; }

        //public UserLogins()
        //{

        //}
    }
}
