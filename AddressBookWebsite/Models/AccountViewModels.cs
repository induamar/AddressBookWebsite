using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading;

namespace AddressBookWebsite.Models
{
    public class ExternalLoginConfirmationViewModel : ValidationAttribute
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Key]
        [Display(Name = "User ID")]
        public int UserID { get; set; }

        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^(?:[A-Z][^\s]*\s?)+$", ErrorMessage = "Username format Invalid")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(250)]
        [RegularExpression(@"^(?:[A-Z][^\s]*\s?)+$", ErrorMessage = "Full Name format Invalid")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [EmailAddress]
        [Required]
        [MaxLength(250)]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class CreateUserRoleViewModel
    {
        [Key]
        [Display(Name = "User ID")]
        public int UserID{ get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "User Name")]
        [MaxLength(10)]
        [RegularExpression(@"^(?:[A-Z][^\s]*\s?)+$", ErrorMessage = "Use title case only in the User Name field please")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(250)]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?:[A-Z][^\s]*\s?)+$", ErrorMessage = "Use title case only in the Full Name field please")]
        [MaxLength(250)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}