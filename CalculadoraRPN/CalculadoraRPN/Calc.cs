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
		ConsoleKeyInfo key;

		public Calc() {
			this.operacoes = new List<Operacao>();
			this.format = new CultureInfo("pt-BR").NumberFormat;
			//format.NumberDecimalSeparator = ".";
			//format.NumberGroupSeparator = ",";
			this.numero = "";
			this.tamanho = 41;
		}

		public void executa() {
			// Trata CTL+C
			Console.TreatControlCAsInput = true;

			exibir();
			do {
				this.key = Console.ReadKey(true);
				valida_tecla();
			} while (this.key.Key != ConsoleKey.Escape);
		}

		void exibir() {
			Console.Clear();
			Console.WriteLine("Calculadora RPN:");
			Console.WriteLine(new String('-', this.tamanho));
			var count = this.operacoes.Count;
			var posic = "{0, " + this.tamanho.ToString() + "}";
			for (int i = 0; i < 4; i++) {
				var num = "";
				if (count > 3 - i)
					num = this.operacoes[count - 4 + i].get_numero(this.format);
				Console.WriteLine(posic, num);
			}
			Console.WriteLine(new String('-', this.tamanho));
		}

		void add_operacao() {
			var count = this.operacoes.Count;
			if (this.numero == "" && count > 0) {
				this.numero = this.operacoes[count - 1].get_numero(this.format);
			}

			if (this.numero != "") {
				this.operacoes.Add(new Operacao(this.numero, this.format));
				this.numero = "";
			}
			exibir();
		}

		void aritimetica(string operacao) {
			int count = this.operacoes.Count;

			if (this.numero == "" && count > 1) {
				this.numero = this.operacoes[count - 1].get_numero(this.format);
				this.operacoes.RemoveAt(count - 1);
				count--;
			}

			if (count > 0) {
				if (operacao == "+") {
					this.operacoes[count - 1].adicao(this.numero, this.format);
				} else if (operacao == "-") {
					this.operacoes[count - 1].subtracao(this.numero, this.format);
				} else if (operacao == "*") {
					this.operacoes[count - 1].multiplicacao(this.numero, this.format);
				} else if (operacao == "/") {
					this.operacoes[count - 1].divisao(this.numero, this.format);
				}
				this.numero = "";
				exibir();
			} else {
				Console.WriteLine("erro");
				add_operacao();
			}
		}

		void valida_tecla() {
			var aux = "";
			var key_char = this.key.KeyChar.ToString();
			if (this.key.Key == ConsoleKey.Spacebar) {

			} else if (this.key.Key == ConsoleKey.Enter) {
				add_operacao();
			} else if ("+-*/".IndexOf(key_char) != -1) {
				aritimetica(key_char);

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

			} else if (this.key.Key == ConsoleKey.Backspace) {
				aux = "\b";

			} else if (char.IsDigit(this.key.KeyChar)) { //números
				aux = key_char;
			} else if (char.IsPunctuation(this.key.KeyChar)) { //pontos
				if (key_char == this.format.NumberDecimalSeparator 
						&& this.numero.IndexOf(key_char) == -1)
					aux = key_char;
				//} else if (keyInfo.Key == ConsoleKey.OemComma) {
				//	aux = ",";
				//} else if (keyInfo.Key == ConsoleKey.OemPeriod) {
				//	aux = ",";
				//} else if (keyInfo.Key == ConsoleKey.Decimal) {
				//	aux = ",";

			} else {
				Console.WriteLine("\n\n" + this.key.Key.ToString());
			}

			Console.Write(aux);

			if (aux == "\b") {
				Console.Write(" \b");
				if (this.numero.Length == 1) this.numero = "";
				if (this.numero.Length > 1) 
					this.numero = this.numero.Substring(0, this.numero.Length - 1);
			} else this.numero += aux;
		}
	}
}
