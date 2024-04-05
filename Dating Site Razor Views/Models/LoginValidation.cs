using System.ComponentModel.DataAnnotations;


namespace Dating_Site_Razor_Views.Models
{
    public class LoginValidation
    {
        [Required(ErrorMessage = "Username Required")]
        public string Username {  get; set; }

        public string Password { get; set; }
    }
}
