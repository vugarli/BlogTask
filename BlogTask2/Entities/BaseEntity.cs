namespace BlogTask2.Entities
{
	public class BaseEntity
	{
        public int Id { get; set; }
        public bool IsDeleted { get; private set; }

        public void Delete()
        {
            IsDeleted = true;
        }
        public void ReokeDelete()
        {
            IsDeleted = false;
        }

    }
}
