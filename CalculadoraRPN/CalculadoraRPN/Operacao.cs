using System;
using System.Globalization;

namespace CalculadoraRPN {
    class Operacao {
        public decimal numero;

        public Operacao(decimal numero) {
            this.numero = numero;
        }
        public Operacao(string numero, NumberFormatInfo format) {
            this.numero = Convert.ToDecimal(numero, format);
        }

        /// <summary>
        /// formata o numero decimal no padrão G em 29 dígitos com 41 
        /// posições. Acrescenta separador decimal e separador de 
        /// grupo quando ponto e espaços a esquerda.
        /// ex:                             1.234,56
        /// </summary>
        /// <param name="format"></param>
        /// <returns>número formatado</returns>
        public string get_numero(NumberFormatInfo format) {
            var num = numero.ToString("G", format);
            var pto = num.IndexOf(format.NumberDecimalSeparator);
            if (pto > -1) {
                var fmt = "N" + (num.Length - pto - 1).ToString();
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
