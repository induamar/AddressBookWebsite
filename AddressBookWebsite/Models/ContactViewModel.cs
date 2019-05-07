using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AddressBookWebsite.Models
{
    public partial class ContactViewModel
    {
        [Key]
        [DisplayName("Contact ID")]
        public int? ContactID { get; set; }
        public string Avatar { get; set; }
        [DisplayName("Age")]
        [Required(ErrorMessage = "Age Field Required")]
        [Range(18, 200)]
        public int Age { get; set; }
        [RegularExpression(@"^(?:[A-Z][^\s]*\s?)+$", ErrorMessage = "Use title case only in the Full Name field please")]
        [Required(ErrorMessage = "Full Name field Required")]
        [MaxLength(250)]
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        // This property will hold all available states for selection
        public string Gender { get; set; }
        [Required(ErrorMessage = "Address One field Required")]
        [DisplayName("Address One")]
        [MaxLength(250)]
        public string AddressOne { get; set; }
        [DisplayName("Address Two")]
        [MaxLength(250)]
        public string AddressTwo { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please enter a valid phone number")]
        [MaxLength(15, ErrorMessage = "Maximum length is 15 digits")]
        public string Phone { get; set; }
        [MaxLength(15, ErrorMessage = "Maximum length is 15 digits")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please enter a valid mobile or phone number")]
        public string Mobile { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email Address not valid")]
        public string Email { get; set; }

        public enum GenderValues
        {
            Male,
            Female
        }
    }
}