using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeroporto_OnTheFly
{
    internal class Passagem
    {
        public string IDPassagem { get; set; }
        public string IDVoo { get; set; }
        public DateTime DataUltOperacao { get; set; }
        public float Valor { get; set; }
        public char Situacao { get; set; }
        public Passagem(string idPassagem, string idVoo, DateTime dataUltOperacao, float valor, char Situacao)
        {
            this.IDPassagem = idPassagem;
            this.IDVoo = idVoo;
            this.DataUltOperacao = dataUltOperacao;
            this.Valor = valor;
            this.Situacao = Situacao;
        }
        public void Cadastro()
        {
            InternalControlDB db = new InternalControlDB();

            Console.WriteLine("\n>>>DIGITE AS INFORMAÇÕES DA PASSAGEM ABAIXO<<<:\n ");

            Console.WriteLine("Número do ID da Passagem: ");
            IDPassagem = Console.ReadLine();
            Console.WriteLine("Número do ID do Voo: ");
            IDVoo = Console.ReadLine();
            DataUltOperacao = DateTime.Now;
            Console.WriteLine("Valor da Passagem: ");
            Valor = float.Parse(Console.ReadLine());

            do
            {
                Console.Write("Situação [L] Livre [R] Reservada [P] Paga: ");
                Situacao = char.Parse(Console.ReadLine().ToUpper());
                if (Situacao == '0')
                    return;
                if (Situacao != 'R' && Situacao != 'P' && Situacao != 'L')
                {
                    Console.WriteLine("Digite um opção válida!!!");
                }
            } while (Situacao != 'R' && Situacao != 'P' && Situacao != 'L');


        }
        
    }
}
