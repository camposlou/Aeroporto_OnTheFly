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

        InternalControlDB db = new InternalControlDB();       

        public Aeronave() { }

        public Aeronave(string inscricao, int capacidade, DateTime dataUltVenda, DateTime dataCadastro, char situacao, string cnpj)
        {
            this.Inscricao = inscricao;
            this.CNPJ = cnpj;
            this.Capacidade = capacidade;
            this.DataUltVenda = dataUltVenda;
            this.DataCadastro = dataCadastro;
            this.Situacao = situacao;

        }
        #region Insert Aeronave
        public void CadastroAeronave()
        {
            Console.Clear();
           

            Console.WriteLine("\n\t>>>DIGITE AS INFORMAÇÕES DA AERONAVE ABAIXO<<<:\n ");

            String sql = $"SELECT Inscricao From InscricaoAeronave";
            db.LocalizarInscricaoAeronave(sql);
            Console.WriteLine("\nDigite o Número de Inscrição da Aeronave seguindo o padrão definido pela ANAC (XX-XXX): ");
            Inscricao = db.TratamentoDado(Console.ReadLine().ToUpper().Trim().Replace("-", ""));
            while (true)
            {
                if (Inscricao == "0")
                    return;
                if (Inscricao.Length != 5)
                {
                    Console.WriteLine("Código de Inscrição Inválido!!!");
                    Console.ReadKey();
                }
                else
                {
                    if (db.VerifExistente(Inscricao, "Inscricao", "Aeronave"))
                    {
                        Console.WriteLine("Aeronave já cadastrada!!!");
                        Console.ReadKey();
                        Inscricao = "";
                    }
                    else
                    {
                        break;
                    }
                }
            }
            
            Console.WriteLine("\n\t>>> Escolha a Companhia Aerea Abaixo: <<<");
            sql = $"SELECT CNPJ, Razao_Social, Data_Abertura, Situacao, Data_Cadastro, Data_UltimoVoo From Companhia_Aerea WHERE Situacao = 'A'; ";
            db.LocalizarDadoCompanhia(sql);
            Console.Write("Digite o CNPJ da Companhia Aerea Selecionada: ");
            CNPJ = Console.ReadLine();
            while (true)
            {
                if (db.LocalizarBloqueados(CNPJ, "CNPJ", "CadastrosRestritos"))
                {
                    Console.WriteLine("CNPJ Bloqueado!!!");
                    Thread.Sleep(2000);
                }
                else
                {
                    Console.WriteLine("CNPJ Apto!!");
                    break;
                }
            }
            Console.WriteLine("Capacidade: ");
            Capacidade = int.Parse(Console.ReadLine());
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

            DataUltVenda = DateTime.Now;
            DataCadastro = DateTime.Now;

            #region Insert Aeronave
            Console.WriteLine("\nDeseja efetuar a gravação? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                sql = $"INSERT INTO Aeronave (Inscricao, CNPJ, Capacidade, Situacao, Data_Cadastro, Data_UltimaVenda) VALUES ('{this.Inscricao}' , " +
                     $"'{this.CNPJ}', '{this.Capacidade}', '{this.Situacao}', '{this.DataCadastro}', '{this.DataUltVenda}');";
                db = new InternalControlDB();
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

        #region Select Aeronave Especifica
        public void LocalizarAeronave()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Localizar Aeronave Especifica <<<");
            Console.Write("\nDigite o Inscrição: ");
            this.Inscricao = Console.ReadLine();

            Console.WriteLine("\nDeseja Continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT Inscricao, CNPJ, Capacidade, Situacao, Data_Cadastro, Data_UltimaVenda From Aeronave WHERE Inscricao=('{this.Inscricao}');";
                
                db.LocalizarDadoAeronave(sql);

                Console.WriteLine("\nAperte ENTER para Retornar ao Menu.");
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a localização da aeronave! Aperte ENTER para retornar ao Menu.");
                Thread.Sleep(2000);

            }

        }
        #endregion

        #region Select Lista de Aeronaves
        public void ConsultarListaAeronave()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Lista de Aeronave(s) <<<");
            Console.WriteLine("\nDeseja continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {

                String sql = $"SELECT Inscricao, CNPJ, Capacidade, Situacao, Data_Cadastro, Data_UltimaVenda From Aeronave WHERE Situacao = 'A';";
                
                db.LocalizarDadoAeronave(sql);

                Console.WriteLine("\nAperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a consulta das aeronaves! Aperte ENTER para retornar ao Menu.");


            }

        }
        #endregion

        #region Update Aeronave
        public void EditarAeronave()
        {
            int opc = 0;
            String sql = "";
            Console.Clear();
            Console.WriteLine("\n\t>>> Editar Dados da Aeronave <<<");
            Console.Write("\nDigite a Inscrição: ");
            this.Inscricao = Console.ReadLine();

            sql = $"SELECT Inscricao, CNPJ, Capacidade, Situacao, Data_Cadastro, Data_UltimaVenda From Aeronave WHERE Inscricao=('{this.Inscricao}');";
            db = new InternalControlDB();

            if (!string.IsNullOrEmpty(db.LocalizarDadoAeronave(sql)))
            {
                Console.WriteLine("\nDeseja Efetuar a Alteração? Digite 1- Sim / 2- Não: ");
                Console.Write("Digite: ");
                opc = int.Parse(Console.ReadLine());

                if (opc == 1)
                {
                    Console.WriteLine("\nSelecione a opção que deseja editar");
                    Console.WriteLine("1-CNPJ");
                    Console.WriteLine("2-Capacidade");
                    Console.WriteLine("3-Situação");
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
                            Console.Write("\nAlterar o CNPJ para: ");
                            this.CNPJ = Console.ReadLine();
                            sql = $"Update Aeronave Set CNPJ=('{this.CNPJ}') Where Inscricao=('{this.Inscricao}');";
                            break;
                        case 2:
                            Console.Write("\nAlterar a Capacidade para: ");
                            this.Capacidade = int.Parse(Console.ReadLine());
                            sql = $"Update Aeronave Set Capacidade=('{this.Capacidade}') Where Inscricao=('{this.Inscricao}');";
                            break;
                        case 3:
                            Console.WriteLine("\nSituação Atual: ");
                            this.Situacao = char.Parse(Console.ReadLine());
                            sql = $"Update Aeronave Set Situacao=('{this.Situacao}') Where Inscricao=('{this.Inscricao}');";
                            break;

                    }
                    Console.WriteLine("\nCadastro alterado com sucesso!!!! Aperte ENTER para retornar ao Menu.");
                    Console.ReadKey();
                    db = new InternalControlDB();
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
                Console.WriteLine("\nAeronave Não Encontrada! Aperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }
        }
        #endregion
    }
}
