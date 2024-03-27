namespace Jogo.Domain.Entities
{
    public abstract class BaseEntity
    {
        public DateTime DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
