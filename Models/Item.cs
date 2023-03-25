using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItemLog.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string ImageURL { get; set; } =  string.Empty;

        public bool Available { get; set; } = true;

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; } = string.Empty;

        [Display(Name = "Brand")]
        public string? Brand { get; set; }

        [Display(Name = "Serial Number")]
        public string? SerialNum { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
       

    }
}

