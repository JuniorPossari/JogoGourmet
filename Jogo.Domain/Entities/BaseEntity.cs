namespace Jogo.Domain.Entities
{
    public abstract class BaseEntity
    {
        public DateTimeOffset DateAdded { get; set; }
        public DateTimeOffset DateUpdated { get; set; }
    }
}
