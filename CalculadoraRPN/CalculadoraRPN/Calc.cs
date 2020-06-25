using System;
using System.Collections.Generic;
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

			do {
				keyInfo = Console.ReadKey(true);

				valida_tecla(keyInfo, ref numero);

				Utils._out(numero);
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

		void exibir() {
			Utils._out("--------------------");
			foreach (var operacao in operacoes) {
				Utils._out(operacao.getNumero());
			}
			Utils._out("--------------------");
		}

		void valida_tecla(ConsoleKeyInfo keyInfo, ref string numero) {
			if (keyInfo.Key == ConsoleKey.D0) {
				numero += "0";
			} else if (keyInfo.Key == ConsoleKey.D1) {
				numero += "1";
			} else if (keyInfo.Key == ConsoleKey.D2) {
				numero += "2";
			} else if (keyInfo.Key == ConsoleKey.D3) {
				numero += "3";
			} else if (keyInfo.Key == ConsoleKey.D4) {
				numero += "4";
			} else if (keyInfo.Key == ConsoleKey.D5) {
				numero += "5";
			} else if (keyInfo.Key == ConsoleKey.D6) {
				numero += "6";
			} else if (keyInfo.Key == ConsoleKey.D7) {
				numero += "7";
			} else if (keyInfo.Key == ConsoleKey.D8) {
				numero += "8";
			} else if (keyInfo.Key == ConsoleKey.D9) {
				numero += "9";
			} else if (keyInfo.Key == ConsoleKey.NumPad0) {
				numero += "0";
			} else if (keyInfo.Key == ConsoleKey.NumPad1) {
				numero += "1";
			} else if (keyInfo.Key == ConsoleKey.NumPad2) {
				numero += "2";
			} else if (keyInfo.Key == ConsoleKey.NumPad3) {
				numero += "3";
			} else if (keyInfo.Key == ConsoleKey.NumPad4) {
				numero += "4";
			} else if (keyInfo.Key == ConsoleKey.NumPad5) {
				numero += "5";
			} else if (keyInfo.Key == ConsoleKey.NumPad6) {
				numero += "6";
			} else if (keyInfo.Key == ConsoleKey.NumPad7) {
				numero += "7";
			} else if (keyInfo.Key == ConsoleKey.NumPad8) {
				numero += "8";
			} else if (keyInfo.Key == ConsoleKey.NumPad9) {
				numero += "9";
			} else if (keyInfo.Key == ConsoleKey.Enter) {
				add_operacao(ref numero);
			} else if (keyInfo.Key == ConsoleKey.Add) {
				adicao(ref numero);
			} else if (keyInfo.Key == ConsoleKey.OemPlus) {
				adicao(ref numero);
			} else if (keyInfo.Key == ConsoleKey.A) {
				exibir();
			} else {
				Utils._out(keyInfo.Key.ToString());
			}

		}
	}
}
