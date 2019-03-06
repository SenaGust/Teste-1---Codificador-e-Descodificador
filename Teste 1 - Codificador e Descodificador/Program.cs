using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Teste_1___Codificador_e_Descodificador
{
    class Codificacao
    {
        #region Atributos
        public string frase;
        private char[,] matrizFrase;
        #endregion

        #region Construtor
        public Codificacao(string frase)
        {
            this.frase = frase;
            matrizFrase = new char[frase.Length, frase.Length];
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
