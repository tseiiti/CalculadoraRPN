using System;
using System.Collections.Generic;
using System.Globalization;

namespace CalculadoraRPN {
	class Tela {
		int tamanho;

		public Tela(int tamanho) {
			this.tamanho = tamanho;
		}

		public void exibir(List<Operacao> operacoes, NumberFormatInfo format) {
			Console.Clear();
			Console.WriteLine("Calculadora RPN:");
			var count = operacoes.Count;
			var posic = "{0, " + this.tamanho.ToString() + "}";
			Console.WriteLine(posic, "(lista: " + count.ToString() + ")");

			Console.WriteLine(new String('-', this.tamanho));
			for (int i = 0; i < 4; i++) {
				var num = "";
				if (count > 3 - i)
					num = operacoes[count - 4 + i].get_numero(format);
				Console.WriteLine(posic, num);
			}
			Console.WriteLine(new String('-', this.tamanho));
		}

		public void converte_tecla(NumberFormatInfo format, ConsoleKeyInfo key, ref string numero, ref string comando) {
			comando = "";
			if ((key.Modifiers & ConsoleModifiers.Alt) != 0) comando = "ALT + ";
			if ((key.Modifiers & ConsoleModifiers.Shift) != 0) comando = "SHIFT + ";
			if ((key.Modifiers & ConsoleModifiers.Control) != 0) comando = "CTL + ";
			comando += key.Key.ToString();

			var aux = "";
			var key_char = key.KeyChar.ToString();
			if (key.Key == ConsoleKey.Spacebar) {
				Console.Clear();
				Console.WriteLine("Separdor Decimal (1 = Vírgula, 2 = Ponto):");
				var resp = "";
				while ("12".IndexOf(resp) != -1) {
					var k = Console.ReadKey();
					resp = k.KeyChar.ToString();
					if (resp == "1") {
						format = new CultureInfo("pt-BR").NumberFormat;
						format.NumberDecimalSeparator = ",";
						format.NumberGroupSeparator = ".";
					}
					if (resp == "2") {
						format = new CultureInfo("en-US").NumberFormat;
						format.NumberDecimalSeparator = ".";
						format.NumberGroupSeparator = ",";
					}
				}
				//this.tela.exibir(this.operacoes, format, this.tamanho);

			} else if (key.Key == ConsoleKey.A) {
				Console.WriteLine(format.NumberDecimalSeparator);

			//} else if (key.Key == ConsoleKey.Enter) {
			//	add_operacao();
			//} else if (key.Key == ConsoleKey.Delete) {
			//	del_operacao();

			//} else if ("+-*/".IndexOf(key_char) != -1) {
			//	aritimetica(key_char);

			//	//} else if (key.Key == ConsoleKey.Add) {
			//	//	aritimetica("+");
			//} else if (key.Key == ConsoleKey.OemPlus) {
			//	aritimetica("+");
			//	//} else if (key.Key == ConsoleKey.Subtract) {
			//	//	aritimetica("-");
			//	//} else if (key.Key == ConsoleKey.OemMinus) {
			//	//	aritimetica("-");
			//	//} else if (key.Key == ConsoleKey.Multiply) {
			//	//	aritimetica("*");
			//	//} else if ((key.Modifiers & ConsoleModifiers.Shift) != 0 && key.Key == ConsoleKey.D8) {
			//	//	aritimetica("*");
			//	//} else if (key.Key == ConsoleKey.Divide) {
			//	//	aritimetica("/");
			//	//} else if (key.Key == ConsoleKey.Oem2) {
			//	//	aritimetica("/");

			} else if (key.Key == ConsoleKey.Backspace) {
				aux = "\b";

			} else if (char.IsDigit(key.KeyChar)) { // números
				aux = key_char;
			} else if (char.IsPunctuation(key.KeyChar)) { // ponto decimal
				if (numero.IndexOf(format.NumberDecimalSeparator) == -1)
					aux = format.NumberDecimalSeparator;

			} else {
				Console.WriteLine("\n\n" + key.Key.ToString());
			}

			//Console.Write(aux);

			if (aux == "\b") {
				Console.Write(" \b");
				if (numero.Length == 1) numero = "";
				if (numero.Length > 1)
					numero = numero.Substring(0, numero.Length - 1);
			} else numero += aux;
		}
	}
}
