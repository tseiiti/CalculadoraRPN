using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CalculadoraRPN {
	enum Comando {
		Adição			= 1,
		Subtração		= 2,
		Multiplicação	= 4,
		Divisão			= 8,
		Potenciação		= 16,
		Radiciação		= 32,
		Seno			= 64,
		Coseno			= 128,
		Tangente		= 256,
		Aritmética		= Adição | Subtração | Multiplicação | Divisão | Potenciação | Radiciação,
		Trigonométrica	= Seno | Coseno | Tangente,
		Ajuda,
		Menu,
		Enter,
		Delete,
		Desfazer,
		Refazer,
		Sair,
		None
	}

	class Calculadora {
		// enviar a collection para classe Numero
		List<Numero> numeros;
		List<Operacao> operacoes;
		int index_ope;
		NumberFormatInfo format;
		Tela tela;
		string numero;

		public void executa() {
			operacoes = new List<Operacao>();
			numeros = new List<Numero>();
			format = new CultureInfo("pt-BR").NumberFormat;
			tela = new Tela(format, numeros);
			index_ope = -1;
			numero = "";
			Comando comando;
			
			// motor de execução
			do {
				// input do teclado
				comando = tela.converte_tecla(ref numero);

				if (comando == Comando.Ajuda) {
					tela.ajuda();
				} else if (comando == Comando.Menu) {
					format = tela.menu();
				} else if (comando == Comando.Enter) {
					add_numeros();
				} else if (comando == Comando.Delete) {
					del_numeros();
				} else if (comando == Comando.Desfazer) {
					desfazer();
				} else if (comando == Comando.Refazer) {
					refazer();
				} else if ((Comando.Trigonométrica & comando) == comando) {
					operacao1(comando);
				} else if ((Comando.Aritmética & comando) == comando) {
					operacao2(comando);
				}

			} while (comando != Comando.Sair);
		}

		private void desfazer() {
			if (index_ope < 0) return;

			var operacao = operacoes[index_ope];
			if (operacao.comando == Comando.Enter) {
				numeros.RemoveAt(operacao.index);
			} else if (operacao.comando == Comando.Delete) {
				numeros.Add(new Numero(operacao.ultimo, format));
			} else if ((Comando.Trigonométrica & operacao.comando) == operacao.comando) {
				numeros.RemoveAt(operacao.index);
				numeros.Add(new Numero(operacao.ultimo, format));
			} else if ((Comando.Aritmética & operacao.comando) == operacao.comando) {
				numeros.RemoveAt(operacao.index);
				numeros.Add(new Numero(operacao.anterior, format));
				numeros.Add(new Numero(operacao.ultimo, format));
			} else return;
			index_ope--;
			tela.exibir(numeros, numero);
		}

		private void refazer() {
			if (index_ope > operacoes.Count - 2) return;

			var operacao = operacoes[index_ope + 1];
			if (operacao.comando == Comando.Enter) {
				numeros.Add(new Numero(operacao.ultimo, format));
			} else if (operacao.comando == Comando.Delete) {
				numeros.RemoveAt(operacao.index);
			} else if ((Comando.Trigonométrica & operacao.comando) == operacao.comando) {
				numeros.RemoveAt(operacao.index);
				numeros.Add(new Numero(operacao.resultado, format));
			} else if ((Comando.Aritmética & operacao.comando) == operacao.comando) {
				numeros.RemoveAt(operacao.index);
				numeros.RemoveAt(numeros.Count - 1);
				numeros.Add(new Numero(operacao.resultado, format));
			} else return;
			index_ope++;
			tela.exibir(numeros, numero);
		}

		private void add_operacao(Comando comando) {
			var operacao = new Operacao {
				comando = comando,
				numero = numero,
				ultimo = numeros.Last().get_numero(format),
				anterior = numeros.Count > 1 ? numeros[^2].get_numero(format) : null,
				index = numeros.Count - 1
			};
			index_ope++;
			operacoes.RemoveRange(index_ope, operacoes.Count - index_ope);
			operacoes.Add(operacao);
		}

		/// <summary>
		/// Adicionar um novo número na lista.
		/// Caso o número a ser adicionado seja vazio, repete o último 
		/// número da lista.
		/// </summary>
		private void add_numeros() {
			if (numero == format.CurrencyDecimalSeparator.ToString())
				numero = "";

			// permite repetir o último número
			if (numero == "" && numeros.Count > 0)
				numero = numeros[^1].get_numero(format);

			if (numero != "") {
				numeros.Add(new Numero(numero, format));
				numero = "";

				// histórico de operações - número depois de adicionado
				add_operacao(Comando.Enter);
			}
			tela.exibir(numeros, numero);
		}

		/// <summary>
		/// Exclui o número em preenchimento.
		/// Caso o número em preenchimento já esteja vazio, exclui o último 
		/// número da lista.
		/// </summary>
		private void del_numeros() {
			// para facilitar o histórico
			if (numero != "") add_numeros();

			// verifica se pode deletar algo
			if (numeros.Count == 0) return;

			// histórico de operações - número depois de excluido
			add_operacao(Comando.Delete);

			numeros.RemoveAt(numeros.Count - 1);
			tela.exibir(numeros, numero);
		}

		/// <summary>
		/// Executa uma operação (conversão) trigonométrica do número digitado em graus. Caso o número 
		/// digitado esteja vazio, executa a operação do último número da lista.
		/// </summary>
		/// <param name="operacao"></param>
		private void operacao1(Comando comando) {
			// permite calcular seno, coseno ou tangente com os números que estão na tela
			if (numero != "") add_numeros();

			if (numeros.Count > 0) {
				// histórico de operações - números antes do cálculo
				add_operacao(comando);

				if (comando == Comando.Seno) {
					numeros.Last().seno();
				} else if (comando == Comando.Coseno) {
					numeros.Last().coseno();
				} else if (comando == Comando.Tangente) {
					numeros.Last().tangente();
				} else return;

				// histórico de operações - atualiza resultado do histórico
				operacoes.Last().resultado = numeros.Last().get_numero(format);
				operacoes.Last().index = numeros.Count - 1;

				tela.exibir(numeros, numero);
			} else {
				Console.WriteLine("erro");
			}
		}

		/// <summary>
		/// Executa uma operação aritmética entre o número digitado e o último 
		/// número da lista. Caso o número digitado esteja vazio, executa a 
		/// operação entre o último e o penúltimo número da lista.
		/// </summary>
		/// <param name="comando"></param>
		private void operacao2(Comando comando) {
			// raiz quadrada por padrão
			if (comando == Comando.Radiciação && numero == "") numero = "2";

			if (numero != "") add_numeros();

			//// faz cálculos com o último e penúltimo número
			//if (numero == "" && numeros.Count > 1) {
			//	numero = numeros.Last().get_numero(format);
			//	numeros.RemoveAt(numeros.Count - 1);
			//}

			if (numeros.Count > 1) {
				// histórico de operações - números antes do cálculo
				add_operacao(comando);

				if (comando == Comando.Adição) {
					numeros[^2].adicao(numeros.Last().numero);
				} else if (comando == Comando.Subtração) {
					numeros[^2].subtracao(numeros.Last().numero);
				} else if (comando == Comando.Multiplicação) {
					numeros[^2].multiplicacao(numeros.Last().numero);
				} else if (comando == Comando.Divisão) {
					numeros[^2].divisao(numeros.Last().numero);
				} else if (comando == Comando.Potenciação) {
					numeros[^2].potencia(numeros.Last().numero);
				} else if (comando == Comando.Radiciação) {
					numeros[^2].raiz(numeros.Last().numero);
				} else return;
				numeros.RemoveAt(numeros.Count - 1);

				// histórico de operações - atualiza resultado do histórico
				operacoes.Last().resultado = numeros.Last().get_numero(format);
				operacoes.Last().index = numeros.Count - 1;

				tela.exibir(numeros, numero);
			}
		}
	}
}
