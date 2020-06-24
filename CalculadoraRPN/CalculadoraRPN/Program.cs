using System;

namespace CalculadoraRPN {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("Calculadora");

			Calc calc = new Calc();
			calc.executa();

			//Console.ReadKey();
		}
	}
}
