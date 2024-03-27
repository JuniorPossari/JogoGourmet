using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo.Domain.Util.Extensions
{
	public static class TextExtensions
	{
		public static bool RespostaSNIsValid(this string resposta)
		{
			return !string.IsNullOrEmpty(resposta) && (resposta.Equals("S", StringComparison.OrdinalIgnoreCase) || resposta.Equals("N", StringComparison.OrdinalIgnoreCase));
		}

		public static bool RespostaIsSim(this string resposta)
		{
			return !string.IsNullOrEmpty(resposta) && resposta.Equals("S", StringComparison.OrdinalIgnoreCase);
		}

		public static bool RespostaIsNao(this string resposta)
		{
			return !string.IsNullOrEmpty(resposta) && resposta.Equals("N", StringComparison.OrdinalIgnoreCase);
		}
	}
}
