using System;

namespace CalculadoraRPN {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("Hello World!");

			Calculadora calculadora = new Calculadora();
			calculadora.execute();

			Console.ReadKey();
		}
	}
}
