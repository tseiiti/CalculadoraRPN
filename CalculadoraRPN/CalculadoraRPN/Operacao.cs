using System;
using System.Collections.Generic;
using System.Text;

namespace CalculadoraRPN {
    class Operacao {
        public decimal numero;
        public string operacao;

        public Operacao(decimal numero, string operacao) {
            this.numero = numero;
            this.operacao = operacao;
        }

        public Operacao(string numero, string operacao) {
            this.numero = Convert.ToDecimal(numero); ;
            this.operacao = operacao;
        }

        public string getNumero() {
            return numero.ToString();
        }

        public decimal adicao(decimal outro_numero) {
            return this.numero + outro_numero;
        }

        public string adicao(string outro_numero) {
            return (this.numero + Convert.ToDecimal(outro_numero)).ToString();
        }

    }
}
