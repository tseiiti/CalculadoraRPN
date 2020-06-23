using System;
using System.Collections.Generic;
using System.Text;

namespace CalculadoraRPN {
	class Calculadora {
		public void execute() {
			string s = Calc._in();
			Calc._out(s);

			Calc.teste();

			Calc calc = new Calc();
		}
	}
}
