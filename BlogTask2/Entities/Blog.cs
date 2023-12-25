using System.ComponentModel.DataAnnotations.Schema;

namespace BlogTask2.Entities
{
	public class Blog : BaseEntity
	{
        public string Title { get; set; }
        public string Author { get; set; }

        [Column(TypeName ="date")]
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
    }
}
