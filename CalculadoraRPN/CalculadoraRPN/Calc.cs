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
			string num = "";

			// Trata CTL+C
			Console.TreatControlCAsInput = true;

			do {
				keyInfo = Console.ReadKey(true);

				validaTecla(keyInfo, ref num);

				Utils._out(num);
			} while (keyInfo.Key != ConsoleKey.Escape);
		}

		void addOperacao(ref string numero, string operacao) {
			numero = "";
			operacoes.Add(new Operacao(numero, operacao));

			foreach (var ope in operacoes) {
				Utils._out(ope.numero.ToString());
			}
		}

		void adicao(ref string numero) {
			if (operacoes.Count > 0) {
				numero = operacoes[operacoes.Count - 1].adicao(numero);
				addOperacao(ref numero, "+");
			} else {
				Utils._out("erro");
				addOperacao(ref numero, "");
			}
		}

		void validaTecla(ConsoleKeyInfo keyInfo, ref string numero) {
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
				addOperacao(ref numero, "");
			} else if (keyInfo.Key == ConsoleKey.Add) {
				adicao(ref numero);
			} else if (keyInfo.Key == ConsoleKey.OemPlus) {
				adicao(ref numero);
			} else if (keyInfo.Key == ConsoleKey.A) {
				foreach (var op in operacoes) {
					Utils._out(op.numero.ToString());
				}
			} else {
				Utils._out(keyInfo.Key.ToString());
			}

		}
	}
}
