using System.ComponentModel.DataAnnotations;

namespace Pressford.Web.Models
{
    public class LoginModel
    {
        [StringLength(50, MinimumLength = 1, ErrorMessage = "The user name must be between 1 and 50 characters long.")]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}