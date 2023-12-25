namespace BlogTask2.Areas.Admin.Models.Blogs
{
    public class BlogCreateVM
    {

        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }

        public IFormFile CoverImage { get; set; }

    }
}
