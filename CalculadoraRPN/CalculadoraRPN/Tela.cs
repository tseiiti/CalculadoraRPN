using System;
using System.Collections.Generic;
using System.Globalization;

namespace CalculadoraRPN {
	class Tela {
		int tamanho;
		NumberFormatInfo format;

		public Tela(NumberFormatInfo format, int tamanho) {
			this.format = format;
			this.tamanho = tamanho;
		}

		public Tela(NumberFormatInfo format, int tamanho, List<Operacao> operacoes) {
			this.format = format;
			this.tamanho = tamanho;
			exibir(operacoes);
		}

		/// <summary>
		/// Exibe na tela as operações da lista
		/// </summary>
		/// <param name="operacoes"></param>
		public void exibir(List<Operacao> operacoes) {
			Console.Clear();
			Console.WriteLine("Calculadora RPN:");
			Console.WriteLine("Tecla H: Ajuda");
			var count = operacoes.Count;
			var posic = "{0, " + this.tamanho.ToString() + "}";
			Console.WriteLine(posic, "(lista: " + count.ToString() + ")");

			// exibe somente as 4 últimas posições da lista
			Console.WriteLine(new String('-', this.tamanho));
			for (int i = 0; i < 4; i++) {
				var num = "";
				if (count > 3 - i)
					num = operacoes[count - 4 + i].get_numero(this.format);
				Console.WriteLine(posic, num);
			}
			Console.WriteLine(new String('-', this.tamanho));
		}

		/// <summary>
		/// Retorna o número e o comando digitado
		/// </summary>
		/// <param name="keyInfo"></param>
		/// <param name="comando"></param>
		/// <param name="numero"></param>
		public void converte_tecla(ConsoleKeyInfo keyInfo, ref string comando, ref string numero) {
			comando = keyInfo.Key.ToString();
			//if ((keyInfo.Modifiers & ConsoleModifiers.Alt) != 0) comando = "ALT + ";
			//if ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0) comando = "SHIFT + ";
			//if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0) comando = "CTL + ";
			//comando += keyInfo.Key.ToString();

			var aux = "";
			if (keyInfo.Key == ConsoleKey.H) {
				comando = "Help";
			} else if (keyInfo.Key == ConsoleKey.Help) {
				comando = "Help";
			} else if (keyInfo.Key == ConsoleKey.M) {
				comando = "Menu";
			} else if (keyInfo.Key == ConsoleKey.Enter) {
				comando = "Enter";
			} else if (keyInfo.Key == ConsoleKey.Delete) {
				comando = "Delete";
			} else if (keyInfo.Key == ConsoleKey.Add) {
				comando = "+";
			} else if (keyInfo.Key == ConsoleKey.OemPlus) {
				comando = "+";
			} else if (keyInfo.Key == ConsoleKey.Subtract) {
				comando = "-";
			} else if (keyInfo.Key == ConsoleKey.OemMinus) {
				comando = "-";
			} else if (keyInfo.Key == ConsoleKey.Multiply) {
				comando = "*";
			} else if ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0 && keyInfo.Key == ConsoleKey.D8) {
				comando = "*";
			} else if (keyInfo.Key == ConsoleKey.Divide) {
				comando = "/";
			} else if (keyInfo.Key == ConsoleKey.Oem2) {
				comando = "/";
			} else if (keyInfo.Key == ConsoleKey.P) {
				comando = "p"; // potênciação
			} else if (keyInfo.Key == ConsoleKey.R) {
				comando = "r"; // raiz quadrada
			} else if (keyInfo.Key == ConsoleKey.S) {
				comando = "s"; // seno
			} else if (keyInfo.Key == ConsoleKey.C) {
				comando = "c"; // coseno
			} else if (keyInfo.Key == ConsoleKey.T) {
				comando = "t"; // tangente

			} else if (char.IsDigit(keyInfo.KeyChar)) { // números
				aux = keyInfo.KeyChar.ToString();
			} else if (char.IsPunctuation(keyInfo.KeyChar)) { // ponto decimal
				if (numero.IndexOf(this.format.NumberDecimalSeparator) == -1)
					aux = format.NumberDecimalSeparator;

			} else if (keyInfo.Key == ConsoleKey.Backspace) {
				aux = "\b";
			} else {
				Console.WriteLine("\n" + keyInfo.Key.ToString());
			}

			// exibe o número na tela
			Console.Write(aux);

			// completa o número digitado
			if (aux == "\b") {
				Console.Write(" \b");
				if (numero.Length == 1) numero = "";
				if (numero.Length > 1)
					numero = numero.Substring(0, numero.Length - 1);
			} else numero += aux;
		}

		/// <summary>
		/// Apresenta menu de ajuda
		/// </summary>
		public void help() {
			Console.Clear();
			Console.WriteLine("Tecla H.....: Help");
			Console.WriteLine("Tecla M.....: Menu");
			Console.WriteLine("Tecla Delete: Exclui último número");
			Console.WriteLine("Tecla +.....: Adição");
			Console.WriteLine("Tecla -.....: Subtração");
			Console.WriteLine("Tecla *.....: Multiplicação");
			Console.WriteLine("Tecla /.....: Divisão");
			Console.WriteLine("Tecla P.....: Potênciação");
			Console.WriteLine("Tecla S.....: Seno");
			Console.WriteLine("Tecla C.....: Coseno");
			Console.WriteLine("Tecla T.....: Tangente");
			Console.WriteLine("Tecla R.....: Raiz quadrada");
			Console.WriteLine("Tecla Esc...: Sair");
			Console.WriteLine("\nPressione qualquer tecla para voltar...");
			Console.ReadKey(true);
		}
	}
}
