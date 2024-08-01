namespace Robo.Application.Models;

public class Braco
{
    public CotoveloEstados Cotovelo { get; set; }
    public PulsoEstados Pulso { get; set; }

    public Braco()
    {
        Cotovelo = CotoveloEstados.Repouso;
        Pulso = PulsoEstados.Repouso;
    }

    public enum CotoveloEstados
    {
        Repouso,
        LevementeContraido,
        Contraido,
        FortementeContraido
    }

    public enum PulsoEstados
    {
        RotacaoMenos90,
        RotacaoMenos45,
        Repouso,
        Rotacao45,
        Rotacao90,
        Rotacao135,
        Rotacao180
    }
}
