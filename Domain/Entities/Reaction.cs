namespace Domain.Entities
{
    public class Reaction
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ReactionType ReactionType { get; set; }

    }
}
