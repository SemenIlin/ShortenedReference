namespace ShortenedReferenceDAL.Models
{
    public class ReferenceInfo
    {
        public int Id { get; set; }
        public string LongReference { get; set; }        
        public string ShortenedReference { get; set; }
        public System.DateTime CreatedData { get; set; }
        public int CountTransitions { get; set; }
    }
}
