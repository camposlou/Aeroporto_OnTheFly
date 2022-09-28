using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeroporto_OnTheFly
{
    internal class Voo
    {
        public string IDVoo { get; set; }
        public string Destino { get; set; }
        public string InscricaoAer { get; set; }
        public DateTime DataHoraVoo { get; set; }
        public int AssentosOcup { get; set; }
        public char Situacao { get; set; }

       
        public Voo(string idVoo, string destino, string inscricaoaer, DateTime dataHoraVoo, int assentosocup, char situacao)
        {
            this.IDVoo = idVoo;
            this.Destino = destino;
            this.InscricaoAer = inscricaoaer;
            this.DataHoraVoo = dataHoraVoo;
            this.AssentosOcup = assentosocup; 
            this.Situacao = situacao; 
        }
        public void Cadastro()
        {
            //InternalControlDB db = new InternalControlDB();

            Console.WriteLine("\n>>>DIGITE AS INFORMAÇÕES DA VOO ABAIXO<<<:\n ");

            Console.WriteLine("Número do ID do Voo : ");
            IDVoo = Console.ReadLine();
            Console.WriteLine("Destino: ");
            Destino = Console.ReadLine();
            Console.WriteLine("Inscrição da Aeronave: ");
            InscricaoAer = Console.ReadLine();
            DataHoraVoo = DateTime.Now;
            Console.WriteLine("Digite a quantidade de Assentos ocupados: ");
            AssentosOcup = int.Parse(Console.ReadLine());

            do
            {
                Console.Write("Situação [A] Ativo [C] Cancelado: ");
                Situacao = char.Parse(Console.ReadLine().ToUpper());
                if (Situacao == '0')
                    return;
                if (Situacao != 'C' && Situacao != 'A')
                {
                    Console.WriteLine("Digite um opção válida!!!");
                }
            } while (Situacao != 'C' && Situacao != 'A');
                 
           


        }

    }
}
