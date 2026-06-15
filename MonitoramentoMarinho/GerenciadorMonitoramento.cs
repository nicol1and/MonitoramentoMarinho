using System;
using System.Collections.Generic;
using System.Linq;

namespace MonitoramentoMarinho
{
    public class GerenciadorMonitoramento
    {
        private readonly List<EspecieMarinha> _catalogo =
            new List<EspecieMarinha>();

        private readonly BancoDados _banco =
            new BancoDados();

        public void ExibirBancoDados()
        {
            _banco.ExibirCoraisBanco();
        }
        public int GerarProximoId()
        {
            if (!_catalogo.Any())
                return 1;

            return _catalogo.Max(e => e.Id) + 1;
        }
        public void CatalogarEspecie(EspecieMarinha especie)
        {
            if (especie == null)
                return;

            _catalogo.Add(especie);
        }

        public void RegistrarObservacao(EspecieMarinha novaEspecie)
        {
            if (novaEspecie == null)
                return;

            _catalogo.Add(novaEspecie);

            _banco.SalvarEspecieCadastrada(novaEspecie);

            if (novaEspecie is CoralEspecie coral)
            {
                _banco.SalvarCoral(coral);
            }

            Console.WriteLine(
                $"[REGISTRO] {novaEspecie.NomePopular} registrado por pesquisador.");
        }
        public void CarregarEspeciesCadastradas()
        {
            var especies = _banco.CarregarEspeciesCadastradas();

            foreach (var especie in especies)
            {
                _catalogo.Add(especie);
            }
        }
        public void ListarEspecies()
        {
            Console.WriteLine("\n===============================================");
            Console.WriteLine("          ESPÉCIES CATALOGADAS");
            Console.WriteLine("===============================================");

            int contador = 1;

            foreach (var especie in _catalogo.OrderBy(e => e.NomePopular))
            {
                Console.WriteLine(
                    $"{contador} - {especie.NomePopular} ({especie.NomeCientifico})");

                contador++;
            }

            Console.WriteLine("===============================================\n");
        }

        public void GerarRelatorioBiodiversidade()
        {
            Console.WriteLine("\n===============================================");
            Console.WriteLine("     RELATÓRIO DE BIODIVERSIDADE MARINHA");
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