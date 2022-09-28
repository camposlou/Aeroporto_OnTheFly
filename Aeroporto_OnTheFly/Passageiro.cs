using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Aeroporto_OnTheFly
{
    internal class Passageiro
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public char Sexo { get; set; }
        public DateTime DataNasc { get; set; }
        public char Situacao { get; set; }
        public DateTime DataUltCompra { get; set; }
        public DateTime DataCadastro { get; set; }

        public InternalControlDB banco;

        public Passageiro() { }



        public Passageiro(string cpf, string nome, char sexo, DateTime dataNascimento, char Situacao, DateTime dataUltCompra, DateTime dataCadastro)
        {
            this.CPF = cpf;
            this.Nome = nome;
            this.Sexo = sexo;
            this.DataNasc = dataNascimento;
            this.Situacao = Situacao;
            this.DataUltCompra = dataUltCompra;
            this.DataCadastro = dataCadastro;


        }
        #region Insert Passageiro
        public void CadastroPassageiro()
        {
            InternalControlDB db = new InternalControlDB();
            Console.Clear();
            Console.WriteLine("\n\t>>>DIGITE AS INFORMAÇÕES DO PASSAGEIRO ABAIXO<<<:\n ");

            do
            {
                Console.Write("CPF: ");
                this.CPF = Console.ReadLine();

                if (!ValidarCPF(CPF))
                {
                    Console.WriteLine("Digite um CPF Válido!");
                    Thread.Sleep(2000);
                }

                bool verifica = db.VerifExistente( CPF, "CPF", "Passageiro");
                if (verifica)
                {
                    Console.WriteLine("CPF Já cadastrado!!!");
                    Console.ReadKey();
                    CPF = "";
                }


            } while (!ValidarCPF(CPF));

            do
            {
                Console.Write("Informe o Nome Completo: ");
                Nome = Console.ReadLine();
                if (Nome.Length == 0)
                {
                    Console.WriteLine("Nome obrigatório!");
                }
                if (Nome.Length > 50)
                {
                    Console.WriteLine("Informe um nome com menos de 50 caracteres!");
                }

            } while (Nome.Length > 50 || Nome.Length == 0);

            do
            {
                Console.Write("Sexo [F] Feminino [M] Masculino [N] Prefere não informar: ");
                Sexo = char.Parse(Console.ReadLine());

                if (Sexo != 'M' && Sexo != 'N' && Sexo != 'F')
                {
                    Console.WriteLine("Digite um opção válida!!!");
                }
            } while (Sexo != 'M' && Sexo != 'N' && Sexo != 'F');

            bool validacao;
            do
            {

                Console.Write("Data de nascimento: ");
                try
                {
                    DataNasc = DateTime.Parse(Console.ReadLine());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Formato de data inválido [dd/mm/aaaa]");
                    validacao = true;
                }
                if (DataNasc > DateTime.Now)
                {
                    Console.WriteLine("Data de aniversário não pode ser futura!");
                    validacao = true;
                }
            } while (validacao);

            do
            {
                Console.Write("Situação [A] Ativo [I] Inativo: ");
                Situacao = char.Parse(Console.ReadLine());

                if (Situacao != 'I' && Situacao != 'A')
                {
                    Console.WriteLine("Digite um opção válida!!!");
                }
            } while (Situacao != 'I' && Situacao != 'A');

            DataUltCompra = DateTime.Now;
            DataCadastro = DateTime.Now;

            #region Insert Passageiro
            Console.WriteLine("\nDeseja efetuar a gravação? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                string sql = $"INSERT INTO Passageiro (CPF, Nome, Sexo, Data_Nascimento, Situacao, Data_UltimaCompra, Data_Cadastro) VALUES ('{this.CPF}' , " +
                     $"'{this.Nome}', '{this.Sexo}', '{this.DataNasc}', '{this.Situacao}', '{this.DataUltCompra}', '{this.DataCadastro}');";
                banco = new InternalControlDB();
                banco.InserirDado(sql);

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

        #region Select Passageiro Especifico
        public void LocalizarPassageiro()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Localizar Passageiro Especifico <<<");
            Console.Write("\nDigite o CPF: ");
            this.CPF = Console.ReadLine();

            while (ValidarCPF(this.CPF) == false || this.CPF.Length < 11)
            {
                Console.WriteLine("\nCPF invalido, digite novamente");
                Console.Write("CPF: ");
                this.CPF = Console.ReadLine();
            }

            Console.WriteLine("\nDeseja Continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT CPF, Nome, Sexo, Data_Nascimento, Situacao, Data_UltimaCompra, Data_Cadastro From Passageiro WHERE CPF=('{this.CPF}');";
                banco = new InternalControlDB();
                banco.LocalizarDadoPassageiro(sql);

                Console.WriteLine("\nAperte ENTER para Retornar ao Menu.");
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a localização do passageiro! Aperte ENTER para retornar ao Menu.");
                Thread.Sleep(2000);

            }

        }
        #endregion

        #region Select Lista de Passageiros
        public void ConsultarListaPassageiro()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Lista de Passageiro(s) <<<");
            Console.WriteLine("\nDeseja continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                
                String sql = $"SELECT CPF,Nome, Sexo, Data_Nascimento, Situacao, Data_UltimaCompra, Data_Cadastro  FROM Passageiro";
                banco = new InternalControlDB();
                banco = new InternalControlDB();
                banco.LocalizarDadoPassageiro(sql);

                Console.WriteLine("\nAperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a consulta dos passageiros! Aperte ENTER para retornar ao Menu.");
                

            }

        }
        #endregion

        #region Update Passageiro
        public void EditarPassageiro()
        {
            int opc = 0;
            String sql = "";
            Console.Clear();
            Console.WriteLine("\n\t>>> Editar Dados do Passageiro <<<");
            Console.Write("\nDigite o cpf: ");
            this.CPF = Console.ReadLine();

            while (ValidarCPF(this.CPF) == false || this.CPF.Length < 11)
            {
                Console.WriteLine("\nCpf invalido, digite novamente");
                Console.Write("CPF: ");
                this.CPF = Console.ReadLine();
            }

            sql = $"SELECT CPF,Nome, Sexo, Data_Nascimento, Situacao, Data_UltimaCompra, Data_Cadastro  FROM Passageiro WHERE CPF=('{this.CPF}');";
            banco = new InternalControlDB();
           
            if (!string.IsNullOrEmpty(banco.LocalizarDadoPassageiro(sql)))
            {
                Console.WriteLine("\nDeseja Efetuar a Alteração? Digite 1- Sim / 2- Não: ");
                Console.Write("Digite: ");
                opc = int.Parse(Console.ReadLine());

                if (opc == 1)
                {
                    Console.WriteLine("\nSelecione a opção que deseja editar");
                    Console.WriteLine("1-Nome");
                    Console.WriteLine("2-Data de Nascimento");
                    Console.WriteLine("3-Sexo (M/F/N)");
                    Console.WriteLine("4-Situação");
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
                            Console.Write("\nAlterar o Nome para: ");
                            this.Nome = Console.ReadLine();
                            sql = $"Update Passageiro Set Nome=('{this.Nome}') Where CPF=('{this.CPF}');";
                            break;
                        case 2:
                            Console.Write("\nAlterar a data de Nascimento para: ");
                            this.DataNasc = DateTime.Parse(Console.ReadLine());
                            sql = $"Update Passageiro Set Data_Nascimento=('{this.DataNasc}') Where CPF=('{this.CPF}');";
                            break;
                        case 3:
                            Console.Write("\nAlterar o Sexo para (M/F/N): ");
                            this.Sexo = char.Parse(Console.ReadLine());
                            sql = $"Update Passageiro Set Sexo=('{this.Sexo}') Where CPF=('{this.CPF}');";
                            break;
                        case 4:
                            Console.WriteLine("\nSituação Atual: ");
                            this.Situacao = char.Parse(Console.ReadLine());
                            sql = $"Update Passageiro Set Situacao=('{this.Situacao}') Where CPF=('{this.CPF}');";
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
                Console.WriteLine("\nPassageiro Não Encontrado! Aperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }
        }
        #endregion

        private static bool ValidarCPF(string vrCPF)
        {
            string valor = vrCPF.Replace(".", "");

            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)

                if (valor[i] != valor[0])

                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)

                numeros[i] = int.Parse(

                  valor[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++)

                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)

            {
                if (numeros[9] != 0)
                    return false;
            }

            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)

            {
                if (numeros[10] != 0)
                    return false;
            }

            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }
    }



   






}



