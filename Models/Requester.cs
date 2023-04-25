using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItemLog.Models
{
    public class Requester
    {
        public int Id { get; set; }

        [Display(Name = "Staff Number")]
        //[Required(ErrorMessage = "Staff Number is required")]
        public int? StaffNum { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Other Name(s)")]
        public string? OtherName { get; set; }

        [Display(Name = "Phone Number")]
        public int PhoneNumber { get; set; }

        [Display(Name = "Department")]
        public string? Department { get; set; }
    }
}

