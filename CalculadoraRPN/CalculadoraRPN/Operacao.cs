using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CalculadoraRPN {
    class Operacao {
        public decimal numero;

        public Operacao(decimal numero) {
            this.numero = numero;
        }
        public Operacao(string numero, NumberFormatInfo format) {
            this.numero = Convert.ToDecimal(numero, format);
        }

        public string get_numero(NumberFormatInfo format, int tamanho) {
            string f = "N";
            string num = numero.ToString(f, format);
            int len = num.Length;
            if (len > tamanho) return num.Substring(0, tamanho);
            else if (len < tamanho) return num.PadLeft(tamanho - len, ' ');
            return num;
        }

        public void adicao(decimal outro_numero) {
            this.numero += outro_numero;
        }
        public void adicao(string outro_numero) {
            if (outro_numero != "")
                adicao(Convert.ToDecimal(outro_numero));
        }

        public void subtracao(decimal outro_numero) {
            this.numero -= outro_numero;
        }
        public void subtracao(string outro_numero) {
            if (outro_numero != "")
                subtracao(Convert.ToDecimal(outro_numero));
		}

		public void multiplicacao(decimal outro_numero) {
			this.numero *= outro_numero;
		}
		public void multiplicacao(string outro_numero) {
			if (outro_numero != "")
				multiplicacao(Convert.ToDecimal(outro_numero));
		}

		public void divisao(decimal outro_numero) {
			this.numero /= outro_numero;
		}
		public void divisao(string outro_numero) {
			if (outro_numero != "")
				divisao(Convert.ToDecimal(outro_numero));
		}

	}
}
