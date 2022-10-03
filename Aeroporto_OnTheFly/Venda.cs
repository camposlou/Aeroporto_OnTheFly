using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aeroporto_OnTheFly
{
    internal class Venda
    {
        public string IDVenda { get; set; }
        public string CPF { get; set; }
        public DateTime DataVenda { get; set; }
        public float ValorTotal { get; set; }
        public float ValorUnitario { get; set; }
        public string IDPassagem { get; set; }
                
        InternalControlDB db = new();

        public Venda() { }

        public Venda(string idvenda, string cpf, DateTime dataVenda, float valortotal, float valorUnitario, string idpassagem)
        {
            this.IDVenda = idvenda;
            this.CPF = cpf;
            this.DataVenda = dataVenda;
            this.ValorTotal = valortotal;
            this.ValorUnitario = valorUnitario;
            this.IDPassagem = idpassagem;

        }
        public bool GeraId()
        {
            for (int i = 1; i <= 999; i++)
            {
                if (!db.VerifExistente(i.ToString(), "IDVenda", "Venda"))
                {
                    IDVenda = i.ToString();
                    break;
                }
            }
            Console.WriteLine($"ID Venda: {this.IDVenda}\n");
            return true;
        }
        #region Insert Venda
        public void CadastroVenda()
        {
            Console.Clear();
            Console.WriteLine("\n>>>REGISTRAR UMA VENDA ABAIXO<<<:\n ");
            if (!GeraId())
                return;

            Console.WriteLine("\n\t>>> Escolha o Passageiro: <<<");
            String sql = $"SELECT CPF, Nome, Sexo, Data_Nascimento, Situacao, Data_UltimaCompra, Data_Cadastro From Passageiro WHERE Situacao = 'A';";
            db.LocalizarDadoPassageiro(sql);

            Console.Write("Digite o CPF do Passageiro selecionado: ");
            CPF = Console.ReadLine();
            while (true)
            {
                Console.WriteLine("\n\tVerificação de CPF Bloqueado");
                if (db.LocalizarBloqueados(CPF, "CPF", "CadastrosBloqueados"))
                {
                    Console.WriteLine("CPF Bloqueado! Não é possível efetuar Venda. Tecle Enter para sair!!!");
                    Thread.Sleep(2000);
                }
                else
                {
                    Console.WriteLine("CPF Apto!!!!");
                    break;
                }
            }
            DataVenda = DateTime.Now;

            Console.Write("Valor Unitário de cada Passagem: ");
            ValorUnitario = float.Parse(Console.ReadLine());

            int quantidade;
            do
            {
                Console.Write("\nDigite a quantidade de passagem que deseja comprar: [máx 4 passagens]: ");
                quantidade = int.Parse(Console.ReadLine());
                if (quantidade > 4 || quantidade <= 0)
                {
                    Console.WriteLine("Impossivel comprar mais que 4 passagens!");
                }
            } while (quantidade > 4 || quantidade <= 0);

            Console.WriteLine("\n\t>>> Escolha a Passagem: <<<");
            sql = $"SELECT IDPassagem, IDVoo, Valor, Situacao, Data_UltimaOperacao From Passagem WHERE Situacao = 'L';";
            db.LocalizarDadoPassagem(sql);
            if (!VerificaIDPassagem())
                return;

            ValorTotal = ValorUnitario * quantidade;
            for (int i = 1; i <= quantidade; i++)
            {
                
                CadastroItemVenda();
            }  
            
        }
        #endregion

        #region
        public void CadastroItemVenda()
        {
            Console.WriteLine("\nDeseja efetuar a gravação? Digite 1- Sim / 2-Não: ");
             Console.Write("Digite: ");
             int opc = int.Parse(Console.ReadLine());

             if (opc == 1)
             {
                 String sql = $"INSERT INTO Venda (IDVenda, CPF, Data_Venda, Valor_Total, Valor_Unitario, IDPassagem) VALUES ('{this.IDVenda}' , " +
                      $"'{this.CPF}', '{this.DataVenda}', '{this.ValorTotal}', '{this.ValorUnitario}', '{this.IDPassagem}');";

                 db.InserirDado(sql);

                 Console.WriteLine("\nGravação efetuada com sucesso! Aperte ENTER para retornar ao Menu.");
                 Console.ReadKey();
             }
             else
             {
                 Console.WriteLine("\nGravação não efetuada! Aperte ENTER para retornar ao Menu.");
                 Console.ReadKey();
             }
            Console.WriteLine("Deseja  atualizar a situação da(s) Passagem(s) selecionada(s)? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                Passagem editpass = new Passagem();
                editpass.EditarSituacaoPassagem();
            }

            else
            {
                Console.WriteLine("\n Aperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }

        }
        #endregion

        #region Select Venda Especifica
        public void LocalizarVenda()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Localizar Venda Especifica <<<");
            Console.Write("\nDigite o ID da Venda: ");
            this.IDVenda = Console.ReadLine();

            Console.WriteLine("\nDeseja Continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT IDVenda, CPF, Data_Venda, Valor_Total From Venda WHERE IDVenda=('{this.IDVenda}');";
                db.LocalizarDadoVenda(sql);

                Console.WriteLine("\nAperte ENTER para Retornar ao Menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a localização da venda! Aperte ENTER para retornar ao Menu.");
                Thread.Sleep(2000);
            }
        }
        #endregion

        #region Select Lista de Vendas
        public void ConsultarListaVendas()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Lista de Venda(s) <<<");
            Console.WriteLine("\nDeseja continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {

                String sql = $"SELECT IDVenda, CPF, Data_Venda, Valor_Total From Venda";                
                db.LocalizarDadoVenda(sql);

                Console.WriteLine("\nAperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a consulta da(s) venda(s)! Aperte ENTER para retornar ao Menu.");

            }
        }
        #endregion

        #region Update Vendas
        public void EditarVenda()
        {
            int opc = 0;
            String sql = "";
            Console.Clear();
            Console.Write("\nDigite a ID da Venda: ");
            this.IDVenda = Console.ReadLine();

            sql = $"SELECT IDVenda, CPF, Data_Venda, Valor_Total From Venda";
            db = new InternalControlDB();

            if (!string.IsNullOrEmpty(db.LocalizarDadoVenda(sql)))
            {
                Console.WriteLine("\nSelecione a opção que deseja editar");
                Console.WriteLine("1-CPF");
                Console.WriteLine("2-Valor Total");
                Console.Write("\nDigite: ");
                opc = int.Parse(Console.ReadLine());
                while (opc < 1 || opc > 5)
                {
                    Console.WriteLine("\nDigite uma opção válida:");
                    Console.Write("\nDigite: ");
                    opc = int.Parse(Console.ReadLine());

                }
                switch (opc)
                {
                    case 1:
                        Console.Write("\nAlterar para: ");
                        this.CPF = Console.ReadLine();
                        sql = $"Update Venda Set CPF = ('{this.CPF}') Where IDVenda = ('{this.IDVenda}')";
                        break;
                    case 2:
                        Console.Write("\nAlterar para: ");
                        this.ValorTotal = float.Parse(Console.ReadLine());
                        sql = $"Update Venda Set Valor_Total = ('{this.ValorTotal}') Where IDVenda = ('{this.IDVenda}')";
                        break;
                }
                Console.WriteLine("\nAlterações de Venda feita com sucesso!!!! Aperte ENTER para retornar ao Menu.");
                Console.ReadKey();                
                db.EditarDado(sql);

            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a operação editar! Aperte ENTER para retornar ao Menu.");
                Console.ReadKey();

            }

        }
        #endregion

        #region Verifica ID Passagem
        private bool VerificaIDPassagem()
        {
            do
            {
                Console.Write("\nDigite o ID da passagem: ");
                IDPassagem = Console.ReadLine();
                if (db.VerifExistente(IDPassagem, "IDPassagem", "Passagem") == false)
                {
                    Console.WriteLine("O ID da passagem não existe no cadastro passagens. Informe um ID válido");
                }
            } while (db.VerifExistente(IDPassagem, "IDPassagem", "Passagem") == false);
            Console.WriteLine($"\nID da passagem encontrado: {this.IDPassagem} continue sua compra");
            return true;
        }
        #endregion

    }
}
