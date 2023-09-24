namespace API.Models.Participants
{
    public class Participant : IEntity

    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}