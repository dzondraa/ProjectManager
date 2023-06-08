namespace Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public virtual User User { get; set; }

        public virtual Comment Parent { get; set; }

        public virtual WorkItem WorkItem { get; set; }
    }
}
