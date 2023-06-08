namespace Application.Requests
{
    public class CreateRoleRequest
    {
        public string Name { get; set; }
    }

    public class UpdateRoleRequest
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
}
