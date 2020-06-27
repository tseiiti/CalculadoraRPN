using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace CalculadoraRPN {
	class Calc {
		List<Operacao> operacoes;
		NumberFormatInfo format;
		string numero;
		int tamanho;

		public Calc() {
			operacoes = new List<Operacao>();
			format = new CultureInfo("pt-BR").NumberFormat;
			format.NumberDecimalSeparator = ".";
			format.NumberGroupSeparator = ",";
			numero = "";
			tamanho = 41;
		}

		public void executa() {
			ConsoleKeyInfo key;

			// Trata CTL+C
			Console.TreatControlCAsInput = true;

			exibir();
			do {
				key = Console.ReadKey(true);
				valida_tecla(key);
			} while (key.Key != ConsoleKey.Escape);
		}

		void exibir() {
			Console.Clear();
			Console.WriteLine("Calculadora RPN:");
			Console.WriteLine(new String('-', tamanho));
			for (int i = 0; i < 4; i++) {
				if (operacoes.Count > 3 - i)
					Console.WriteLine(operacoes[operacoes.Count - (4 - i)].get_numero(format, tamanho));
				else
					Console.WriteLine("");
			}
			Console.WriteLine(new String('-', tamanho));
		}

		void add_operacao() {
			if (numero == "" && operacoes.Count > 0) {
				numero = operacoes[operacoes.Count - 1].get_numero(format, tamanho);
			}

			if (numero != "") {
				operacoes.Add(new Operacao(numero, format));
				numero = "";
			}
			exibir();
		}

		void aritimetica(string operacao) {
			int count = operacoes.Count;

			if (numero == "" && count > 1) {
				numero = operacoes[count - 1].get_numero(format, tamanho);
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
				Console.WriteLine("erro");
				add_operacao();
			}
		}

		void valida_tecla(ConsoleKeyInfo key) {
			string aux = "";
			if (key.Key == ConsoleKey.Spacebar) {

			} else if (key.Key == ConsoleKey.Enter) {
				add_operacao();
			} else if ("+-*/".IndexOf(key.KeyChar.ToString()) != -1) {
				aritimetica(key.KeyChar.ToString());

			//} else if (key.Key == ConsoleKey.Add) {
			//	aritimetica("+");
			//} else if (key.Key == ConsoleKey.OemPlus) {
			//	aritimetica("+");
			//} else if (key.Key == ConsoleKey.Subtract) {
			//	aritimetica("-");
			//} else if (key.Key == ConsoleKey.OemMinus) {
			//	aritimetica("-");
			//} else if (key.Key == ConsoleKey.Multiply) {
			//	aritimetica("*");
			//} else if ((key.Modifiers & ConsoleModifiers.Shift) != 0 && key.Key == ConsoleKey.D8) {
			//	aritimetica("*");
			//} else if (key.Key == ConsoleKey.Divide) {
			//	aritimetica("/");
			//} else if (key.Key == ConsoleKey.Oem2) {
			//	aritimetica("/");

			} else if (key.Key == ConsoleKey.Backspace) {
				aux = "\b";

			} else if (char.IsDigit(key.KeyChar)) { //números
				aux = key.KeyChar.ToString();
			} else if (char.IsPunctuation(key.KeyChar)) { //pontos
				if (key.KeyChar.ToString() == format.NumberDecimalSeparator)
				aux = key.KeyChar.ToString();
				//} else if (keyInfo.Key == ConsoleKey.OemComma) {
				//	aux = ",";
				//} else if (keyInfo.Key == ConsoleKey.OemPeriod) {
				//	aux = ",";
				//} else if (keyInfo.Key == ConsoleKey.Decimal) {
				//	aux = ",";

			} else {
				Console.WriteLine("\n\n" + key.Key.ToString());
			}

			Console.Write(aux);

			if (aux == "\b") {
				if (numero.Length == 1) numero = "";
				if (numero.Length > 1) 
					numero = numero.Substring(0, numero.Length - 1);
			} else numero += aux;
		}
	}
}
