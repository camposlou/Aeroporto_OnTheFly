using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
       
        InternalControlDB db = new ();
        public Passagem() { }

        public Passagem(string idpassagem, string idVoo, float valor, char situacao)
        {
            this.IDPassagem = idpassagem;
            this.IDVoo = idVoo;
            this.DataUltOperacao = DateTime.Now;
            this.Valor = valor;
            this.Situacao = situacao;
        }
        #region Insert Passagem
        public void CadastroPassagem()
        {            

            Console.WriteLine("\n>>>CADASTRO DE PASSAGENS <<<:\n");

            Random random = new Random();
            IDPassagem = "PA" + random.Next(0001, 9999).ToString("0000");

            Console.WriteLine($"O ID da sua Passagem {this.IDPassagem}\n");

            Console.WriteLine("\n\t>>> Escolha o Voo Abaixo: <<<");
            String sql = $"SELECT IDVoo, Inscricao_Aero, Iatas, Data_HoraVoo, Situacao, Assentos_Ocupados From Voo WHERE Situacao = 'A'; ";
            db.LocalizarDadoVoo(sql);
            Console.WriteLine("Número do ID do Voo Selecionado: ");
            IDVoo = Console.ReadLine();

            DataUltOperacao = DateTime.Now;

            Valor = 50;

            do
            {
                Console.Write("Situação da Passagem [L] Livre [R] Reservada [P] Paga: ");
                Situacao = char.Parse(Console.ReadLine().ToUpper());
                if (Situacao == '0')
                    return;
                if (Situacao != 'R' && Situacao != 'P' && Situacao != 'L')
                {
                    Console.WriteLine("Digite um opção válida!!!");
                }
            } while (Situacao != 'R' && Situacao != 'P' && Situacao != 'L');
                        
            Console.WriteLine("\nDeseja efetuar a gravação? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                sql = $"INSERT INTO Passagem (IDPassagem, IDVoo, Valor, Situacao, Data_UltimaOperacao) VALUES ('{this.IDPassagem}' , " +
                     $"'{this.IDVoo}', '{this.Valor}', '{this.Situacao}', '{this.DataUltOperacao}');";

                db.InserirDado(sql);

                Console.WriteLine("\nGravação efetuada com sucesso! Aperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nGravação não efetuada! Aperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }
            
        }
        #endregion

        #region Select Passagem Especifica
        public void LocalizarPassagem()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Localizar Passagem Especifica <<<");
            Console.Write("\nDigite o ID da Passagem: ");
            this.IDPassagem = Console.ReadLine();
                      

            Console.WriteLine("\nDeseja Continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT IDPassagem, IDVoo, Valor, Situacao, Data_UltimaOperacao From Passagem WHERE IDPassagem=('{this.IDPassagem}');";
               
                db.LocalizarDadoPassagem(sql);

                Console.WriteLine("\nAperte ENTER para Retornar ao Menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a localização da passagem! Aperte ENTER para retornar ao Menu.");
                Thread.Sleep(2000);
            }
        }
        #endregion

        #region Select Lista de Passagens
        public void ConsultarListaPassagem()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Lista de Passagem(s) <<<");
            Console.WriteLine("\nDeseja continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {

                String sql = $"SELECT IDPassagem, IDVoo, Valor, Situacao, Data_UltimaOperacao From Passagem WHERE Situacao = 'L'";
                
                db.LocalizarDadoPassagem(sql);

                Console.WriteLine("\nAperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a consulta da(s) passagem(s)! Aperte ENTER para retornar ao Menu.");

            }
        }
        #endregion

        #region Update Passagem
        public void EditarPassagem()
        {
            int opc = 0;
            String sql = "";
            Console.Clear();
            Console.WriteLine("\n\t>>> Editar Dados da Passagem <<<");
            Console.Write("\nDigite a ID da Passagem: ");
            this.IDPassagem = Console.ReadLine();

            sql = $"SELECT IDPassagem, IDVoo, Valor, Situacao, Data_UltimaOperacao From Passagem WHERE IDPassagem=('{this.IDPassagem}');";
            db = new InternalControlDB();

            if (!string.IsNullOrEmpty(db.LocalizarDadoPassagem(sql)))
            {
                Console.WriteLine("\nDeseja Efetuar a Alteração? Digite 1- Sim / 2- Não: ");
                Console.Write("Digite: ");
                opc = int.Parse(Console.ReadLine());

                if (opc == 1)
                {
                    Console.WriteLine("\nSelecione a opção que deseja editar");
                    Console.WriteLine("1-IDVoo");
                    Console.WriteLine("2-Valor");
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
                            Console.Write("\nAlterar o IDVoo para: ");
                            this.IDVoo = Console.ReadLine();
                            sql = $"Update Passagem Set IDVoo=('{this.IDVoo}') Where IDPassagem=('{this.IDPassagem}');";
                            break;
                        case 2:
                            Console.Write("\nAlterar a Valor para: ");
                            this.Valor = float.Parse(Console.ReadLine());
                            sql = $"Update Passagem Set Valor =('{this.Valor}') Where IDPassagem=('{this.IDPassagem}');";
                            break;
                    }
                    Console.WriteLine("\nCadastro de Passagem alterado com sucesso!!!! Aperte ENTER para retornar ao Menu.");
                    Console.ReadKey();                   
                    db.EditarDado(sql);
                }
                else
                {
                    Console.WriteLine("\nNÃO foi possível acionar a operação editar cadastro! Aperte ENTER para retornar ao Menu.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("\nVoo Não Encontrado! Aperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }
        }
        #endregion

        public void EditarSituacaoPassagem()
        {
            int opc = 0;
            String sql = "";
            Console.Clear();
            Console.Write("\nDigite a ID da Passagem: ");
            this.IDPassagem = Console.ReadLine();

            sql = $"SELECT IDPassagem, IDVoo, Valor, Situacao, Data_UltimaOperacao From Passagem WHERE Situacao = 'L';";
            db = new InternalControlDB();

            if (!string.IsNullOrEmpty(db.LocalizarDadoPassagem(sql)))
            {
                Console.WriteLine("\nSelecione a opção que deseja editar");
                Console.WriteLine("1-Livre para Reservada");
                Console.WriteLine("2-Reservada para Paga");
                Console.WriteLine("3-Reservada para Livre");
                Console.WriteLine("4-Livre para Paga");
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
                        this.Situacao = char.Parse(Console.ReadLine());
                        sql = $"Update Passagem Set Situacao = ('{this.Situacao}') Where Situacao= 'L';";
                        break;
                    case 2:
                        Console.Write("\nAlterar para: ");
                        this.Situacao = char.Parse(Console.ReadLine());
                        sql = $"Update Passagem Set Situacao = ('{this.Situacao}') Where Situacao= 'R';";
                        break;
                    case 3:
                        Console.Write("\nAlterar para: ");
                        this.Situacao = char.Parse(Console.ReadLine());
                        sql = $"Update Passagem Set Situacao = ('{this.Situacao}') Where Situacao= 'R';";
                        break;
                    case 4:
                        Console.Write("\nAlterar para: ");
                        this.Situacao = char.Parse(Console.ReadLine());
                        sql = $"Update Passagem Set Situacao = ('{this.Situacao}') Where Situacao= 'L';";
                        break;
                }
                Console.WriteLine("\nSituação de Passagem alterada com sucesso!!!! ");
                Console.ReadKey();                
                db.EditarDado(sql);                              

            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a operação editar situação! Aperte ENTER para retornar ao Menu.");
                Console.ReadKey();

            }
            Console.WriteLine("Deseja  atualizar a quantidade de assentos ocupados do Voo? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                Voo editass = new Voo();
                editass.EditarVoo();
            }
            else
            {
                Console.WriteLine("\n Aperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }

        }

    }
}