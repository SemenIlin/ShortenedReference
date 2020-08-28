using System;
using System.ComponentModel.DataAnnotations;

namespace ShortReference.Models
{
    public class ReferenceInfoViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$", ErrorMessage = "Invalid data")]
        public string LongReference { get; set; }
        
        public string ShortenedReference { get; set; }

        public DateTime CreatedData { get; set; }
        public int CountTransitions { get; set; }
    }
}
