using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace CalculadoraRPN {
	class Calc {

		public static void teste() {
			ConsoleKeyInfo cki;
			// Prevent example from ending if CTL+C is pressed.
			Console.TreatControlCAsInput = true;

			Console.WriteLine("Press any combination of CTL, ALT, and SHIFT, and a console key.");
			Console.WriteLine("Press the Escape (Esc) key to quit: \n");
			do {
				cki = Console.ReadKey(true);
				Console.Write(" --- You pressed ");
				if ((cki.Modifiers & ConsoleModifiers.Control) != 0) Console.Write("CTL+");
				if ((cki.Modifiers & ConsoleModifiers.Alt) != 0) Console.Write("ALT+");
				if ((cki.Modifiers & ConsoleModifiers.Shift) != 0) Console.Write("SHIFT+");

				Console.WriteLine(cki.Key.ToString());

			} while (cki.Key != ConsoleKey.Escape);
		}

		public static string _in() {
			return Console.ReadLine();
		}

		public static void _out(string s) {
			Console.WriteLine(s);
		}
	}
}
