namespace Application.Requests
{
    public class CreateWorkItemRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int ProjectId { get; set; }

        public int TypeId { get; set; }
    }

    public class UpdateWorkItemRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ProjectId { get; set; }

        public int? TypeId { get; set; }
    }
}
