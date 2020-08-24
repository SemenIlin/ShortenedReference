using System;
using System.ComponentModel.DataAnnotations;

namespace ShortenedReferenceCommon.Model
{
    public class ReferenceInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$", ErrorMessage = "Invalid data")]
        public string LongReference { get; set; }
        
        public string ShortenedReference { get; set; }

        public DateTime CreatedData { get; set; }
        public Counter Counter { get; set; }
    }
}
