using System;
using System.Collections.Generic;
using System.Globalization;

namespace CalculadoraRPN {
	class Tela {
		int tamanho;
		NumberFormatInfo format;

		public Tela(NumberFormatInfo format, List<Numero> numeros) {
			this.tamanho = 41;
			this.format = format;
			exibir(numeros, "");
		}

		/// <summary>
		/// Exibe na tela os números da lista
		/// </summary>
		/// <param name="numeros"></param>
		public void exibir(List<Numero> numeros, string numero) {
			Console.Clear();
			Console.WriteLine("Calculadora RPN (a = ajuda):");
			var count = numeros.Count;
			var posic = "{0, " + tamanho.ToString() + "}";
			Console.WriteLine(posic, "(lista: " + count.ToString() + ")");

			// exibe somente as 4 últimas posições da lista
			Console.WriteLine(new String('-', tamanho));
			for (int i = 0; i < 4; i++) {
				var num = "";
				if (count > 3 - i)
					num = numeros[count - 4 + i].get_numero(format);
				Console.WriteLine(posic, num);
			}
			Console.WriteLine(new String('-', tamanho));
			Console.Write(numero);
		}

		/// <summary>
		/// Retorna o número e o comando digitado
		/// </summary>
		/// <param name="keyInfo"></param>
		/// <param name="comando"></param>
		/// <param name="numero"></param>
		public Comando converte_tecla(ref string numero) {
			ConsoleKeyInfo keyInfo = Console.ReadKey(true);
			var aux = "";
			Comando comando = Comando.None;

			if (keyInfo.Key == ConsoleKey.A) {
				comando = Comando.Ajuda;
			} else if (keyInfo.Key == ConsoleKey.Help) {
				comando = Comando.Ajuda;
			} else if (keyInfo.Key == ConsoleKey.M) {
				comando = Comando.Menu;
			} else if (keyInfo.Key == ConsoleKey.Escape) {
				comando = Comando.Sair;
			} else if (keyInfo.Key == ConsoleKey.Enter) {
				comando = Comando.Enter;
			} else if (keyInfo.Key == ConsoleKey.Delete) {
				comando = Comando.Delete;
			} else if (keyInfo.Key == ConsoleKey.Add) {
				comando = Comando.Adição;
			} else if (keyInfo.Key == ConsoleKey.OemPlus) {
				comando = Comando.Adição;
			} else if (keyInfo.Key == ConsoleKey.Subtract) {
				comando = Comando.Subtração;
			} else if (keyInfo.Key == ConsoleKey.OemMinus) {
				comando = Comando.Subtração;
			} else if (keyInfo.Key == ConsoleKey.Multiply) {
				comando = Comando.Multiplicação;
			} else if ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0 && keyInfo.Key == ConsoleKey.D8) {
				comando = Comando.Multiplicação;
			} else if (keyInfo.Key == ConsoleKey.Divide) {
				comando = Comando.Divisão;
			} else if (keyInfo.Key == ConsoleKey.Oem2) {
				comando = Comando.Divisão;
			} else if (keyInfo.Key == ConsoleKey.P) {
				comando = Comando.Potenciação;
			} else if (keyInfo.Key == ConsoleKey.R) {
				comando = Comando.Radiciação;
			} else if (keyInfo.Key == ConsoleKey.S) {
				comando = Comando.Seno;
			} else if (keyInfo.Key == ConsoleKey.C) {
				comando = Comando.Coseno;
			} else if (keyInfo.Key == ConsoleKey.T) {
				comando = Comando.Tangente;
			} else if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0 && keyInfo.Key == ConsoleKey.Z) {
				comando = Comando.Desfazer;
			} else if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0 && keyInfo.Key == ConsoleKey.Y) {
				comando = Comando.Refazer;

			// Números
			} else if (char.IsDigit(keyInfo.KeyChar)) {
				aux = keyInfo.KeyChar.ToString();

			// Ponto decimal
			} else if (char.IsPunctuation(keyInfo.KeyChar)) {
				if (numero.IndexOf(format.NumberDecimalSeparator) == -1)
					aux = format.NumberDecimalSeparator;

			} else if (keyInfo.Key == ConsoleKey.Backspace) {
				aux = "\b";
			} else {
				Console.WriteLine("\n" + aux);
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

			return comando;
		}

		/// <summary>
		/// Apresenta menu de ajuda
		/// </summary>
		public void ajuda() {
			Console.Clear();
			Console.WriteLine("Tecla A.....: Ajuda");
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

		/// <summary>
		/// Menu para selecionar separador decimal entre ponto ou vírgula
		/// </summary>
		public NumberFormatInfo menu() {
			ConsoleKeyInfo keyInfo;
			string resp = "";
			
			Console.Clear();
			Console.WriteLine("Separador Decimal (. = Ponto, , = Vírgula):");

			while (resp != "." && resp != ",") {
				keyInfo = Console.ReadKey(true);
				resp = keyInfo.KeyChar.ToString();
				if (resp == ".") {
					//format = new CultureInfo("en-US").NumberFormat;
					format.NumberDecimalSeparator = ".";
					format.NumberGroupSeparator = ",";
				}
				if (resp == ",") {
					//format = new CultureInfo("pt-BR").NumberFormat;
					format.NumberDecimalSeparator = ",";
					format.NumberGroupSeparator = ".";
				}
			}

			return format;
		}
	}
}
