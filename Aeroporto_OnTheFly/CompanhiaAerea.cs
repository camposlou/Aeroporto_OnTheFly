using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aeroporto_OnTheFly
{
    internal class CompanhiaAerea
    {
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataUltVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }

        public InternalControlDB banco;

        public CompanhiaAerea() { }

        public CompanhiaAerea(string cnpj, string razaoSocial, DateTime DataAbertura, DateTime DataUltvoo, DateTime DataCadastro, char Situacao)
        {
            this.CNPJ = cnpj;
            this.RazaoSocial = razaoSocial;
            this.DataAbertura = DataAbertura;
            this.DataUltVoo = System.DateTime.Now;
            this.DataCadastro = System.DateTime.Now;
            this.Situacao = Situacao;
        }
        #region Insert Companhia Aerea
        public void CadastroCompanhiaAerea()
        {
            InternalControlDB db = new InternalControlDB();
            Console.Clear();

            Console.WriteLine("\n\t>>>DIGITE AS INFORMAÇÕES DA COMPANHIA AÉREA ABAIXO<<<:\n ");

            do
            {
                Console.WriteLine("CNPJ: ");
                CNPJ = Console.ReadLine();
                if (CNPJ == "0")
                    return;
                if (!ValidarCNPJ(CNPJ))
                {
                    Console.WriteLine("Digite um CNPJ Válido!");
                    Thread.Sleep(2000);
                }

                bool verifica = db.VerifExistente(CNPJ, "CNPJ", "Companhia_Aerea");
                if (verifica)
                {
                    Console.WriteLine("CNPJ Já cadastrado!!!");
                    Console.ReadLine();
                    CNPJ = "";
                }

            } while (!ValidarCNPJ(CNPJ));

            do
            {
                Console.WriteLine("Informe a Razão Social: ");
                RazaoSocial = Console.ReadLine();
                if (RazaoSocial.Length == 0)
                {
                    Console.WriteLine("Razão Social obrigatória!");
                }
                if (RazaoSocial.Length > 50)
                {
                    Console.WriteLine("Informe uma Razão Social com menos de 50 caracteres!");
                }

            } while (RazaoSocial.Length > 50 || RazaoSocial.Length == 0);

            bool validacao;
            do
            {

                Console.Write("Data de Abertura do CNPJ: ");
                try
                {
                    DataAbertura = DateTime.Parse(Console.ReadLine());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Formato de data inválido [dd/mm/aaaa]");
                    validacao = true;
                }
                if (DataAbertura > DateTime.Now)
                {
                    Console.WriteLine("Data de abertura não pode ser futura!");
                    validacao = true;
                }
                if (DataAbertura > DateTime.Now.AddMonths(-6))
                {
                    Console.WriteLine("Não é possível cadastrar empresas com menos de 6 meses!!!");
                    Thread.Sleep(2000);
                   validacao = true;
                }
            } while (validacao);

            DataUltVoo = DateTime.Now;
            DataCadastro = DateTime.Now;
            do
            {
                Console.Write("Situação [A] Ativo [I] Inativo: ");
                Situacao = char.Parse(Console.ReadLine());
                if (Situacao == '0')
                    return;
                if (Situacao != 'I' && Situacao != 'A')
                {
                    Console.WriteLine("Digite um opção válida!!!");
                }
            } while (Situacao != 'I' && Situacao != 'A');

            #region Insert Companhia Aerea
            Console.WriteLine("\nDeseja efetuar a gravação? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                string sql = $"INSERT INTO Companhia_Aerea  (CNPJ, Razao_Social, Data_Abertura, Situacao, Data_Cadastro, Data_UltimoVoo) VALUES ('{this.CNPJ}' , " +
                     $"'{this.RazaoSocial}', '{this.DataAbertura}', '{this.Situacao}', '{this.DataCadastro}', '{this.DataUltVoo.ToString("dd/MM/yyyy HH:mm")}');";
               
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

        #region Select Companhia Aerea Especifica
        public void LocalizarCompanhiaAerea()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Localizar Companhia Aerea Especifica <<<");
            Console.Write("\nDigite o CNPJ: ");
            this.CNPJ = Console.ReadLine();

            while (ValidarCNPJ(this.CNPJ) == false || this.CNPJ.Length < 11)
            {
                Console.WriteLine("\nCNPJ invalido, digite novamente");
                Console.Write("CNPJ: ");
                this.CNPJ = Console.ReadLine();
            }

            Console.WriteLine("\nDeseja Continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT CNPJ, Razao_Social, Data_Abertura, Situacao, Data_Cadastro, Data_UltimoVoo From Companhia_Aerea WHERE CNPJ=('{this.CNPJ}');";
                banco = new InternalControlDB();
                banco.LocalizarDadoCompanhia(sql);

                Console.WriteLine("\nAperte ENTER para Retornar ao Menu.");
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a localização da companhia aerea! Aperte ENTER para retornar ao Menu.");
                Thread.Sleep(2000);

            }

        }
        #endregion

        #region Select Lista de Companhias Aerea
        public void ConsultarListaCompanhia()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Lista de Companhia(s) <<<");
            Console.WriteLine("\nDeseja continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {

                String sql = $"SELECT CNPJ, Razao_Social, Data_Abertura, Situacao, Data_Cadastro, Data_UltimoVoo From Companhia_Aerea";
                banco = new InternalControlDB();
                banco = new InternalControlDB();
                banco.LocalizarDadoCompanhia(sql);

                Console.WriteLine("\nAperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a consulta das companhias! Aperte ENTER para retornar ao Menu.");


            }

        }
        #endregion

        #region Update CompanhiaAerea
        public void EditarCompanhia()
        {
            int opc = 0;
            String sql = "";
            Console.Clear();
            Console.WriteLine("\n\t>>> Editar Dados da Companhia <<<");
            Console.Write("\nDigite o CNPJ: ");
            this.CNPJ = Console.ReadLine();

            while (ValidarCNPJ(this.CNPJ) == false || this.CNPJ.Length < 11)
            {
                Console.WriteLine("\nCNPJ inválido, digite novamente");
                Console.Write("CNPJ: ");
                this.CNPJ = Console.ReadLine();
            }

            sql = $"SELECT CNPJ, Razao_Social, Data_Abertura, Situacao, Data_Cadastro, Data_UltimoVoo From Companhia_Aerea WHERE CNPJ=('{this.CNPJ}');";
            banco = new InternalControlDB();

            if (!string.IsNullOrEmpty(banco.LocalizarDadoCompanhia(sql)))
            {
                Console.WriteLine("\nDeseja Efetuar a Alteração? Digite 1- Sim / 2- Não: ");
                Console.Write("Digite: ");
                opc = int.Parse(Console.ReadLine());

                if (opc == 1)
                {
                    Console.WriteLine("\nSelecione a opção que deseja editar");
                    Console.WriteLine("1-Razão Social");
                    Console.WriteLine("2-Data de Abertura");
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
                            Console.Write("\nAlterar a Razão Social para: ");
                            this.RazaoSocial = Console.ReadLine();
                            sql = $"Update Comapnhia_Aerea Set Razao_Social=('{this.RazaoSocial}') Where CNPJ=('{this.CNPJ}');";
                            break;
                        case 2:
                            Console.Write("\nAlterar a Data de Abertura para: ");
                            this.DataAbertura = DateTime.Parse(Console.ReadLine());
                            sql = $"Update Companhia_Aerea Set Data_Abertura=('{this.DataAbertura}') Where CNPJ=('{this.CNPJ}');";
                            break;
                        case 3:
                            Console.WriteLine("\nSituação Atual: ");
                            this.Situacao = char.Parse(Console.ReadLine());
                            sql = $"Update Companhia_Aerea Set Situacao=('{this.Situacao}') Where CNPJ=('{this.CNPJ}');";
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
                Console.WriteLine("\ncompanhia Não Encontrado! Aperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }
        }
        #endregion




        public bool ValidarCNPJ(string vrCNPJ)
        {
            string CNPJ = vrCNPJ.Replace(".", "");
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");
            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;
            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;
            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                        CNPJ.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }
                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (
                         resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == 0);
                    else
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));
                }
                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch
            {
                return false;
            }

        }

    }
}
