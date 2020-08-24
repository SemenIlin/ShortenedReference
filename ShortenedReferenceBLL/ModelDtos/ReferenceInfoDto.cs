namespace ShortenedReferenceBLL.ModelDtos
{
    public class ReferenceInfoDto
    {
        public int Id { get; set; }
        public string LongReference { get; set; }        
        public string ShortenedReference { get; set; }
        public System.DateTime CreatedData { get; set; }
        public int CountTransitions { get; set; }
    }
}