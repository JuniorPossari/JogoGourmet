using System.Text.RegularExpressions;

namespace Jogo.Domain.Util.Extensions
{
	public static class RespostaExtensions
	{
		public static string Formatar(this string resposta)
		{
			if (string.IsNullOrEmpty(resposta)) return string.Empty;

			return Regex.Replace(resposta, "[^a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ\\s]", "").Replace("  ", " ").Trim();
		}

		public static bool Valida(this string resposta, bool permiteRespostaVazia = false)
		{
			if (permiteRespostaVazia && string.IsNullOrEmpty(resposta)) return true;

			var valida = !string.IsNullOrEmpty(resposta) && resposta.Length >= 3 && resposta.Length <= 50;

			if (!valida) Console.WriteLine($" * Resposta inválida! [a resposta deve ter entre 3 e 30 caracteres]");

			return valida;
		}

		public static bool ValidaSN(this string resposta)
		{
			var valida = !string.IsNullOrEmpty(resposta) && (resposta.Equals("S", StringComparison.OrdinalIgnoreCase) || resposta.Equals("N", StringComparison.OrdinalIgnoreCase));

			if (!valida) Console.WriteLine($" * Resposta inválida! [responda apenas com a letra 'S' para SIM ou letra 'N' para NÃO]");

			return valida;
		}

		public static bool Sim(this string resposta)
		{
			return !string.IsNullOrEmpty(resposta) && resposta.Equals("S", StringComparison.OrdinalIgnoreCase);
		}

		public static bool Nao(this string resposta)
		{
			return !string.IsNullOrEmpty(resposta) && resposta.Equals("N", StringComparison.OrdinalIgnoreCase);
		}
	}
}
