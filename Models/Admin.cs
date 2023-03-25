using System;
using System.ComponentModel.DataAnnotations;

namespace ItemLog.Models
{
	public class Admin
	{
        public int Id { get; set; }

        [Display(Name = "Staff Number")]
        [Required(ErrorMessage = "Staff Number is required")]
        public int StaffNum { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = string.Empty;

        public string? OtherName { get; set; }

        [Display(Name = "Phone Number")]
        public int PhoneNumber { get; set; }
    }
}

