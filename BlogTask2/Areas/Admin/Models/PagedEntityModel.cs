namespace BlogTask2.Areas.Admin.Models
{
	public class PagedEntityModel<T>
	{
		public PagedEntityModel(T data, int currentPage, int pageSize, int itemCount, string next, string prev)
		{
			Data = data;
			CurrentPage = currentPage;
			PageSize = pageSize;
			ItemCount = itemCount;
			Next = next;
			Prev = prev;
		}

		public T Data { get; set; }
        public int CurrentPage { get; init; }
        public int PageSize { get; init; }
        public int ItemCount { get; init; }

        public string Next  { get; init; }
        public string Prev { get; init; }

        public bool HasNext { get => CurrentPage < PageCount; }
        public bool HasPrev { get => CurrentPage > 1;}

        public int PageCount { get => (int)Math.Ceiling((decimal)ItemCount / PageSize); }

    }
}
