using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aeroporto_OnTheFly
{
    internal class Aeronave
    {

        public string Inscricao { get; set; }
        public string CNPJ { get; set; }
        public int Capacidade { get; set; }
        public char Situacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataUltVenda { get; set; }

        
        public Aeronave(string inscricao, int capacidade, DateTime DataUltVenda, DateTime DataCadastro, char situacao, string cnpj)
        {
            this.Inscricao = inscricao;
            this.CNPJ = cnpj;
            this.Capacidade = capacidade;
            this.DataUltVenda = System.DateTime.Now;
            this.DataCadastro = System.DateTime.Now;
            this.Situacao = situacao;

        }
        public void Cadastro()
        {
            //InternalControlDB db = new InternalControlDB();

            Console.WriteLine("\n>>>DIGITE AS INFORMAÇÕES DA AERONAVE ABAIXO<<<:\n ");

            Console.WriteLine("Número de Inscrição da Aeronave: ");
            Inscricao = Console.ReadLine();
            Console.WriteLine("CNPJ: ");
            CNPJ = Console.ReadLine();
            Console.WriteLine("Capacidade: ");
            Capacidade = int.Parse(Console.ReadLine());
            DataUltVenda = DateTime.Now;
            DataCadastro = DateTime.Now;

            do
            {
                Console.Write("Situação [A] Ativo [I] Inativo: ");
                Situacao = char.Parse(Console.ReadLine().ToUpper());
                if (Situacao == '0')
                    return;
                if (Situacao != 'I' && Situacao != 'A')
                {
                    Console.WriteLine("Digite um opção válida!!!");
                }
            } while (Situacao != 'I' && Situacao != 'A');

              

        }
    }
}
