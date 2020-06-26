using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace CalculadoraRPN {
	class Calc {
		List<Operacao> operacoes = new List<Operacao>();

		public void executa() {
			ConsoleKeyInfo keyInfo;
			string numero = "";

			// Trata CTL+C
			Console.TreatControlCAsInput = true;

			exibir();

			do {
				keyInfo = Console.ReadKey(true);

				valida_tecla(keyInfo, ref numero);

			} while (keyInfo.Key != ConsoleKey.Escape);
		}

		void add_operacao(ref string numero) {
			if (numero != "") {
				operacoes.Add(new Operacao(numero));
				numero = "";
			}
			exibir();
		}

		void adicao(ref string numero) {
			int count = operacoes.Count;

			if (numero == "" && count > 1) {
				numero = operacoes[count - 1].getNumero();
				operacoes.RemoveAt(count - 1);
				count--;
			}

			if (count > 0) {
				operacoes[count - 1].adicao(numero);
				numero = "";
				exibir();
			} else {
				Utils._out("erro");
				add_operacao(ref numero);
			}
		}

		void subtracao(ref string numero) {
			int count = operacoes.Count;

			if (numero == "" && count > 1) {
				numero = operacoes[count - 1].getNumero();
				operacoes.RemoveAt(count - 1);
				count--;
			}

			if (count > 0) {
				operacoes[count - 1].subtracao(numero);
				numero = "";
				exibir();
			} else {
				Utils._out("erro");
				add_operacao(ref numero);
			}
		}

		void exibir() {
			Console.Clear();
			Console.WriteLine("Calculadora:");
			Utils._out("----------------------------------------");
			foreach (var operacao in operacoes) {
				Utils._out(operacao.getNumero());
			}
			Utils._out("----------------------------------------");
		}

		void valida_tecla(ConsoleKeyInfo keyInfo, ref string numero) {
			string aux = "";
			if (keyInfo.Key == ConsoleKey.D0) {
				aux = "0";
			} else if (keyInfo.Key == ConsoleKey.D1) {
				aux = "1";
			} else if (keyInfo.Key == ConsoleKey.D2) {
				aux = "2";
			} else if (keyInfo.Key == ConsoleKey.D3) {
				aux = "3";
			} else if (keyInfo.Key == ConsoleKey.D4) {
				aux = "4";
			} else if (keyInfo.Key == ConsoleKey.D5) {
				aux = "5";
			} else if (keyInfo.Key == ConsoleKey.D6) {
				aux = "6";
			} else if (keyInfo.Key == ConsoleKey.D7) {
				aux = "7";
			} else if (keyInfo.Key == ConsoleKey.D8) {
				aux = "8";
			} else if (keyInfo.Key == ConsoleKey.D9) {
				aux = "9";
			} else if (keyInfo.Key == ConsoleKey.NumPad0) {
				aux = "0";
			} else if (keyInfo.Key == ConsoleKey.NumPad1) {
				aux = "1";
			} else if (keyInfo.Key == ConsoleKey.NumPad2) {
				aux = "2";
			} else if (keyInfo.Key == ConsoleKey.NumPad3) {
				aux = "3";
			} else if (keyInfo.Key == ConsoleKey.NumPad4) {
				aux = "4";
			} else if (keyInfo.Key == ConsoleKey.NumPad5) {
				aux = "5";
			} else if (keyInfo.Key == ConsoleKey.NumPad6) {
				aux = "6";
			} else if (keyInfo.Key == ConsoleKey.NumPad7) {
				aux = "7";
			} else if (keyInfo.Key == ConsoleKey.NumPad8) {
				aux = "8";
			} else if (keyInfo.Key == ConsoleKey.NumPad9) {
				aux = "9";
			} else if (keyInfo.Key == ConsoleKey.OemComma) {
				aux = ",";
			} else if (keyInfo.Key == ConsoleKey.OemPeriod) {
				aux = ",";
			} else if (keyInfo.Key == ConsoleKey.Enter) {
				add_operacao(ref numero);
			} else if (keyInfo.Key == ConsoleKey.Add) {
				adicao(ref numero);
			} else if (keyInfo.Key == ConsoleKey.OemPlus) {
				adicao(ref numero);
			} else if (keyInfo.Key == ConsoleKey.Subtract) {
				subtracao(ref numero);
			} else if (keyInfo.Key == ConsoleKey.OemMinus) {
				subtracao(ref numero);
			} else if (keyInfo.Key == ConsoleKey.A) {
				exibir();
			} else {
				Utils._out("\n\n" + keyInfo.Key.ToString());
			}

			Utils._o(aux);
			numero += aux;
		}
	}
}
