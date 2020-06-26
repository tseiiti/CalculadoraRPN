using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace CalculadoraRPN {
	class Calc {
		List<Operacao> operacoes = new List<Operacao>();

		void exibir() {
			Console.Clear();
			Console.WriteLine("Calculadora RPN:");
			Utils._out("-----------------------------");
			for (int i = 0; i < 4; i++) {
				if (operacoes.Count > 3 - i)
					Utils._out(operacoes[operacoes.Count - (4 - i)].getNumero());
				else
					Utils._out("");
			}
			Utils._out("-----------------------------");
		}

		void add_operacao(ref string numero) {
			if (numero == "" && operacoes.Count > 1) {
				numero = operacoes[operacoes.Count - 1].getNumero();
			}

			if (numero != "") {
				operacoes.Add(new Operacao(numero));
				numero = "";
			}
			exibir();
		}

		void aritimetica(ref string numero, string operacao) {
			int count = operacoes.Count;

			if (numero == "" && count > 1) {
				numero = operacoes[count - 1].getNumero();
				operacoes.RemoveAt(count - 1);
				count--;
			}

			if (count > 0) {
				if (operacao == "+") {
					operacoes[count - 1].adicao(numero);
				} else if (operacao == "-") {
					operacoes[count - 1].subtracao(numero);
				} else if (operacao == "*") {
					operacoes[count - 1].multiplicacao(numero);
				} else if (operacao == "/") {
					operacoes[count - 1].divisao(numero);
				}
				numero = "";
				exibir();
			} else {
				Utils._out("erro");
				add_operacao(ref numero);
			}
		}

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

		void valida_tecla(ConsoleKeyInfo keyInfo, ref string numero) {
			string aux = "";
			if (keyInfo.Key == ConsoleKey.Spacebar) {

			} else if (keyInfo.Key == ConsoleKey.Enter) {
				add_operacao(ref numero);

			} else if (keyInfo.Key == ConsoleKey.Add) {
				aritimetica(ref numero, "+");
			} else if (keyInfo.Key == ConsoleKey.OemPlus) {
				aritimetica(ref numero, "+");
			} else if (keyInfo.Key == ConsoleKey.Subtract) {
				aritimetica(ref numero, "-");
			} else if (keyInfo.Key == ConsoleKey.OemMinus) {
				aritimetica(ref numero, "-");
			} else if (keyInfo.Key == ConsoleKey.Multiply) {
				aritimetica(ref numero, "*");
			} else if ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0 && keyInfo.Key == ConsoleKey.D8) {
				aritimetica(ref numero, "*");
			} else if (keyInfo.Key == ConsoleKey.Divide) {
				aritimetica(ref numero, "/");
			} else if (keyInfo.Key == ConsoleKey.Oem2) {
				aritimetica(ref numero, "/");

			} else if (keyInfo.Key == ConsoleKey.D0) {
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
			} else if (keyInfo.Key == ConsoleKey.Decimal) {
				aux = ",";

			} else {
				Utils._out("\n\n" + keyInfo.Key.ToString());
			}

			Utils._o(aux);
			numero += aux;
		}
	}
}
