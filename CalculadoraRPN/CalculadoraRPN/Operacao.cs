using System;
using System.Collections.Generic;
using System.Text;

namespace CalculadoraRPN {
    class Operacao {
        public string numero;
        public int index;
        public string ultimo;
        public string anterior;
        public string resultado;
        public Comando comando;
    }
}
