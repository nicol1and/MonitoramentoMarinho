using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MonitoramentoMarinho
{
    public class BancoDados
    {
        private readonly string _conexao =
            "Data Source=monitoramento.db";

        public BancoDados()
        {
            CriarTabela();
        }

        private void CriarTabela()
        {
            using var conexao = new SqliteConnection(_conexao);

            conexao.Open();

            string sqlCorais = @"
                CREATE TABLE IF NOT EXISTS Corais (
                    Id INTEGER PRIMARY KEY,
                    NomeCientifico TEXT,
                    NomePopular TEXT,
                    StatusSaude TEXT,
                    ProfundidadeMetros REAL,
                    DataObservacao TEXT
                );";

            using var comandoCorais =
                new SqliteCommand(sqlCorais, conexao);

            comandoCorais.ExecuteNonQuery();

            string sqlEspecies = @"
                CREATE TABLE IF NOT EXISTS EspeciesCadastradas (
                    Id INTEGER PRIMARY KEY,
                    NomeCientifico TEXT,
                    NomePopular TEXT,
                    Tipo TEXT,
                    DadoExtra TEXT,
                    DataObservacao TEXT
                );";

            using var comandoEspecies =
                new SqliteCommand(sqlEspecies, conexao);

            comandoEspecies.ExecuteNonQuery();
        }

        public void SalvarEspecieCadastrada(EspecieMarinha especie)
        {
            using var conexao = new SqliteConnection(_conexao);

            conexao.Open();

            string tipo = especie.GetType().Name;
            string dadoExtra = "";

            if (especie is CoralEspecie coral)
            {
                dadoExtra =
                    $"{coral.StatusSaude};{coral.ProfundidadeMetros.ToString(CultureInfo.InvariantCulture)}";
            }
            else if (especie is PeixeEspecie peixe)
            {
                dadoExtra =
                    peixe.TamanhoCm.ToString(CultureInfo.InvariantCulture);
            }
            else if (especie is TartarugaMarinha tartaruga)
            {
                dadoExtra = tartaruga.Situacao;
            }
            else if (especie is Golfinho golfinho)
            {
                dadoExtra = golfinho.QuantidadeObservada.ToString();
            }

            string sql = @"
                INSERT OR REPLACE INTO EspeciesCadastradas
                (Id, NomeCientifico, NomePopular, Tipo, DadoExtra, DataObservacao)
                VALUES
                (@id, @cientifico, @popular, @tipo, @extra, @data);";

            using var comando =
                new SqliteCommand(sql, conexao);

            comando.Parameters.AddWithValue("@id", especie.Id);
            comando.Parameters.AddWithValue("@cientifico", especie.NomeCientifico);
            comando.Parameters.AddWithValue("@popular", especie.NomePopular);
            comando.Parameters.AddWithValue("@tipo", tipo);
            comando.Parameters.AddWithValue("@extra", dadoExtra);
            comando.Parameters.AddWithValue(
                "@data",
                especie.DataObservacao.ToString("yyyy-MM-dd HH:mm:ss"));

            comando.ExecuteNonQuery();
        }

        public List<EspecieMarinha> CarregarEspeciesCadastradas()
        {
            var especies = new List<EspecieMarinha>();

            using var conexao = new SqliteConnection(_conexao);

            conexao.Open();

            string sql =
                "SELECT * FROM EspeciesCadastradas";

            using var comando =
                new SqliteCommand(sql, conexao);

            using var leitor =
                comando.ExecuteReader();

            while (leitor.Read())
            {
                int id = Convert.ToInt32(leitor["Id"]);
                string nomeCientifico =
                    leitor["NomeCientifico"].ToString() ?? "";
                string nomePopular =
                    leitor["NomePopular"].ToString() ?? "";
                string tipo =
                    leitor["Tipo"].ToString() ?? "";
                string dadoExtra =
                    leitor["DadoExtra"].ToString() ?? "";

                if (tipo == "TartarugaMarinha")
                {
                    especies.Add(
                        new TartarugaMarinha(
                            id,
                            nomeCientifico,
                            nomePopular,
                            dadoExtra));
                }
                else if (tipo == "Golfinho")
                {
                    int quantidade =
                        int.Parse(dadoExtra);

                    especies.Add(
                        new Golfinho(
                            id,
                            nomeCientifico,
                            nomePopular,
                            quantidade));
                }
                else if (tipo == "PeixeEspecie")
                {
                    double tamanho =
                        double.Parse(
                            dadoExtra,
                            CultureInfo.InvariantCulture);

                    especies.Add(
                        new PeixeEspecie(
                            id,
                            nomeCientifico,
                            nomePopular,
                            tamanho));
                }
                else if (tipo == "CoralEspecie")
                {
                    string[] partes =
                        dadoExtra.Split(';');

                    string status =
                        partes[0];

                    double profundidade =
                        double.Parse(
                            partes[1],
                            CultureInfo.InvariantCulture);

                    especies.Add(
                        new CoralEspecie(
                            id,
                            nomeCientifico,
                            nomePopular,
                            status,
                            profundidade));
                }
            }

            return especies;
        }

        public void SalvarCoral(CoralEspecie coral)
        {
            using var conexao = new SqliteConnection(_conexao);

            conexao.Open();

            string sql = @"
                INSERT OR REPLACE INTO Corais
                (Id, NomeCientifico, NomePopular,
                 StatusSaude, ProfundidadeMetros, DataObservacao)
                VALUES
                (@id, @cientifico, @popular,
                 @status, @profundidade, @data);";

            using var comando =
                new SqliteCommand(sql, conexao);

            comando.Parameters.AddWithValue("@id", coral.Id);
            comando.Parameters.AddWithValue("@cientifico", coral.NomeCientifico);
            comando.Parameters.AddWithValue("@popular", coral.NomePopular);
            comando.Parameters.AddWithValue("@status", coral.StatusSaude);
            comando.Parameters.AddWithValue("@profundidade", coral.ProfundidadeMetros);
            comando.Parameters.AddWithValue(
                "@data",
                coral.DataObservacao.ToString("yyyy-MM-dd HH:mm:ss"));

            comando.ExecuteNonQuery();
        }

        public void ExibirCoraisBanco()
        {
            using var conexao =
                new SqliteConnection(_conexao);

            conexao.Open();

            string sql =
                "SELECT * FROM Corais";

            using var comando =
                new SqliteCommand(sql, conexao);

            using var leitor =
                comando.ExecuteReader();

            Console.WriteLine("\n===============================================");
            Console.WriteLine("      DADOS RECUPERADOS DO SQLITE");
            Console.WriteLine("===============================================");

            while (leitor.Read())
            {
                Console.WriteLine(
                    $"{leitor["Id"]} - " +
                    $"{leitor["NomePopular"]} - " +
                    $"{leitor["StatusSaude"]}");
            }

            Console.WriteLine("===============================================\n");
        }
    }
}