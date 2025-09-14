using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookOrganizer.Models
{
    public class Book
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 4)]
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? description { get; set; } = string.Empty;

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; }
        public string Genre { get; set; } = string.Empty;

        [Display(Name = "Actual Page")]
        public int actualPage { get; set; }

        [Display(Name = "Total Pages")]
        public int TotalPages { get; set; }


        public int GetPercentageCompleted()
        {
            if (actualPage == 0) return 0;
            return (int)Math.Round((double)(actualPage * 100) / TotalPages);
        }
    }
}
