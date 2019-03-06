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
        #region Primeira Etapa
        protected override void criacaoMatriz()
        {
            //preenchimento da matriz 
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
        public string retornaRespostaCodificada()
        {
            int indiceEntrada;

            criacaoMatriz();

            //começa ordenação
            indiceEntrada = 0;

            //["nomeCodificado", Indice utilizado]
            return "[\'" + retornaUltimaColuna() + "\', " + indiceEntrada + "]";
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
