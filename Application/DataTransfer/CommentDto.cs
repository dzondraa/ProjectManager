namespace Application.DataTransfer
{
    public class CommentDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public UserDto User { get; set; }

        public int ParentId { get; set; }

        public CommentDto Parent { get; set; }
    }
}
