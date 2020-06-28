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

        public string get_numero(NumberFormatInfo format) {
            var num = numero.ToString("G", format);
            var len = num.Length;
            var pto = num.IndexOf(format.NumberDecimalSeparator);
            if (pto > -1) {
                var fmt = "N" + (len - pto - 1).ToString();
                num = numero.ToString(fmt, format);
                num = num.TrimEnd('0');
                num = num.TrimEnd(format.NumberDecimalSeparator.ToCharArray()[0]);
            } else {
                var fmt = "##" + format.NumberDecimalSeparator + "#";
                num = numero.ToString(fmt, format);
            }
            return num;
        }

        public void adicao(decimal outro_numero) {
            this.numero += outro_numero;
        }
        public void adicao(string outro_numero, NumberFormatInfo format) {
            if (outro_numero != "")
                adicao(Convert.ToDecimal(outro_numero, format));
        }

        public void subtracao(decimal outro_numero) {
            this.numero -= outro_numero;
        }
        public void subtracao(string outro_numero, NumberFormatInfo format) {
            if (outro_numero != "")
                subtracao(Convert.ToDecimal(outro_numero, format));
		}

		public void multiplicacao(decimal outro_numero) {
			this.numero *= outro_numero;
		}
		public void multiplicacao(string outro_numero, NumberFormatInfo format) {
			if (outro_numero != "")
				multiplicacao(Convert.ToDecimal(outro_numero, format));
		}

		public void divisao(decimal outro_numero) {
			this.numero /= outro_numero;
		}
		public void divisao(string outro_numero, NumberFormatInfo format) {
			if (outro_numero != "")
				divisao(Convert.ToDecimal(outro_numero, format));
		}

	}
}
