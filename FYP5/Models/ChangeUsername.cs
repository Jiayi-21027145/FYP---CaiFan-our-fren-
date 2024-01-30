using Microsoft.AspNetCore.Mvc;

namespace FYP5.Models
{
    public class ChangeUsername
    {
        public string CurrentUsername { get; set; } = null!;

        [Required(ErrorMessage = "New Username Required")]
        [DataType(DataType.Text)]
        [Remote("VerifyNewUsername", "Account",
                ErrorMessage = "Username Not Available")]
        public string NewUname { get; set; } = null!;

        [Required(ErrorMessage = "Confim Username Required")]
        [DataType(DataType.Text)]
        [Compare("NewUname", ErrorMessage = "Usernames not matched")]
        public string ConfirmUname { get; set; } = null!;
    }
}
