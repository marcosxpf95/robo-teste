namespace Robo.Application.Models;

public class Cabeca
{
    public RotacaoEstados Rotacao { get; set; }
    public InclinacaoEstados Inclinacao { get; set; }

    public Cabeca()
    {
        Rotacao = RotacaoEstados.Repouso;
        Inclinacao = InclinacaoEstados.Repouso;
    }

    public enum RotacaoEstados
    {
        RotacaoMenos90,
        RotacaoMenos45,
        Repouso,
        Rotacao45,
        Rotacao90,
    }

    public enum InclinacaoEstados
    {
        ParaCima,
        Repouso,
        ParaBaixo,
    }
}
