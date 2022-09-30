using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeroporto_OnTheFly
{
    internal class InternalControlDB
    {
        private static string Conexao = "Data Source=localhost; Initial Catalog=Aeroporto_OnTheFly; User Id=sa; Password=MiliBe1@;";
        private static SqlConnection Conecta = new SqlConnection(Conexao);


        public InternalControlDB() { }

        public string AbrirConexao()
        {
            return Conexao;
        }

        #region Insert, Select, Update
        public void InserirDado(String sql)
        {
            try
            {
                Conecta.Open();
                SqlCommand cmd = new SqlCommand(sql, Conecta);
                cmd.Connection = Conecta;
                cmd.ExecuteNonQuery();
                Conecta.Close();

            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        public String LocalizarDadoPassageiro(String sql)
        {
            String recebe = "";
            try
            {
                Conecta.Open();

                SqlCommand cmd = new SqlCommand(sql, Conecta);
                cmd.Connection = Conecta;
                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\n\t>>> Passageiro(s) Localizado(s) <<<\n");
                    while (reader.Read())
                    {
                        recebe = reader.GetString(0);
                        Console.Write($"CPF: {reader.GetString(0)}\n");
                        Console.Write($"Nome: {reader.GetString(1)}\n");
                        Console.Write($"Sexo: {reader.GetString(2)}\n");
                        Console.Write($"Data de Nascimento: {reader.GetDateTime(3).ToShortDateString()}\n");
                        Console.Write($"Situação: {reader.GetString(4)}\n");
                        Console.Write($"Data da Última Compra: {reader.GetDateTime(5).ToShortDateString()}\n");
                        Console.Write($"Data de Cadastro: {reader.GetDateTime(6).ToShortDateString()}\n");

                        Console.WriteLine("\n");
                    }
                }
                Conecta.Close();
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }
            return recebe;
        }
        public String LocalizarDadoCompanhia(String sql)
        {
            String recebe = "";
            try
            {
                Conecta.Open();

                SqlCommand cmd = new SqlCommand(sql, Conecta);
                cmd.Connection = Conecta;
                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("Aqui foi");
                    Console.WriteLine("\n\t>>> Companhia Aerea(s) Localizada(s) <<<\n");
                    while (reader.Read())
                    {
                        recebe = reader.GetString(0);
                        Console.Write($"CNPJ: {reader.GetString(0)}\n");
                        Console.Write($"Razão Social: {reader.GetString(1)}\n");
                        Console.Write($"Data de Abertura: {reader.GetDateTime(2).ToShortDateString()}\n");
                        Console.Write($"Situação: {reader.GetString(3)}\n");
                        Console.Write($"Data Cadastro: {reader.GetDateTime(5).ToShortDateString()}\n");
                        Console.Write($"Data do Último Voo: {reader.GetDateTime(5).ToShortDateString()}\n");

                        Console.WriteLine("\n");
                    }
                }
                Conecta.Close();

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return recebe;
        }
        public String LocalizarDadoAeronave(String sql)
        {
            String recebe = "";
            try
            {
                Conecta.Open();

                SqlCommand cmd = new SqlCommand(sql, Conecta);
                cmd.Connection = Conecta;
                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\n\t>>> Aeronave(s) Localizada(s) <<<\n");
                    while (reader.Read())
                    {
                        recebe = reader.GetString(0);
                        Console.Write($"Inscrição: {reader.GetString(0)}\n");
                        Console.Write($"CNPJ: {reader.GetString(1)}\n");
                        Console.Write($"Capacidade: {reader.GetInt32(2)}\n");
                        Console.Write($"Situação: {reader.GetString(3)}\n");
                        Console.Write($"Data Cadastro: {reader.GetDateTime(4).ToShortDateString()}\n");
                        Console.Write($"Data da Última Venda: {reader.GetDateTime(5).ToShortDateString()}\n");

                        Console.WriteLine("\n");
                    }
                }
                Conecta.Close();
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }
            return recebe;

        }
        public String LocalizarDadoVoo(String sql)
        {
            String recebe = "";
            try
            {
                Conecta.Open();

                SqlCommand cmd = new SqlCommand(sql, Conecta);
                cmd.Connection = Conecta;
                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\n\t>>> Voo(s) Localizada(s) <<<\n");
                    while (reader.Read())
                    {
                        recebe = reader.GetString(0);
                        Console.Write($"IDVoo: {reader.GetString(0)}\n");
                        Console.Write($"Inscrição: {reader.GetString(1)}\n");
                        Console.Write($"Iata: {reader.GetDateTime(2).ToShortDateString()}\n");
                        Console.Write($"Data Cadastro: {reader.GetDateTime(3).ToShortDateString()}\n");
                        Console.Write($"Situação: {reader.GetString(4)}\n");
                        Console.Write($"Assentos Ocupados: {reader.GetInt32(5)}\n");

                        Console.WriteLine("\n");
                    }
                }
                Conecta.Close();

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return recebe;
        }     

        public String LocalizarIATAS(String sql)
        {
            String recebe = "";
            try
            {
                Conecta.Open();
                SqlCommand cmd = new SqlCommand(sql, Conecta);
                cmd.Connection = Conecta;
                SqlDataReader reader = null;


                using (reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\n\t>>> Iata(s) Localizada(s) <<<\n");
                    while (reader.Read())
                    {
                        recebe = reader.GetString(0);
                        Console.Write($"Iata: {reader.GetString(0)}");
                        Console.WriteLine($" Destino: {reader.GetString(1)}");
                    }
                }
                Conecta.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return recebe;
        }
        public void EditarDado(String sql)
        {
            try
            {
                Conecta.Open();

                SqlCommand cmd = new SqlCommand(sql, Conecta);
                cmd.Connection = Conecta;
                cmd.ExecuteNonQuery();

                Conecta.Close();
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        public bool VerifExistente(string dado, string campo, string tabela)
        {
            string queryString = $"SELECT {campo} FROM {tabela} WHERE {campo} = '{dado}'";
            try
            {
                SqlCommand command = new SqlCommand(queryString);
                command.Connection = Conecta;
                Conecta.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Conecta.Close();
                        return true;
                    }
                    else
                    {
                        Conecta.Close();
                        return false;
                    }

                }
            }
            catch (Exception e)
            {
                Conecta.Close();
                Console.WriteLine($"Erro ao acessar o Banco de Dados!!!\n{e.Message}");
                Console.WriteLine("Tecle Enter para continuar....");
                Console.ReadKey();
                return false;
            }

        }
        public string TratamentoDado(string dado)
        {
            string dadoTratado = dado.Replace(".", "").Replace("-", "").Replace("'", "").Replace("]", "").Replace("[", "");
            return dadoTratado;
        }


    }
}

