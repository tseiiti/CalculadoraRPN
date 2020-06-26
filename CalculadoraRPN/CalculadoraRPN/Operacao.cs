using System;
using System.Collections.Generic;
using System.Text;

namespace CalculadoraRPN {
    class Operacao {
        public decimal numero;

        public Operacao(decimal numero) {
            this.numero = numero;
        }

        public Operacao(string numero) {
            this.numero = Convert.ToDecimal(numero);
        }

        public string getNumero() {
            return (new String(' ', 29 - numero.ToString().Length)) + numero.ToString();
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

    }
}
