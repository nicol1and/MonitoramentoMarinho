using System;
using System.Collections.Generic;
using System.Linq;

namespace MonitoramentoMarinho
{
    public class GerenciadorMonitoramento
    {
        private readonly List<EspecieMarinha> _catalogo =
            new List<EspecieMarinha>();

        public void GerarRelatorioBiodiversidade()
        {
            Console.WriteLine("\n===============================================");
            Console.WriteLine("      RELATÓRIO DE BIODIVERSIDADE MARINHA");
            Console.WriteLine("===============================================");

            Console.WriteLine($"Corais: {_catalogo.OfType<CoralEspecie>().Count()}");
            Console.WriteLine($"Peixes: {_catalogo.OfType<PeixeEspecie>().Count()}");
            Console.WriteLine($"Tartarugas: {_catalogo.OfType<TartarugaMarinha>().Count()}");
            Console.WriteLine($"Golfinhos: {_catalogo.OfType<Golfinho>().Count()}");

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine($"Total de registros: {_catalogo.Count}");

            Console.WriteLine("===============================================\n");
        }

        public void GerarRelatorioSaude()
        {
            Console.WriteLine("\n===============================================");
            Console.WriteLine("  RELATÓRIO DE SAÚDE DOS RECIFES DE CORAL");
            Console.WriteLine("===============================================");

            var corais = _catalogo.OfType<CoralEspecie>().ToList();

            if (!corais.Any())
            {
                Console.WriteLine("Nenhum coral registrado.");
                return;
            }

            var gruposSaude = corais.GroupBy(c => c.StatusSaude);

            foreach (var grupo in gruposSaude)
            {
                int quantidade = grupo.Count();
                double percentual =
                    ((double)quantidade / corais.Count) * 100;

                Console.WriteLine(
                    $"- Status: {grupo.Key,-12} | Qtd: {quantidade} | Proporção: {percentual:F1}%");
            }

            Console.WriteLine("===============================================\n");
        }
    }
}