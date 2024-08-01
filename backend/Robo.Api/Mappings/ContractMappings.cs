namespace Robo.Api.Mappings;

using Robo.Application.Models;
using Robo.Application.Enums;
using Robo.Contracts.Requests;
using Robo.Contracts.Responses;
public static class ContractMappings
{
    public static RoboResponse MatToResponse(this Robo robo)
    {
        return new RoboResponse
        {
            CabecaRotacao = robo.CabecaRotacao.ToString(),
            ProximosCabecaRotacao = robo.ProximosCabecaRotacao,
            CabecaInclinacao = robo.CabecaInclicacao.ToString(),
            ProximosCabecaInclinacao = robo.ProximosCabecaInclinacao,
            BracoDireitoCotoveloContracao = robo.BracoDireitoCotoveloContracao.ToString(),
            ProximosBracoDireitoCotoveloContracao = robo.ProximosBracoDireitoCotoveloContracao,
            BracoDireitoPulsoRotacao = robo.BracoDireitoPulsoRotacao.ToString(),
            ProximosBracoDireitoPulsoRotacao = robo.ProximosBracoDireitoPulsoRotacao,
            BracoEsquerdoCotoveloContracao = robo.BracoEsquerdoCotoveloContracao.ToString(),
            ProximosBracoEsquerdoCotoveloContracao = robo.ProximosBracoEsquerdoCotoveloContracao,
            BracoEsquerdoPulsoRotacao = robo.BracoEsquerdoPulsoRotacao.ToString(),
            ProximosBracoEsquerdoPulsoRotacao = robo.ProximosBracoEsquerdoPulsoRotacao
        };
    }
}
