using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Teste_1___Codificador_e_Descodificador
{
    class Matriz
    {
        //Possui todos os atributos e métodos compartilhados pelas classes Codificacao e Descodificacao

        #region Atributos
        private char[,] matrizFrase;
        #endregion

        #region Getters e Setters
        protected char[,] MatrizFrase
        {
            get { return matrizFrase; }
            set { matrizFrase = value; }
        }
        #endregion

        #region Construtor
        public Matriz(string frase)
        {
            matrizFrase = new char[frase.Length, frase.Length];
        }
        public Matriz()
        {

        }
        #endregion

        #region Métodos
        #region Ordenação
        public int selectionSort(char[,] matrizFrase)
        {
            int ondeEstaPalavraCerta = 0; //guarda posição do indice de entrada
            for (int linha = 0; linha < matrizFrase.GetLength(0); linha++)
            {
                for (int linhaMais1 = linha + 1; linhaMais1 < matrizFrase.GetLength(0); linhaMais1++)
                {
                    if (char.ToLower(matrizFrase[linha, 0]) > char.ToLower(matrizFrase[linhaMais1, 0]))
                    {
                        trocarLinha(linha, linhaMais1, matrizFrase);
                        trocarIndice(linha, linhaMais1, ref ondeEstaPalavraCerta);
                    }
                    else if (char.ToLower(matrizFrase[linha, 0]) == char.ToLower(matrizFrase[linhaMais1, 0]))
                        Recursivo(linha, linhaMais1, matrizFrase, 0, ref ondeEstaPalavraCerta);
                    #region Não recursivo Comentado
                    //  Não recursivo
                    //    int indice = 1;
                    //    do
                    //    {
                    //        if (char.ToLower(matrizFrase[linha, indice]) > char.ToLower(matrizFrase[linhaMais1, indice]))
                    //        {
                    //            trocarLinha(linha, linhaMais1, matrizFrase);
                    //            trocarIndice(linha, linhaMais1, ref ondeEstaPalavraCerta);
                    //        }
                    //        else
                    //            indice++;
                    //    } while (char.ToLower(matrizFrase[linha, indice - 1]) == char.ToLower(matrizFrase[linhaMais1, indice - 1]) && indice < matrizFrase.GetLength(1));
                    //}
                    #endregion
                }
            }
            return ondeEstaPalavraCerta;
        }
        private void Recursivo(int linha1, int linha2, char[,] matrizFrase, int indice, ref int ondeEstaPalavraCerta)
        {
            if (indice > matrizFrase.GetLength(1) - 1)
            {
                //Base
            }
            else if (char.ToLower(matrizFrase[linha1, indice]) > char.ToLower(matrizFrase[linha2, indice]))
            {
                trocarLinha(linha1, linha2, matrizFrase);
                trocarIndice(linha1, linha2, ref ondeEstaPalavraCerta);
            }
            else if (char.ToLower(matrizFrase[linha1, indice]) == char.ToLower(matrizFrase[linha2, indice]))
                Recursivo(linha1, linha2, matrizFrase, indice + 1, ref ondeEstaPalavraCerta);

        }
        private void trocarLinha(int linha1, int linha2, char[,] matrizFrase)
        {
            for (int coluna = 0; coluna < matrizFrase.GetLength(1); coluna++)
            {
                char aux = matrizFrase[linha1, coluna];
                matrizFrase[linha1, coluna] = matrizFrase[linha2, coluna];
                matrizFrase[linha2, coluna] = aux;
            }
        }
        private void trocarIndice(int linha1, int linha2, ref int ondeEstaPalavraCerta)
        {
            if (linha1 == ondeEstaPalavraCerta)
                ondeEstaPalavraCerta = linha2;
            else
                ondeEstaPalavraCerta = linha1;
        }
        #endregion
        public void escreverMatriz()
        {
            //para testes
            for (int linha = 0; linha < matrizFrase.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < matrizFrase.GetLength(1); coluna++)
                {
                    Console.Write(matrizFrase[linha, coluna]);
                }
                Console.WriteLine();
            }
        }
        protected virtual void criacaoMatriz()
        {
            //será implementado nas classes derivadas
        }
        #endregion
    }
    class Codificacao: Matriz
    {
        #region Atributos
        public string frase;
        #endregion

        #region Construtor
        public Codificacao(string frase): base(frase)
        {
            this.frase = frase;
        }
        #endregion

        #region Métodos
        public string retornaRespostaCodificada()
        {
            int indiceEntrada;

            criacaoMatriz();

            //começa ordenação
            indiceEntrada = 0;

            //["nomeCodificado", Indice utilizado]
            return "[\'" + retornaUltimaColuna() + "\', " + indiceEntrada + "]";
        }
        #region Primeira Etapa
        protected override void criacaoMatriz()
        {
            //preenchimento da matriz usando shiftRightLogical
            char[] encode = frase.ToCharArray();

            for (int linha = 0; linha < MatrizFrase.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < MatrizFrase.GetLength(1); coluna++)
                    MatrizFrase[linha, coluna] = encode[coluna];
                encode = shiftRightLogical(encode);
            }
        }
        private char[] shiftRightLogical(char[] frase)
        {
            //envia o último caracter para a primeira posição da linha
            char[] resposta = new char[frase.Length];
            resposta[0] = frase[frase.Length - 1]; //o primeiro caracter, recebe o ultimo caracter

            for (int coluna = 1; coluna < resposta.Length; coluna++)
            {
                resposta[coluna] = frase[coluna - 1];
            }
            return resposta;
        }
        #endregion
        #region Segunda Etapa
        private string retornaUltimaColuna()
        {
            char[] teste = new char[MatrizFrase.GetLength(0)];

            for (int posicao = 0; posicao < MatrizFrase.GetLength(1); posicao++) //Salva toda a ultima coluna em um vetor do tipo char
                teste[posicao] = MatrizFrase[posicao, MatrizFrase.GetLength(1) - 1];

            return new string(teste);
        }
        #endregion
        #endregion
    }
    class Decodificacao: Matriz
    {
        #region Atributos
        private string fraseCriptografada;
        private int indiceFrase;
        #endregion

        #region Construtores
        public Decodificacao(string fraseCompleta):base()
        {
            //Regex usado para separar os dados frase e indice
            Regex retirandoInformacoes = new Regex(@"\[\'(.*)\',\s?(\d+)\]");
            Match encontrados = retirandoInformacoes.Match(fraseCompleta);

            fraseCriptografada = encontrados.Groups[1].Value;
            indiceFrase = Convert.ToInt32(encontrados.Groups[2].Value);
            base.MatrizFrase = new char[fraseCriptografada.Length, fraseCriptografada.Length];
        }
        #endregion

        #region métodos
        public string retornaRespostaDescodificada()
        {
            criacaoMatriz();
            return retornaLinha();
        }
        protected override void criacaoMatriz()
        {
            for (int colunas = fraseCriptografada.Length - 1; colunas > -1; colunas--)
            {
                //Add
                for (int linha = 0; linha < fraseCriptografada.Length; linha++)
                    MatrizFrase[linha, colunas] = fraseCriptografada[linha];

                //Sort

            }
        }
        private string retornaLinha()
        {
            //Recolhendo a linha com a palavra correta
            char[] palavraCorreta = new char[fraseCriptografada.Length];
            for (int pos = 0; pos < fraseCriptografada.Length; pos++)
            {
                palavraCorreta[pos] = MatrizFrase[indiceFrase, pos];
            }

            return new string(palavraCorreta);
        }
        #endregion
    }
    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
