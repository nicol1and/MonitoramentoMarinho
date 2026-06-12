using System;

namespace MonitoramentoMarinho
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicializando Sistema de Monitoramento Marinho...\n");

            var gerenciador = new GerenciadorMonitoramento();

            PopulateDadosIniciais(gerenciador);



            Console.WriteLine("Execução concluída. Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }

        private static void PopulateDadosIniciais(
            GerenciadorMonitoramento gerenciador)
        {
            // Corais

            gerenciador.RegistrarObservacao(
                new CoralEspecie(
                    1,
                    "Millepora alcicornis",
                    "Coral-de-fogo",
                    "Saudável",
                    4.5));

            gerenciador.RegistrarObservacao(
                new CoralEspecie(
                    2,
                    "Montastraea cavernosa",
                    "Coral-cérebro",
                    "Branqueado",
                    6.2));

            // Peixe

            gerenciador.RegistrarObservacao(
                new PeixeEspecie(
                    3,
                    "Scarus trispinosus",
                    "Peixe-papagaio",
                    45));

            // Tartaruga

            gerenciador.RegistrarObservacao(
                new TartarugaMarinha(
                    4,
                    "Chelonia mydas",
                    "Tartaruga-verde",
                    "Monitorada"));

            // Golfinho

            gerenciador.RegistrarObservacao(
                new Golfinho(
                    5,
                    "Stenella longirostris",
                    "Golfinho-rotador",
                    12));
        }
    }
}