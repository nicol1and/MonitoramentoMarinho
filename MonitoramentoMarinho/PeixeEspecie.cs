namespace MonitoramentoMarinho
{
    public class PeixeEspecie : EspecieMarinha
    {
        public double TamanhoCm { get; set; }

        public PeixeEspecie(
            int id,
            string nomeCientifico,
            string nomePopular,
            double tamanhoCm)
            : base(id, nomeCientifico, nomePopular)
        {
            TamanhoCm = tamanhoCm;
        }
    }
}