namespace Application.Requests
{
    public class CreateCommentRequest
    {
        public string Content { get; set; }

        public int UserId { get; set; }

        public int ParentId { get; set; }

        public int WorkItemId { get; set; }
    }

    public class UpdateCommentRequest
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int? UserId { get; set; }

        public int? ParentId { get; set; }
        
        public int WorkItemId { get; set; }
    }
}
