using System;
using System.Collections.Generic;
using System.Globalization;

namespace CalculadoraRPN {
	class Calculadora {
		List<Operacao> operacoes;
		NumberFormatInfo format;
		Tela tela;
		ConsoleKeyInfo key;
		string numero;
		string comando;
		int tamanho;

		public Calculadora() {
			this.operacoes = new List<Operacao>();
			this.format = new CultureInfo("pt-BR").NumberFormat;
			this.format.NumberDecimalSeparator = ".";
			this.format.NumberGroupSeparator = ",";
			this.tela = new Tela();
			this.numero = "";
			this.comando = "";
			this.tamanho = 41;
		}

		public void executa() {
			// Trata CTL+C
			Console.TreatControlCAsInput = true;

			this.tela.exibir(this.operacoes, this.format, this.tamanho);
			do {
				this.key = Console.ReadKey(true);
				this.tela.converte_tecla(this.format, this.key, ref this.numero, ref this.comando);
				valida_tecla();
			} while (this.key.Key != ConsoleKey.Escape);
		}

		private void valida_tecla() {
			Console.WriteLine(this.numero + " " + this.comando);
		}

		void add_operacao() {
			var count = this.operacoes.Count;
			if (this.numero == this.format.CurrencyDecimalSeparator.ToString())
				this.numero = "";

			if (this.numero == "" && count > 0)
				this.numero = this.operacoes[count - 1].get_numero(this.format);

			if (this.numero != "") {
				this.operacoes.Add(new Operacao(this.numero, this.format));
				this.numero = "";
			}
			this.tela.exibir(this.operacoes, this.format, this.tamanho);
		}

		void del_operacao() {
			if (this.numero != "") this.numero = "";
			else if (this.operacoes.Count > 0)
				this.operacoes.RemoveAt(this.operacoes.Count - 1);
			this.tela.exibir(this.operacoes, this.format, this.tamanho);
		}

		void aritimetica(string operacao) {
			int count = this.operacoes.Count;

			if (this.numero == "" && count > 1) {
				this.numero = this.operacoes[count - 1].get_numero(this.format);
				this.operacoes.RemoveAt(count - 1);
				count--;
			}

			if (count > 0) {
				if (operacao == "+") {
					this.operacoes[count - 1].adicao(this.numero, this.format);
				} else if (operacao == "-") {
					this.operacoes[count - 1].subtracao(this.numero, this.format);
				} else if (operacao == "*") {
					this.operacoes[count - 1].multiplicacao(this.numero, this.format);
				} else if (operacao == "/") {
					this.operacoes[count - 1].divisao(this.numero, this.format);
				}
				this.numero = "";
				this.tela.exibir(this.operacoes, this.format, this.tamanho);
			} else if (this.numero != "") {
				add_operacao();
			} else {
				Console.WriteLine("erro");
			}
		}
	}
}
