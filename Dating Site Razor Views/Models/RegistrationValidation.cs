using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Dating_Site_Razor_Views.Models
{
    public class RegistrationValidation
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string SecurityQuestion1 { get; set; }

        [Required(ErrorMessage = "Security question must be answered.")]
        public string SecurityAnswer1 { get; set; }
        public string SecurityQuestion2 { get; set; }

        [Required(ErrorMessage = "Security question must be answered.")]
        public string SecurityAnswer2 { get; set; }
        public string SecurityQuestion3 { get; set; }

        [Required(ErrorMessage = "Security question must be answered.")]
        public string SecurityAnswer3 { get; set; }
    }
    public class SecurePassword
    {
        string username { get; set; }
        public string EncryptedPassword {  get; set; }
        public string DecryptedPassword {  get; set; }

        public static string EncryptPass(string password) //encrypts password to base64
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public static string DecryptPass(string encodedData)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
    }
}
