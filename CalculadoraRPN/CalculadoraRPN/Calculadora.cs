﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace CalculadoraRPN {
	class Calculadora {
		List<Operacao> operacoes;
		NumberFormatInfo format;
		Tela tela;
		string numero;

		public Calculadora() {
			this.operacoes = new List<Operacao>();
			this.format = new CultureInfo("pt-BR", false).NumberFormat;
		}

		public void executa() {
			ConsoleKeyInfo key;
			string comando = "";
			this.numero = "";

			// Trata CTL+C
			Console.TreatControlCAsInput = true;

			this.tela = new Tela(this.format, 41, this.operacoes);
			do {
				key = Console.ReadKey(true);
				this.tela.converte_tecla(key, ref comando, ref this.numero);

				if (comando == "Help") {
					help();
				} else if (comando == "Menu") {
					menu();
				} else if (comando == "Enter") {
					add_operacao();
				} else if (comando == "Delete") {
					del_operacao();
				} else if ("+-*/pr".IndexOf(comando) != -1) {
					aritmetica(comando);
				}

			} while (key.Key != ConsoleKey.Escape);
		}

		/// <summary>
		/// Apresenta menu de ajuda
		/// </summary>
		void help() {
			Console.Clear();
			Console.WriteLine("Tecla H.....: Help");
			Console.WriteLine("Tecla M.....: Menu");
			Console.WriteLine("Tecla Delete: Exclui último número");
			Console.WriteLine("Tecla +.....: Adição");
			Console.WriteLine("Tecla -.....: Subtração");
			Console.WriteLine("Tecla *.....: Multiplicação");
			Console.WriteLine("Tecla /.....: Divisão");
			Console.WriteLine("Tecla P.....: Potênciação");
			Console.WriteLine("Tecla R.....: Raiz quadrada");
			Console.WriteLine("\nPressione qualquer tecla para voltar...");
			Console.ReadKey(true);
		}

		/// <summary>
		/// Menu para selecionar separador decimal entre ponto ou vírgula
		/// </summary>
		void menu() {
			ConsoleKeyInfo k;
			string resp = "";

			Console.Clear();
			Console.WriteLine("Separador Decimal (. = Ponto, , = Vírgula):");

			while (resp != "." && resp != ",") {
				k = Console.ReadKey(true);
				resp = k.KeyChar.ToString();
				if (resp == ".") {
					//this.format = new CultureInfo("en-US").NumberFormat;
					this.format.NumberDecimalSeparator = ".";
					this.format.NumberGroupSeparator = ",";
				}
				if (resp == ",") {
					//this.format = new CultureInfo("pt-BR").NumberFormat;
					this.format.NumberDecimalSeparator = ",";
					this.format.NumberGroupSeparator = ".";
				}
			}
			this.tela = new Tela(this.format, 41, this.operacoes);
		}

		/// <summary>
		/// Adicionar um novo número na lista de operações.
		/// Caso o número a ser adicionado seja vazio, repete o último 
		/// número da lista.
		/// </summary>
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
			this.tela.exibir(this.operacoes);
		}

		/// <summary>
		/// Exclui o número em preenchimento.
		/// Caso o número em preenchimento já esteja vazio, exclui o último 
		/// número da lista.
		/// </summary>
		void del_operacao() {
			if (this.numero != "") this.numero = "";
			else if (this.operacoes.Count > 0)
				this.operacoes.RemoveAt(this.operacoes.Count - 1);
			this.tela.exibir(this.operacoes);
		}

		/// <summary>
		/// Executa uma operação aritmética entre o número digitado e o último 
		/// número da lista. Caso o número digitado esteja vazio, executa a 
		/// operação entre o último e o penúltimo número da lista.
		/// </summary>
		/// <param name="operacao">atualmente pode ser "+", "-", "*" ou "/"</param>
		void aritmetica(string operacao) {
			int count = this.operacoes.Count;

			// raiz quadrada por padrão
			if (operacao == "r" && this.numero == "") {
				this.numero = "2";
			}

			// permite calcular com os números que estão na tela
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
				} else if (operacao == "p") {
					this.operacoes[count - 1].potencia(this.numero, this.format);
				} else if (operacao == "r") {
					var aux = 1 / Convert.ToDecimal(this.numero, this.format);
					this.numero = new Operacao(aux).get_numero(this.format);
					this.operacoes[count - 1].potencia(this.numero, this.format);
				}
				this.numero = "";
				this.tela.exibir(this.operacoes);
			} else {
				Console.WriteLine("erro");
			}
		}
	}
}
