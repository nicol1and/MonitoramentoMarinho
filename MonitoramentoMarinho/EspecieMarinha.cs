using System;

namespace MonitoramentoMarinho
{
    public class EspecieMarinha
    {
        public int Id { get; set; }
        public string NomeCientifico { get; set; }
        public string NomePopular { get; set; }
        public DateTime DataObservacao { get; set; }

        public EspecieMarinha(
            int id,
            string nomeCientifico,
            string nomePopular)
        {
            Id = id;
            NomeCientifico = nomeCientifico;
            NomePopular = nomePopular;
            DataObservacao = DateTime.Now;
        }
    }
}