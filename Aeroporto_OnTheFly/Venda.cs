using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeroporto_OnTheFly
{
    internal class Venda
    {        
        public string CPF { get; set; }
        public DateTime DataVenda { get; set; }
        public float ValorTotal { get; set; }
        public Venda(string cpf, DateTime dataVenda, float valorTotal)
        {          
            this.CPF = cpf;
            this.DataVenda = dataVenda;
            this.ValorTotal = valorTotal;
        }
        public void Cadastro()
        {
            //InternalControlDB db = new InternalControlDB();
            Console.WriteLine("\n>>>DIGITE AS INFORMAÇÕES DA VENDA ABAIXO<<<:\n ");

            Console.WriteLine("CPF do Passageiro: ");
            CPF = Console.ReadLine();
            DataVenda = DateTime.Now;
            Console.WriteLine("Valor Total: ");
            ValorTotal = float.Parse(Console.ReadLine());
           

           




        }
    }
}
