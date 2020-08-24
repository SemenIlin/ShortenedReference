using System.ComponentModel.DataAnnotations;

namespace ShortenedReferenceCommon.Model
{
    public class Counter
    {
        public int AmountClickLink { get; set; }
        [Key]
        public int ReferenceInfoId { get; set; }

        public ReferenceInfo ReferenceInfo { get; set; }
    }
}
