using System.ComponentModel.DataAnnotations;

namespace TopKnow.Management.Client.ViewModels;

public class CreateNewAdminViewModel
{
    [Required(ErrorMessage = "Bu alan gereklidir")]
    [MaxLength(32)]
    public string DisplayName { get; set; }
    [Required]
    [MaxLength(32)]
    [EmailAddress]
    public string EMail { get; set; }
    [Required]
    [MaxLength(16)]
    public string Password { get; set; }

    [Compare(nameof(Password), ErrorMessage = "Parolalar Eşleşmiyor")]
    public string PasswordAgain { get; set; }
}
