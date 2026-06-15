using System;
using System.Collections.Generic;

namespace MonitoramentoMarinho
{
    class Program
    {
        private static List<string> observacoes =
    new List<string>
    {
        "Golfinho-rotador: Comportamento social observado durante monitoramento próximo à Baía dos Golfinhos.",

        "Coral-cérebro: Pequena área de branqueamento observada em colônia localizada a 6 metros de profundidade."
    };
        private static void AreaPesquisador(
            GerenciadorMonitoramento gerenciador)
        {
            Console.Write("Usuário: ");
            string usuario = Console.ReadLine() ?? "";

            Console.Write("Senha: ");
            string senha = Console.ReadLine() ?? "";

            if (usuario != "pesquisadoruninter" ||
                senha != "projetotamar1234")
            {
                Console.WriteLine("\nAcesso negado.");
                return;
            }

            int opcao;

            do
            {
                Console.Clear();

                Console.WriteLine("===================================");
                Console.WriteLine(" ÁREA DO PESQUISADOR");
                Console.WriteLine("===================================");
                Console.WriteLine("1 - Registrar nova espécie");
                Console.WriteLine("2 - Fazer observação");
                Console.WriteLine("3 - Consultar observações");
                Console.WriteLine("0 - Voltar");

                Console.Write("\nEscolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    opcao = -1;
                }

                switch (opcao)
                {
                    case 1:
                        RegistrarNovaEspecie(gerenciador);
                        break;

                    case 2:
                        FazerObservacao();
                        break;

                    case 3:
                        ConsultarObservacoes();
                        break;

                    case 0:
                        break;

                    default:
                        Console.WriteLine("\nOpção inválida.");
                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcao != 0);
        }

        private static void FazerObservacao()
        {
            Console.Clear();

            Console.WriteLine("===================================");
            Console.WriteLine(" REGISTRAR OBSERVAÇÃO");
            Console.WriteLine("===================================");

            Console.Write("Espécie observada: ");
            string especie = Console.ReadLine() ?? "";

            Console.Write("Observação: ");
            string observacao = Console.ReadLine() ?? "";

            observacoes.Add($"{especie}: {observacao}");

            Console.WriteLine("\nObservação registrada com sucesso!");
        }

        private static void ConsultarObservacoes()
        {
            Console.Clear();

            Console.WriteLine("===================================");
            Console.WriteLine(" OBSERVAÇÕES REGISTRADAS");
            Console.WriteLine("===================================");

            foreach (var observacao in observacoes)
            {
                Console.WriteLine($"\n• {observacao}");
            }

            Console.WriteLine("\n===================================");
        }

        private static void RegistrarNovaEspecie(
        GerenciadorMonitoramento gerenciador)
        {
            Console.Clear();

            Console.WriteLine("===================================");
            Console.WriteLine(" REGISTRAR NOVA ESPÉCIE");
            Console.WriteLine("===================================");
            Console.WriteLine("1 - Coral");
            Console.WriteLine("2 - Peixe");
            Console.WriteLine("3 - Tartaruga");
            Console.WriteLine("4 - Golfinho");

            Console.Write("\nTipo da espécie: ");

            if (!int.TryParse(Console.ReadLine(), out int tipo))
            {
                Console.WriteLine("\nTipo inválido.");
                return;
            }

            int id = gerenciador.GerarProximoId();

            Console.WriteLine($"ID gerado automaticamente: {id}");

            Console.Write("Nome científico: ");
            string nomeCientifico = Console.ReadLine() ?? "";

            Console.Write("Nome popular: ");
            string nomePopular = Console.ReadLine() ?? "";

            switch (tipo)
            {
                case 1:
                    Console.Write("Status de saúde: ");
                    string status = Console.ReadLine() ?? "";

                    Console.Write("Profundidade em metros: ");
                    double profundidade = double.Parse(Console.ReadLine() ?? "0");

                    gerenciador.RegistrarObservacao(
                        new CoralEspecie(
                            id,
                            nomeCientifico,
                            nomePopular,
                            status,
                            profundidade));

                    break;

                case 2:
                    Console.Write("Tamanho em cm: ");
                    double tamanho = double.Parse(Console.ReadLine() ?? "0");

                    gerenciador.RegistrarObservacao(
                        new PeixeEspecie(
                            id,
                            nomeCientifico,
                            nomePopular,
                            tamanho));

                    break;

                case 3:
                    Console.Write("Situação: ");
                    string situacao = Console.ReadLine() ?? "";

                    gerenciador.RegistrarObservacao(
                        new TartarugaMarinha(
                            id,
                            nomeCientifico,
                            nomePopular,
                            situacao));

                    break;

                case 4:
                    Console.Write("Quantidade observada: ");
                    int quantidade = int.Parse(Console.ReadLine() ?? "0");

                    gerenciador.RegistrarObservacao(
                        new Golfinho(
                            id,
                            nomeCientifico,
                            nomePopular,
                            quantidade));

                    break;

                default:
                    Console.WriteLine("\nTipo inválido.");
                    break;
            }

            Console.WriteLine("\nEspécie registrada com sucesso!");
        }

        static void Main(string[] args)
        {
            var gerenciador = new GerenciadorMonitoramento();

            PopulateDadosIniciais(gerenciador);
            gerenciador.CarregarEspeciesCadastradas();

            int opcao;

            do
            {
                Console.Clear();

                Console.WriteLine("===================================");
                Console.WriteLine(" SISTEMA DE MONITORAMENTO MARINHO ");
                Console.WriteLine("===================================");
                Console.WriteLine("1 - Listar espécies");
                Console.WriteLine("2 - Relatório biodiversidade");
                Console.WriteLine("3 - Relatório saúde dos corais");
                Console.WriteLine("4 - Consultar o banco de dados");
                Console.WriteLine("5 - Área do Pesquisador");
                Console.WriteLine("0 - Sair");

                Console.Write("\nEscolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    opcao = -1;
                }

                Console.Clear();

                switch (opcao)
                {
                    case 1:
                        gerenciador.ListarEspecies();
                        break;

                    case 2:
                        gerenciador.GerarRelatorioBiodiversidade();
                        break;

                    case 3:
                        gerenciador.GerarRelatorioSaude();
                        break;

                    case 4:
                        gerenciador.ExibirBancoDados();
                        break;

                    case 5:
                        AreaPesquisador(gerenciador);
                        break;

                    case 0:
                        Console.WriteLine("Encerrando sistema...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcao != 0);
        }

        private static void PopulateDadosIniciais(
    GerenciadorMonitoramento gerenciador)
        {

            gerenciador.CatalogarEspecie(
                new CoralEspecie(
                    1,
                    "Millepora alcicornis",
                    "Coral-de-fogo",
                    "Saudável",
                    4.5));

            gerenciador.CatalogarEspecie(
                new CoralEspecie(
                    2,
                    "Montastraea cavernosa",
                    "Coral-cérebro",
                    "Branqueado",
                    6.2));

            gerenciador.CatalogarEspecie(
                new CoralEspecie(
                    3,
                    "Siderastrea stellata",
                    "Coral-estrela",
                    "Saudável",
                    5.1));

            gerenciador.CatalogarEspecie(
                new PeixeEspecie(
                    4,
                    "Scarus trispinosus",
                    "Peixe-papagaio",
                    45));

            gerenciador.CatalogarEspecie(
                new PeixeEspecie(
                    5,
                    "Acanthurus chirurgus",
                    "Peixe-cirurgião",
                    30));

            gerenciador.CatalogarEspecie(
                new PeixeEspecie(
                    6,
                    "Pomacanthus paru",
                    "Peixe-anjo-francês",
                    35));

            gerenciador.CatalogarEspecie(
                new PeixeEspecie(
                    7,
                    "Abudefduf saxatilis",
                    "Sargentinho",
                    15));

            gerenciador.CatalogarEspecie(
                new PeixeEspecie(
                    8,
                    "Chaetodon striatus",
                    "Peixe-borboleta",
                    18));

            gerenciador.CatalogarEspecie(
                new TartarugaMarinha(
                    9,
                    "Chelonia mydas",
                    "Tartaruga-verde",
                    "Monitorada"));

            gerenciador.CatalogarEspecie(
                new TartarugaMarinha(
                    10,
                    "Eretmochelys imbricata",
                    "Tartaruga-de-pente",
                    "Monitorada"));

            gerenciador.CatalogarEspecie(
                new Golfinho(
                    11,
                    "Stenella longirostris",
                    "Golfinho-rotador",
                    12));

            gerenciador.CatalogarEspecie(
                new Golfinho(
                    12,
                    "Tursiops truncatus",
                    "Golfinho-nariz-de-garrafa",
                    5));

            gerenciador.CatalogarEspecie(
                new Golfinho(
                    13,
                    "Stenella attenuata",
                    "Golfinho-pantropical-pintado",
                    8));
        }
    }
}