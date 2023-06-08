namespace Application.DataTransfer
{
    public class WorkItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ProjectDto Project { get; set; }

        public WorkItemTypeDto Type { get; set; }
    }
}
