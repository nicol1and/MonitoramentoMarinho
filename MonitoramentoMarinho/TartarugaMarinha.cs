namespace MonitoramentoMarinho
{
    public class TartarugaMarinha : EspecieMarinha
    {
        public string Situacao { get; set; }

        public TartarugaMarinha(
            int id,
            string nomeCientifico,
            string nomePopular,
            string situacao)
            : base(id, nomeCientifico, nomePopular)
        {
            Situacao = situacao;
        }
    }
}