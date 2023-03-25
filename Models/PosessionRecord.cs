using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItemLog.Models
{
    public class PosessionRecord
    {
        public int Id { get; set; }

        public int AdminId { get; set; }
        [ForeignKey("AdminId")]
        public Admin? Admin { get; set; }

        public int PosessorId { get; set; }
        [ForeignKey("PosessorId")]
        public Posessor? posessor { get; set; }

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

