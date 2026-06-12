namespace MonitoramentoMarinho
{
    public class Golfinho : EspecieMarinha
    {
        public int QuantidadeObservada { get; set; }

        public Golfinho(
            int id,
            string nomeCientifico,
            string nomePopular,
            int quantidade)
            : base(id, nomeCientifico, nomePopular)
        {
            QuantidadeObservada = quantidade;
        }
    }
}