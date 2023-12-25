using System.ComponentModel.DataAnnotations.Schema;

namespace BlogTask2.Models
{
    public class BlogItemVM
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string DateString { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
    }
}
