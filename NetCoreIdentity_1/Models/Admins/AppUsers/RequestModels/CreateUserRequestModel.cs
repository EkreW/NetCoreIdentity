using System.ComponentModel.DataAnnotations;

namespace NetCoreIdentity_1.Models.Admins.AppUsers.RequestModels
{
    public class CreateUserRequestModel
    {
        [Required(ErrorMessage = "Kullanıcı adı alanı girilmesi zorunludur.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email formatında giriş yapınız.")]
        public string Email { get; set; }
    }
}
