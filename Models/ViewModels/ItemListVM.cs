using System;
using System.ComponentModel.DataAnnotations;

namespace ItemLog.Models.ViewModels
{
	public class ItemListVM
	{
        public List <Item>? Items { get; set; }

        [Display(Name = "Total Number")]
        public int TotalNum { get; set; }
    }
}

