using System;
using System.ComponentModel.DataAnnotations;

namespace ShortReference.Models
{
    public class ReferenceInfoViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
		public string LongReference { get; set; }
        
        public string ShortenedReference { get; set; }

        public DateTime CreatedData { get; set; }
        public int CountTransitions { get; set; }
    }
}
