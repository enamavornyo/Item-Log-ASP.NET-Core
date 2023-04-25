using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItemLog.Models
{
    public class Request
    {
        public int Id { get; set; }


        [Display(Name = "Admin")]
        public int AdminId { get; set; }
        [ForeignKey("AdminId")]
        public Admin? Admin { get; set; }

        [Display(Name = "Requester")]
        public int RequesterId { get; set; }
        [ForeignKey("RequesterId")]
        public Requester? Requester { get; set; }


        [Display(Name = "Item")]
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item? Item { get; set; }

        [Display(Name = "Request Date")]
        [Required(ErrorMessage = "Request Date is required")]
        public DateTime RequestDate  { get; set; }

        [Display(Name = "Return Date")]
        public DateTime? ReturnDate { get; set; }

    }
}

