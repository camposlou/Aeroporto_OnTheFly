using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aeroporto_OnTheFly
{
    internal class Voo
    {
        public string IDVoo { get; set; }
        public string Iatas { get; set; }
        public string Inscricao { get; set; }
        public DateTime DataHoraVoo { get; set; }
        public int AssentosOcup { get; set; }
        public char Situacao { get; set; }

        public InternalControlDB banco;      

        public Voo() { }

        public Voo(string idVoo, string iatas, string inscricao, DateTime dataHoraVoo, int assentosocup, char situacao)
        {
            this.IDVoo = idVoo;
            this.Iatas = iatas;
            this.Inscricao = inscricao;
            this.DataHoraVoo = dataHoraVoo;
            this.AssentosOcup = assentosocup; 
            this.Situacao = situacao; 
        }
        #region Insert Voo
        public void CadastroVoo()
        {
            InternalControlDB db = new InternalControlDB();

            Console.WriteLine("\n>>>DIGITE AS INFORMAÇÕES DA VOO ABAIXO<<<:\n ");

            Console.WriteLine("Número do ID do Voo: ");
            IDVoo = Console.ReadLine();
            bool verifica = db.VerifExistente(IDVoo, "IDVoo", "Voo");
            if (verifica)
            {
                Console.WriteLine("Voo Já cadastrado!!!");
                Console.ReadLine();
                IDVoo = "";
            }            

            Console.WriteLine("\n\t>>> Escolha a Aeronave(s) Abaixo: <<<");                    
            String sql = $"SELECT Inscricao, CNPJ, Capacidade, Situacao, Data_Cadastro, Data_UltimaVenda From Aeronave WHERE Situacao = 'A'; ";
            db.LocalizarDadoAeronave(sql);
            Console.WriteLine("Inscrição da Aeronave selecionada: ");
            Inscricao = Console.ReadLine();

           Console.WriteLine("\n\t>>> Escolha o IATA/Destino Abaixo: <<<");
            db.AbrirConexao() ;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Iatas, Cidade From Destino";
            using(SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine();
                    Console.WriteLine($"Iata: {reader.GetString(0)}");
                    Console.WriteLine($"Cidade: {reader.GetString(1)}");

                }
            }
            Console.WriteLine("Digite Destino/IATA desejado : ");
            Iatas = Console.ReadLine();
            DataHoraVoo = DateTime.Now;
            AssentosOcup = 0;
            do
            {
                Console.Write("Situação do Voo [A] Ativo [C] Cancelado: ");
                Situacao = char.Parse(Console.ReadLine().ToUpper());
                if (Situacao == '0')
                    return;
                if (Situacao != 'C' && Situacao != 'A')
                {
                    Console.WriteLine("Digite um opção válida!!!");
                }
            } while (Situacao != 'C' && Situacao != 'A');

            #region Insert Voo
            Console.WriteLine("\nDeseja efetuar a gravação? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                sql = $"INSERT INTO Voo (IDVoo, Inscricao, Iatas, Data_HoraVoo, Situacao, Assentos_Ocupados) VALUES ('{this.IDVoo}' , " +
                     $"'{this.Inscricao}', '{this.Iatas}', '{this.DataHoraVoo}', '{this.Situacao}', '{this.AssentosOcup}');";
                
                db.InserirDado(sql);

                Console.WriteLine("\nGravação efetuada com sucesso! Aperte ENTER para retornar ao Menu.");
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("\nGravação não efetuada! Aperte ENTER para retornar ao Menu.");
                Console.ReadKey();

            }
            #endregion

        }
        #endregion

        #region Select Voo Especifico
        public void LocalizarAeronave()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Localizar Voo Especifico <<<");
            Console.Write("\nDigite o ID do Voo: ");
            this.Inscricao = Console.ReadLine();

            Console.WriteLine("\nDeseja Continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT IDVoo, Inscricao, Iatas, Data_HoraVoo, Situacao, Assentos_Ocupados From Voo WHERE IDVoo=('{this.IDVoo}');";
                banco = new InternalControlDB();
                banco.LocalizarDadoVoo(sql);

                Console.WriteLine("\nAperte ENTER para Retornar ao Menu.");
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a localização do voo! Aperte ENTER para retornar ao Menu.");
                Thread.Sleep(2000);

            }

        }
        #endregion

        #region Select Lista de Vôos
        public void ConsultarListaVôos()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Lista de Voo(s) <<<");
            Console.WriteLine("\nDeseja continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {

                String sql = $"SELECT IDVoo, Inscricao, Iatas, Data_HoraVoo, Situacao, Assentos_Ocupados From Voo WHERE IDVoo=('{this.IDVoo}');";
                banco = new InternalControlDB();
                banco.LocalizarDadoVoo(sql);

                Console.WriteLine("\nAperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a consulta dod vôos! Aperte ENTER para retornar ao Menu.");


            }

        }
        #endregion

        #region Update Voo
        public void EditarVoo()
        {
            int opc = 0;
            String sql = "";
            Console.Clear();
            Console.WriteLine("\n\t>>> Editar Dados do Voo <<<");
            Console.Write("\nDigite a ID do Voo: ");
            this.Inscricao = Console.ReadLine();

            sql = $"SELECT IDVoo, Inscricao, Iatas, Data_HoraVoo, Situacao, Assentos_Ocupados From Voo WHERE IDVoo=('{this.IDVoo}');";
            banco = new InternalControlDB();

            if (!string.IsNullOrEmpty(banco.LocalizarDadoVoo(sql)))
            {
                Console.WriteLine("\nDeseja Efetuar a Alteração? Digite 1- Sim / 2- Não: ");
                Console.Write("Digite: ");
                opc = int.Parse(Console.ReadLine());

                if (opc == 1)
                {
                    Console.WriteLine("\nSelecione a opção que deseja editar");
                    Console.WriteLine("1-Iata");
                    Console.WriteLine("2-Data/Hora Voo");
                    Console.WriteLine("3-Situação");
                    Console.WriteLine("4-Assentos Ocupados: ");
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
                            Console.Write("\nAlterar o Iata para: ");
                            this.Iatas = Console.ReadLine();
                            sql = $"Update Voo Set Iatas=('{this.Iatas}') Where IDVoo=('{this.IDVoo}');";
                            break;
                        case 2:
                            Console.Write("\nAlterar a Data/Hora para: ");
                            this.DataHoraVoo = DateTime.Parse(Console.ReadLine());
                            sql = $"Update Voo Set Data_HoraVoo=('{this.DataHoraVoo}') Where IDVoo=('{this.IDVoo}');";
                            break;
                        case 3:
                            Console.WriteLine("\nSituação Atual: ");
                            this.Situacao = char.Parse(Console.ReadLine());
                            sql = $"Update Voo Set Situacao=('{this.Situacao}') Where IDVoo=('{this.IDVoo}');";
                            break;
                        case 4:
                            Console.WriteLine("\nAssentos Ocupados : ");
                            this.AssentosOcup = int.Parse(Console.ReadLine());
                            sql = $"Update Voo Set Assentos_Ocupados=('{this.AssentosOcup}') Where IDVoo=('{this.IDVoo}');";
                            break;

                    }
                    Console.WriteLine("\nCadastro alterado com sucesso!!!! Aperte ENTER para retornar ao Menu.");
                    Console.ReadKey();
                    banco = new InternalControlDB();
                    banco.EditarDado(sql);
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

    }
}
