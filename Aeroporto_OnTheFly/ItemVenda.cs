using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeroporto_OnTheFly
{
    internal class ItemVenda
    {
        public int IDVenda { get; set; }
        public int IDPassagem { get; set; }
        public float ValorUnitario { get; set; }

        public ItemVenda(int idvenda, int idpassagem, float valorunitario)
        {
            this.IDVenda = idvenda;
            this.IDPassagem = idpassagem;
            this.ValorUnitario = valorunitario;
        }
        public void Cadastro()
        {
            //InternalControlDB db = new InternalControlDB();
            Console.WriteLine("\n>>>INFORMAÇÕES ESPECÍFICAS DE CADA VENDA<<<:\n ");

            Console.WriteLine("Informe o ID da Venda: ");
            IDVenda = int.Parse(Console.ReadLine());
            Console.WriteLine("Informe o ID da Passagem: ");
            IDPassagem = int.Parse(Console.ReadLine());
            Console.WriteLine("Valor Unitário de cada Passagem: ");
            ValorUnitario = float.Parse(Console.ReadLine());



        }

    }
}
