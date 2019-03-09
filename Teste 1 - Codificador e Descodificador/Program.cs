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
            if(frase != null)
                matrizFrase = new char[frase.Length, frase.Length];
        }
        #endregion

        #region Métodos
        #region Ordenação
        protected int SelectionSort()
        {
            int ondeEstaPalavraCerta = 0; //guarda posição do indice de entrada
            for (int linha = 0; linha < matrizFrase.GetLength(1); linha++)
                for (int linhaMais1 = linha + 1; linhaMais1 < matrizFrase.GetLength(1); linhaMais1++)
                    Recursivo(linha, linhaMais1, 0, ref ondeEstaPalavraCerta);
            return ondeEstaPalavraCerta;
        }
        private void Recursivo(int linha1, int linha2, int indice, ref int ondeEstaPalavraCerta)
        {
            if(indice < matrizFrase.GetLength(1)) //PRIMEIRA BASE - Fim da coluna
                if (char.ToLower(matrizFrase[linha1, indice]) == char.ToLower(matrizFrase[linha2, indice])) //mesma letra ignorando maiusculo/minusculo?
                {
                    if (char.IsUpper(matrizFrase[linha2, indice]) && char.IsLower(matrizFrase[linha1, indice])) //a e A
                    {
                        TrocarLinha(linha1, linha2);
                        TrocarIndice(linha1, linha2, ref ondeEstaPalavraCerta);
                    }
                    else if (matrizFrase[linha1, indice] == matrizFrase[linha2, indice]) //"a e a" ou "A e A"
                        Recursivo(linha1, linha2, indice + 1, ref ondeEstaPalavraCerta);
                }
                else if (char.ToLower(matrizFrase[linha1, indice]) > char.ToLower(matrizFrase[linha2, indice])) //qual letra vem primeiro
                {
                    TrocarLinha(linha1, linha2);
                    TrocarIndice(linha1, linha2, ref ondeEstaPalavraCerta);
                }
        }
        private void TrocarLinha(int linha1, int linha2)
        {
            for (int coluna = 0; coluna < matrizFrase.GetLength(1); coluna++)
            {
                char aux = matrizFrase[linha1, coluna];
                matrizFrase[linha1, coluna] = matrizFrase[linha2, coluna];
                matrizFrase[linha2, coluna] = aux;
            }
        }
        private void TrocarIndice(int linha1, int linha2, ref int ondeEstaPalavraCerta)
        {
            if (linha1 == ondeEstaPalavraCerta)
                ondeEstaPalavraCerta = linha2;
            else if (linha2 == ondeEstaPalavraCerta)
                ondeEstaPalavraCerta = linha1;
        }
        #endregion
        public void EscreverMatriz()
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
        protected virtual void CriacaoMatriz()
        {
            //será implementado nas classes derivadas
        }
        #endregion
    }
    class Codificacao: Matriz
    {
        #region Atributos
        public string fraseDescodificada;
        #endregion

        #region Construtor
        public Codificacao(string frase): base(frase)
        {
            this.fraseDescodificada = frase;
        }
        #endregion

        #region Métodos
        public string RetornaRespostaCodificada()
        {
            int indiceEntrada;

            CriacaoMatriz();

            //começa ordenação
            indiceEntrada = SelectionSort();

            //["nomeCodificado", Indice utilizado]
            return "[\'" + RetornaUltimaColuna() + "\', " + indiceEntrada + "]";
        }
        #region Primeira Etapa
        protected override void CriacaoMatriz()
        {
            //preenchimento da matriz usando shiftRightLogical
            char[] encode = fraseDescodificada.ToCharArray();

            for (int linha = 0; linha < MatrizFrase.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < MatrizFrase.GetLength(1); coluna++)
                    MatrizFrase[linha, coluna] = encode[coluna];
                encode = ShiftRightLogical(encode);
            }
        }
        private char[] ShiftRightLogical(char[] frase)
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
        private string RetornaUltimaColuna()
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
        private string fraseCodificada;
        private readonly int indiceFrase;
        #endregion

        #region Construtores
        public Decodificacao(string fraseCompleta):base(null)
        {
            //Regex usado para separar os dados frase e indice
            Regex retirandoInformacoes = new Regex(@"\[\'(.*)\',\s?(\d+)\]");
            Match encontrados = retirandoInformacoes.Match(fraseCompleta);

            fraseCodificada = encontrados.Groups[1].Value;
            indiceFrase = Convert.ToInt32(encontrados.Groups[2].Value);
            base.MatrizFrase = new char[fraseCodificada.Length, fraseCodificada.Length];
        }
        #endregion

        #region métodos
        public string RetornaRespostaDescodificada()
        {
            //Retorna a resposta descodificada
            CriacaoMatriz();
            return RetornaLinhaCorreta();
        }
        protected override void CriacaoMatriz()
        {
            //Preenche uma matriz adicionando a frase encriptografada da ultima coluna até a primeira.
            //Após a adição de cada linha, ordeno cada uma das linhas em ordem alfabética
            for (int colunas = fraseCodificada.Length - 1; colunas > -1; colunas--)
            {
                //Add
                for (int linha = 0; linha < fraseCodificada.Length; linha++)
                    MatrizFrase[linha, colunas] = fraseCodificada[linha];

                //Sort
                SelectionSort();
            }
        }
        private string RetornaLinhaCorreta()
        {
            //Recolhendo a linha com a palavra correta
            char[] palavraCorreta = new char[fraseCodificada.Length];
            for (int pos = 0; pos < fraseCodificada.Length; pos++)
                palavraCorreta[pos] = MatrizFrase[indiceFrase, pos];

            return new string(palavraCorreta);
        }
        #endregion
    }
    class Program
    {
        static void Main(string[] args)
        {
            testar();
            //fim
            Console.WriteLine("\n\nPressiona qualquer tecla para continuar...");
            Console.ReadKey();
        }
        static void testar()
        {
            Decodificacao decode;
            Codificacao encode;
            string teste = "Complete Log Of This Run Can Be Found";
            string resultadoEncode, resultadoDecode;

            encode = new Codificacao(teste);
            resultadoEncode = encode.RetornaRespostaCodificada();

            decode = new Decodificacao(resultadoEncode);
            resultadoDecode = decode.RetornaRespostaDescodificada();

            Console.WriteLine("\n\nResultado");
            Console.WriteLine(resultadoDecode);
            Console.WriteLine(resultadoEncode);
            if (resultadoDecode == teste)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("DEU CERTO PORRA");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("DEU errado");
            }
            
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
