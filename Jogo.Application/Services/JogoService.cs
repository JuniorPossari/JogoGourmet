using Jogo.Domain.Interfaces.Repositories;
using Jogo.Domain.Interfaces.Services;
using Jogo.Domain.Util.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo.Application.Services
{
	public class JogoService : IJogoService
	{
		private bool _acertouPrato;
		private bool _desistiu;
		private readonly int _maximoTentativas;
		private int _indiceTentativaAtual;
		private int _idCategoriaPai;
		private int _idCategoria;
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
			_maximoTentativas = 30;
			_indiceTentativaAtual = 0;
			_idCategoriaPai = 0;
			_idCategoria = 0;
			_categoriaRepository = categoriaRepository;
			_pratoRepository = pratoRepository;
		}

		public async Task Iniciar()
		{
			Console.Clear();
			Console.WriteLine("Pense em um prato e aperte qualquer tecla para iniciar!");
			Console.ReadKey();

			while (!_acertouPrato && !_desistiu && _indiceTentativaAtual <= _maximoTentativas)
			{
				_indiceTentativaAtual++;

				await this.VerificarCategorias();
			}

			if (_acertouPrato)
			{
				Console.Clear();
				Console.WriteLine("Eu acertei! Fim de jogo!!");
			}
			else
			{
				Console.Clear();
				Console.WriteLine("Eu desisto!");
			}
		}

		private string ObterRespostaSN()
		{
			var resposta = Console.ReadLine();

			while (!resposta.RespostaSNIsValid())
			{
				Console.WriteLine($"Resposta inválida! Responda apenas com a letra 'S' para SIM ou letra 'N' para NÃO.");
				resposta = Console.ReadLine();
			}

			return resposta;
		}

		private async Task VerificarCategorias()
		{
			var categorias = await _categoriaRepository.ObterCategorias(_idCategoriaPai);

			foreach (var categoria in categorias)
			{
				Console.Clear();
				Console.WriteLine($"O prato que você esta pensando é {categoria.Nome}? (Responda apenas com a letra 'S' para SIM ou letra 'N' para NÃO).");

				var resposta = this.ObterRespostaSN();

				if (resposta.RespostaIsSim())
				{
					_idCategoria = categoria.Id;
					break;
				}
			}

			await this.VerificarPratos();
		}

		private async Task VerificarPratos()
		{
			if (_idCategoria != 0)
			{
				var pratos = await _pratoRepository.ObterPorIdCategoria(_idCategoria);

				foreach (var prato in pratos)
				{
					Console.Clear();
					Console.WriteLine($"O prato que você esta pensando é {prato.Nome}? (Responda apenas com a letra 'S' para SIM ou letra 'N' para NÃO).");

					var resposta = this.ObterRespostaSN();

					if (resposta.RespostaIsSim())
					{
						_acertouPrato = true;
						break;
					}
				}

				if (!_acertouPrato && await _categoriaRepository.PossuiSubCategorias(_idCategoria))
				{
					_idCategoriaPai = _idCategoria;
					return;
				}
			}
			else
			{
				_desistiu = true;
			}
		}
	}
}
