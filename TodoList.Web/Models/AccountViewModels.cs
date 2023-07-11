using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ELTE.TodoList.Web.Models
{
    public class LoginViewModel
    {
        [DisplayName("Név")]
        public string UserName { get; set; } = null!;

        [DisplayName("Jelszó")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }

    public class RegisterViewModel
    {
        [DisplayName("Név")]
        public string UserName { get; set; } = null!;

        [DisplayName("Jelszó")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DisplayName("Jelszó megerősítése")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordRepeat { get; set; } = null!;
    }
}
