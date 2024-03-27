using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jogo.Domain.Entities
{
    public partial class Prato : BaseEntity
    {
        public Prato() { }

		[Key]
		public int Id { get; set; }

		public int IdCategoria { get; set; }

		[Required]
		[StringLength(256)]
		public string Nome { get; set; }

		[ForeignKey("IdCategoria")]
		[InverseProperty("Pratos")]
		public virtual Categoria Categoria { get; set; }
	}
}
