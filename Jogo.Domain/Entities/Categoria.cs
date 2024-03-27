using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jogo.Domain.Entities
{
    public partial class Categoria : BaseEntity
    {
        public Categoria() { }

		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(256)]
		public string Nome { get; set; }

		[InverseProperty("Categoria")]
		public virtual ICollection<Prato> Pratos { get; set; } = new List<Prato>();
	}
}
