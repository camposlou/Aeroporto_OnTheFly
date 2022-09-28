using System;
using System.Data.SqlClient;

namespace Aeroporto_OnTheFly
{
    internal class Program
    {
        static void MenuPrincipal()
        {
            do
            {
                InternalControlDB cnx = new InternalControlDB();
                SqlConnection conexaosql = new SqlConnection(cnx.AbrirConexao());

                int opcMenu = 9;
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("*** >>> BEM VINDO AO NOSSO AEROPORTO ON THE FLY <<< ***");
                Console.WriteLine();
                Console.WriteLine("|_____________________________________________________|");
                Console.WriteLine("|>>>>>>>>>>>>>>>>>> MENU PASSAGEIRO <<<<<<<<<<<<<<<<<<|");
                Console.WriteLine("|                                                     |");
                Console.WriteLine("|  0 - Encerrar:                                      |");
                Console.WriteLine("|  1 - Cadastrar Passageiro:                          |");
                Console.WriteLine("|  2 - Selecionar Passageiro Específico:              |");
                Console.WriteLine("|  3 - Exibir Lista de Passageiro:                    |");
                Console.WriteLine("|  4 - Alterar dados de Passageiros:                  |");
                Console.WriteLine("|_____________________________________________________|");
                Console.WriteLine("|>>>>>>>>>>>>>>> MENU COMPANHIA AÉREA <<<<<<<<<<<<<<<<|");
                Console.WriteLine("|                                                     |");
                Console.WriteLine("|  5 - Cadastrar Companhia Aérea:                     |");
                Console.WriteLine("|  6 - Selecionar Companhia Aérea Específica:         |");
                Console.WriteLine("|  7 - Exibir Lista de Companhia Aérea:               |");
                Console.WriteLine("|  8 - Alterar dados de Companhia Aérea:              |");
                Console.WriteLine("|_____________________________________________________|");
                Console.WriteLine("|>>>>>>>>>>>>>>>>>>> MENU AERONAVE <<<<<<<<<<<<<<<<<<<|");
                Console.WriteLine("|                                                     |");
                Console.WriteLine("|  9 - Cadastrar Aeronave:                            |");
                Console.WriteLine("|  10- Selecionar Aeronave Específica:                |");
                Console.WriteLine("|  11- Exibir Lista de Aeronaves:                     |");
                Console.WriteLine("|  12- Alterar dados de Aeronave:                     |");
                Console.WriteLine("|_____________________________________________________|");
                Console.WriteLine("|                                                     |");
                Console.WriteLine("|                                                     |");
                Console.Write("Opção: ");
               
              
               
                try
                {
                    opcMenu = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {

                }

                switch (opcMenu)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        Passageiro cdtpassageiro = new Passageiro ();
                        cdtpassageiro.CadastroPassageiro();
                        break;

                    case 2:
                        Passageiro selpassageiroesp = new Passageiro();
                        selpassageiroesp.LocalizarPassageiro();
                        break;

                    case 3:
                        Passageiro exibpassageiro = new Passageiro();
                        exibpassageiro.ConsultarListaPassageiro();
                        break;

                    case 4:
                        Passageiro altpassageiro = new Passageiro();
                        altpassageiro.EditarPassageiro();
                        break;

                    case 5:
                        CompanhiaAerea cdtcompanhia = new CompanhiaAerea();
                        cdtcompanhia.CadastroCompanhiaAerea();
                        break;
                    case 6:
                       
                                             
                        break;
                    case 7:
                       

                        break;
                    case 8:
                        
                        break;

                }

            } while (true);

        }

        static void Main(string[] args)
        {
            MenuPrincipal();
        }
    }
}
