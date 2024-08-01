namespace Robo.Application.Data;

using Robo.Application.Enums;
using Robo.Application.Models;

public static class RoboInitializer
{
    public static Robo CriarInitializedRobo()
    {
        return new Robo
        {
            CabecaRotacao = CabecaRotacao.EmRepouso,
            CabecaInclicacao = CabecaInclinacao.EmRepouso,
            BracoEsquerdoPulsoRotacao = PulsoRotacao.EmRepouso,
            BracoEsquerdoCotoveloContracao = CotoveloContracao.EmRepouso,
            BracoDireitoPulsoRotacao = PulsoRotacao.EmRepouso,
            BracoDireitoCotoveloContracao = CotoveloContracao.EmRepouso
        };
    }
}
