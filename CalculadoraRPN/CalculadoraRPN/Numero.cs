﻿using System;
using System.Globalization;

namespace CalculadoraRPN {
    class Numero {
        public decimal numero;

        public Numero(string numero, NumberFormatInfo format) {
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
            if (numero == 0) {
                num = "0";
            } else if (pto > -1) {
                var fmt = "N" + (num.Length - pto - 1).ToString();
                num = numero.ToString(fmt, format);
                num = num.TrimEnd('0');
                num = num.TrimEnd(format.NumberDecimalSeparator.ToCharArray()[0]);
            } else {
                var fmt = "#" + format.NumberDecimalSeparator + "#";
                num = numero.ToString(fmt, format);
            }
            return num;
        }

        public void adicao(decimal outro_numero) {
            numero += outro_numero;
        }
        public void adicao(string outro_numero, NumberFormatInfo format) {
            if (outro_numero != "")
                adicao(Convert.ToDecimal(outro_numero, format));
        }

        public void subtracao(decimal outro_numero) {
            numero -= outro_numero;
        }
        public void subtracao(string outro_numero, NumberFormatInfo format) {
            if (outro_numero != "")
                subtracao(Convert.ToDecimal(outro_numero, format));
        }

        public void multiplicacao(decimal outro_numero) {
            numero *= outro_numero;
        }
        public void multiplicacao(string outro_numero, NumberFormatInfo format) {
            if (outro_numero != "")
                multiplicacao(Convert.ToDecimal(outro_numero, format));
        }

        public void divisao(decimal outro_numero) {
            numero /= outro_numero;
        }
        public void divisao(string outro_numero, NumberFormatInfo format) {
            if (outro_numero != "")
                divisao(Convert.ToDecimal(outro_numero, format));
        }

        public void potencia(decimal outro_numero) {
            numero = Convert.ToDecimal(
                Math.Pow(decimal.ToDouble(numero), decimal.ToDouble(outro_numero))
            );
        }
        public void potencia(string outro_numero, NumberFormatInfo format) {
            if (outro_numero != "")
                potencia(Convert.ToDecimal(outro_numero, format));
        }

        public void raiz(decimal outro_numero) {
            potencia(1 / outro_numero);
        }
        public void raiz(string outro_numero, NumberFormatInfo format) {
            if (outro_numero != "") {
                raiz(Convert.ToDecimal(outro_numero, format));
            }
        }

        public void seno() {
            numero = Convert.ToDecimal(
                Math.Sin(Math.PI * decimal.ToDouble(numero) / 180)
            );
        }
        public void coseno() {
            numero = Convert.ToDecimal(
                Math.Cos(Math.PI * decimal.ToDouble(numero) / 180)
            );
        }
        public void tangente() {
            numero = Convert.ToDecimal(
                Math.Tan(Math.PI * decimal.ToDouble(numero) / 180)
            );
        }

    }
}
