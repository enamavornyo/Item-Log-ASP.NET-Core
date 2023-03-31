﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItemLog.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Slug { get; set; } = string.Empty;

        public string ImageURL { get; set; } =  string.Empty;

        public bool Available { get; set; } = true;

        [Required, Range(1, int.MaxValue, ErrorMessage = "You must choose a category")]
        public long CategoryId { get; set; }

        public Category Category { get; set; }

        [Display(Name = "Brand")]
        public string? Brand { get; set; }

        [Display(Name = "Serial Number")]
        public string? SerialNum { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
       

    }
}

