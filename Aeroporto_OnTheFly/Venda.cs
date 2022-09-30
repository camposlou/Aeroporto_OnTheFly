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
            InternalControlDB db = new InternalControlDB();
            Console.WriteLine("\n>>>DIGITE AS INFORMAÇÕES DA VENDA ABAIXO<<<:\n ");

            Console.WriteLine("\n\t>>> Escolha o(s) Passageiro(s) Abaixo: <<<");
            String sql = $"SELECT CPF, Nome, Sexo, Data_Nascimento, Situacao, Data_UltimaCompra, Data_Cadastro From Passageiro WHERE Situacao = 'A';";
            db.LocalizarDadoPassageiro(sql);            
            Console.Write("Digite o CPF do Passageiro selecionado: ");
            CPF = Console.ReadLine();

            DataVenda = DateTime.Now;
            Console.WriteLine("Valor Total: ");
            ValorTotal = float.Parse(Console.ReadLine());
           

           




        }
    }
}
