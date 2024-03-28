using Jogo.Domain.Entities;
using Jogo.Domain.Interfaces.Repositories;
using Jogo.Domain.Interfaces.Services;
using Jogo.Domain.Util.Extensions;
using System.IO;

namespace Jogo.Application.Services
{
	public class JogoService : IJogoService
	{
		private bool _continuarJogando;
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
			_continuarJogando = true;
			_categoriaRepository = categoriaRepository;
			_pratoRepository = pratoRepository;
		}

		private void LimparDados()
		{
			_acertouPrato = false;
			_desistiu = false;
			_idCategoriaPai = 0;
			_idCategoriaSelecionada = 0;
		}

		private void LimparTela()
		{
			Console.Clear();
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine(
			"""
			     ██╗ ██████╗  ██████╗  ██████╗      ██████╗  ██████╗ ██╗   ██╗██████╗ ███╗   ███╗███████╗████████╗
			     ██║██╔═══██╗██╔════╝ ██╔═══██╗    ██╔════╝ ██╔═══██╗██║   ██║██╔══██╗████╗ ████║██╔════╝╚══██╔══╝
			     ██║██║   ██║██║  ███╗██║   ██║    ██║  ███╗██║   ██║██║   ██║██████╔╝██╔████╔██║█████╗     ██║   
			██   ██║██║   ██║██║   ██║██║   ██║    ██║   ██║██║   ██║██║   ██║██╔══██╗██║╚██╔╝██║██╔══╝     ██║   
			╚█████╔╝╚██████╔╝╚██████╔╝╚██████╔╝    ╚██████╔╝╚██████╔╝╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗   ██║   
			 ╚════╝  ╚═════╝  ╚═════╝  ╚═════╝      ╚═════╝  ╚═════╝  ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝   ╚═╝   
			                                                                                                      
			""");
			Console.WriteLine();
			Console.WriteLine();
		}

		public async Task Iniciar()
		{
			while (_continuarJogando)
			{
				this.LimparDados();

				this.LimparTela();
				Console.WriteLine(" * Pense em um prato! [aperte qualquer tecla para iniciar]");
				Console.Write("--> ");
				Console.ReadKey();

				while (!_acertouPrato && !_desistiu) await this.VerificarCategorias();

				if (_acertouPrato)
				{
					this.LimparTela();
					Console.WriteLine(" * Acertei!! Fim de jogo.. Muito obrigado por jogar!! :D");
				}
				else
				{
					this.LimparTela();
					Console.WriteLine(" * Eu desisto!! :( Em qual prato você estava pensando?");

					var nomePrato = this.ObterResposta();

					var nomeCategoria = "";

					if (_idCategoriaSelecionada > 0)
					{
						var categoriaSelecionada = await _categoriaRepository.ObterPorId(_idCategoriaSelecionada);

						this.LimparTela();
						Console.WriteLine($" * Pensando dentro da categoria [{categoriaSelecionada.Nome}], qual sub categoria você acha que o prato ({nomePrato}) se encaixa?");
						Console.WriteLine(" * [caso não possua uma sub categoria para esse prato, aperte qualquer tecla para continuar]");

						nomeCategoria = this.ObterResposta(permiteRespostaVazia: true);
					}
					else
					{
						this.LimparTela();
						Console.WriteLine($" * Em qual categoria você acha que o prato [{nomePrato}] se encaixa?");

						nomeCategoria = this.ObterResposta();
					}

					await this.AdicionarNovaCategoriaPrato(nomeCategoria, nomePrato);
				}

				Console.WriteLine();
				Console.WriteLine($" * Deseja continuar jogando?");
				Console.WriteLine($" * [responda apenas com a letra 'S' para SIM ou letra 'N' para NÃO]");

				var resposta = this.ObterResposta(apenasSimOuNao: true);

				_continuarJogando = resposta.Sim();
			}
		}

		private async Task VerificarCategorias()
		{
			var categorias = await _categoriaRepository.ObterCategorias(_idCategoriaPai);

			foreach (var categoria in categorias.OrderBy(c => Guid.NewGuid()).ToArray())
			{
				this.LimparTela();
				Console.WriteLine($" * O prato que você esta pensando é um/uma {categoria.Nome}?");
				Console.WriteLine($" * [responda apenas com a letra 'S' para SIM ou letra 'N' para NÃO]");

				var resposta = this.ObterResposta(apenasSimOuNao: true);

				if (resposta.Sim())
				{
					_idCategoriaSelecionada = categoria.Id;
					break;
				}
			}

			if (_idCategoriaPai > 0 && _idCategoriaSelecionada > 0 && _idCategoriaPai == _idCategoriaSelecionada)
			{
				_desistiu = true;
				return;
			}

			await this.VerificarPratos();
		}

		private async Task VerificarPratos()
		{
			if (_idCategoriaSelecionada > 0)
			{
				var pratos = await _pratoRepository.ObterPorIdCategoria(_idCategoriaSelecionada);

				foreach (var prato in pratos.OrderBy(c => Guid.NewGuid()).ToArray())
				{
					this.LimparTela();
					Console.WriteLine($" * O prato que você esta pensando é {prato.Nome}?");
					Console.WriteLine($" * [responda apenas com a letra 'S' para SIM ou letra 'N' para NÃO]");

					var resposta = this.ObterResposta(apenasSimOuNao: true);

					if (resposta.Sim())
					{
						_acertouPrato = true;
						return;
					}
				}

				if (_idCategoriaPai != _idCategoriaSelecionada && await _categoriaRepository.PossuiSubCategorias(_idCategoriaSelecionada))
				{
					_idCategoriaPai = _idCategoriaSelecionada;
					return;
				}
			}

			_desistiu = true;
		}

		private string ObterResposta(bool apenasSimOuNao = false, bool permiteRespostaVazia = false)
		{
			Console.Write("--> ");
			var resposta = Console.ReadLine().Formatar();

			if (apenasSimOuNao)
			{
				while (!resposta.ValidaSN())
				{
					Console.Write("--> ");
					resposta = Console.ReadLine();
				}
			}
			else
			{
				while (!resposta.Valida(permiteRespostaVazia))
				{
					Console.Write("--> ");
					resposta = Console.ReadLine();
				}
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
				if (_idCategoriaSelecionada > 0) categoria.IdCategoriaPai = _idCategoriaSelecionada;

				sucesso = _categoriaRepository.Add(categoria);

				if (!sucesso)
				{
					this.LimparTela();
					Console.WriteLine($" * Erro ao salvar a nova categoria [{nomeCategoria}] do prato [{nomePrato}]!");
					return;
				}
			}

			var prato = new Prato();

			prato.Nome = nomePrato;
			prato.IdCategoria = categoria.Id;

			sucesso = _pratoRepository.Add(prato);

			if (!sucesso)
			{
				this.LimparTela();
				Console.WriteLine($" * Erro ao salvar o novo prato [{nomePrato}] da categoria [{nomeCategoria}]!");
				return;
			}

			this.LimparTela();
			Console.WriteLine($" * Adicionamos o prato [{nomePrato}] a categoria [{categoria.Nome}]. Muito obrigado por jogar!! :D");
		}
	}
}
