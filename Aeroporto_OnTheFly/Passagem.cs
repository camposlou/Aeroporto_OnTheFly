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

        public InternalControlDB banco;
        public Passagem( string idVoo )
        {
            Random random = new Random();
            this.IDPassagem = "PA" + random.Next(0001, 9999).ToString("0000");
            
            this.IDVoo = idVoo;
            this.DataUltOperacao = DateTime.Now;
            this.Valor = 50;
            this.Situacao = 'L' ;
        }
        public void CadastroPassagem()
        {
            InternalControlDB db = new InternalControlDB();

            //Console.WriteLine("\n>>>DIGITE AS INFORMAÇÕES DA PASSAGEM ABAIXO<<<:\n");
            while (true)
            {
                Random random = new Random();
                IDVoo = "PA" + random.Next(0001, 9999).ToString("0000");

                if (db.VerifExistente(IDPassagem, "IDPassagem", "Passagem"))
                {
                    Console.WriteLine("Passagem Já cadastrada!!!");
                    Thread.Sleep(2000);
                }
                else
                {
                    break;
                }
            }            
            Console.WriteLine("Número do ID do Voo: ");//???????
            IDVoo = Console.ReadLine();
            DataUltOperacao = DateTime.Now;            
            Valor = 50;

            /*do
            {
                Console.Write("Situação [L] Livre [R] Reservada [P] Paga: ");
                Situacao = char.Parse(Console.ReadLine().ToUpper());
                if (Situacao == '0')
                    return;
                if (Situacao != 'R' && Situacao != 'P' && Situacao != 'L')
                {
                    Console.WriteLine("Digite um opção válida!!!");
                }
            } while (Situacao != 'R' && Situacao != 'P' && Situacao != 'L');*/


        }
        #region Select Passagem Especifico
        public void LocalizarPassagem()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Localizar Passagem Especifica <<<");
            Console.Write("\nDigite o ID da Passagem: ");//???????????????
            this.IDVoo = Console.ReadLine();//????????????????????????

            Console.WriteLine("\nDeseja Continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT IDVoo, Inscricao, Iatas, Data_HoraVoo, Situacao, Assentos_Ocupados From Voo WHERE IDVoo=('{this.IDVoo}');";
                banco = new InternalControlDB();
                banco.LocalizarDadoPassagem(sql);

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

        #region Select Lista de Passagens
        public void ConsultarListaPassagem()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Lista de Voo(s) <<<");
            Console.WriteLine("\nDeseja continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {

                String sql = $"SELECT IDPassagem, IDVoo, Data_UltOperacao, Situacao, Assentos_Ocupados From Voo WHERE Situacao = 'A';";
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

    }
}
