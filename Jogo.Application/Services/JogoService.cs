using Jogo.Domain.Entities;
using Jogo.Domain.Interfaces.Repositories;
using Jogo.Domain.Interfaces.Services;
using Jogo.Domain.Util.Extensions;

namespace Jogo.Application.Services
{
	public class JogoService : IJogoService
	{
		private bool _acertouPrato;
		private bool _desistiu;
		private int _idCategoriaPai;
		private int _idCategoriaSelecionada;
		private readonly ICategoriaRepository _categoriaRepository;
		private readonly IPratoRepository _pratoRepository;

		public JogoService
		(
			ICategoriaRepository categoriaRepository,
			IPratoRepository pratoRepository
		)
		{
			_acertouPrato = false;
			_desistiu = false;
			_idCategoriaPai = 0;
			_idCategoriaSelecionada = 0;
			_categoriaRepository = categoriaRepository;
			_pratoRepository = pratoRepository;
		}

		public async Task Iniciar()
		{
			Console.Clear();
			Console.WriteLine("Pense em um prato e aperte qualquer tecla para iniciar!");
			Console.ReadKey();

			while (!_acertouPrato && !_desistiu) await this.VerificarCategorias();

			if (_acertouPrato)
			{
				Console.Clear();
				Console.WriteLine("Acertei!! Fim de jogo.. Muito obrigado por jogar!! :D");
			}
			else
			{
				Console.Clear();
				Console.WriteLine("Eu desisto!! :( Em qual prato você estava pensando?");

				var nomePrato = this.ObterResposta();

				var nomeCategoria = "";

				if (_idCategoriaSelecionada > 0)
				{
					var categoriaSelecionada = await _categoriaRepository.ObterPorId(_idCategoriaSelecionada);

					Console.Clear();
					Console.WriteLine($"Pensando dentro da categoria ({categoriaSelecionada.Nome}), qual sub categoria você acha que o prato ({nomePrato}) se encaixa?");
					Console.WriteLine("Caso acredite que não possua uma sub categoria para esse prato, apenas aperte qualquer tecla para continuar.");

					nomeCategoria = this.ObterResposta(permiteRespostaVazia: true);
				}
				else
				{
					Console.Clear();
					Console.WriteLine($"Em qual categoria você acha que o prato ({nomePrato}) se encaixa?");

					nomeCategoria = this.ObterResposta();
				}

				await this.AdicionarNovaCategoriaPrato(nomeCategoria, nomePrato);
			}
		}

		private async Task VerificarCategorias()
		{
			var categorias = await _categoriaRepository.ObterCategorias(_idCategoriaPai);

			foreach (var categoria in categorias)
			{
				Console.Clear();
				Console.WriteLine($"O prato que você esta pensando é {categoria.Nome}? (Responda apenas com a letra 'S' para SIM ou letra 'N' para NÃO).");

				var resposta = this.ObterResposta(apenasSimOuNao: true);

				if (resposta.Sim())
				{
					_idCategoriaSelecionada = categoria.Id;
					break;
				}
			}

			await this.VerificarPratos();
		}

		private async Task VerificarPratos()
		{
			if (_idCategoriaSelecionada != 0)
			{
				var pratos = await _pratoRepository.ObterPorIdCategoria(_idCategoriaSelecionada);

				foreach (var prato in pratos)
				{
					Console.Clear();
					Console.WriteLine($"O prato que você esta pensando é {prato.Nome}? (Responda apenas com a letra 'S' para SIM ou letra 'N' para NÃO).");

					var resposta = this.ObterResposta(apenasSimOuNao: true);

					if (resposta.Sim())
					{
						_acertouPrato = true;
						return;
					}
				}

				if (await _categoriaRepository.PossuiSubCategorias(_idCategoriaSelecionada))
				{
					_idCategoriaPai = _idCategoriaSelecionada;
					return;
				}
			}

			_desistiu = true;
		}

		private string ObterResposta(bool apenasSimOuNao = false, bool permiteRespostaVazia = false)
		{
			var resposta = Console.ReadLine().Formatar();

			if (apenasSimOuNao)
			{
				while (!resposta.ValidaSN()) resposta = Console.ReadLine();
			}
			else
			{
				while (!resposta.Valida(permiteRespostaVazia)) resposta = Console.ReadLine();
			}

			return resposta;
		}

		private async Task AdicionarNovaCategoriaPrato(string nomeCategoria, string nomePrato)
		{
			var sucesso = false;

			var categoria = _idCategoriaSelecionada > 0 && string.IsNullOrEmpty(nomeCategoria) ? await _categoriaRepository.ObterPorId(_idCategoriaSelecionada) : new Categoria();

			if (!string.IsNullOrEmpty(nomeCategoria))
			{
				categoria.Nome = nomeCategoria;
				categoria.IdCategoriaPai = _idCategoriaSelecionada;

				sucesso = _categoriaRepository.Add(categoria);

				if (!sucesso)
				{
					Console.Clear();
					Console.WriteLine($"Erro ao salvar a nova categoria ({nomeCategoria}) do prato ({nomePrato})!");
					return;
				}
			}

			var prato = new Prato();

			prato.Nome = nomePrato;
			prato.IdCategoria = categoria.Id;

			sucesso = _pratoRepository.Add(prato);

			if (!sucesso)
			{
				Console.Clear();
				Console.WriteLine($"Erro ao salvar o novo prato ({nomePrato}) da categoria ({nomeCategoria})!");
				return;
			}

			Console.Clear();
			Console.WriteLine($"Adicionamos o prato ({nomePrato}) a categoria ({nomeCategoria}). Muito obrigado por jogar!! :D");
		}
	}
}
