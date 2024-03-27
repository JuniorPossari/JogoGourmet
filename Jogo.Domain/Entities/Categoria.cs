using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jogo.Domain.Entities
{
    public partial class Categoria : BaseEntity
    {
        public Categoria() { }

		[Key]
		public int Id { get; set; }

		public int? IdCategoriaPai { get; set; }

		[Required]
		[StringLength(256)]
		public string Nome { get; set; }

		[ForeignKey("IdCategoriaPai")]
		[InverseProperty("SubCategorias")]
		public virtual Categoria CategoriaPai { get; set; }

		[InverseProperty("CategoriaPai")]
		public virtual ICollection<Categoria> SubCategorias { get; set; } = new List<Categoria>();

		[InverseProperty("Categoria")]
		public virtual ICollection<Prato> Pratos { get; set; } = new List<Prato>();
	}
}
