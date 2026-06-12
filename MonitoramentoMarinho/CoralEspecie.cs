using System;

namespace MonitoramentoMarinho
{
    public class CoralEspecie : EspecieMarinha
    {
        public string StatusSaude { get; set; }
        public double ProfundidadeMetros { get; set; }

        public CoralEspecie(
            int id,
            string nomeCientifico,
            string nomePopular,
            string statusSaude,
            double profundidade)
            : base(id, nomeCientifico, nomePopular)
        {
            StatusSaude = statusSaude;
            ProfundidadeMetros = profundidade;
        }
    }
}