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
            CabecaRotacao = robo.Cabeca.Rotacao.ToString(),
            ProximosCabecaRotacao = robo.ProximosCabecaRotacao,
            CabecaInclinacao = robo.Cabeca.Inclinacao.ToString(),
            ProximosCabecaInclinacao = robo.ProximosCabecaInclinacao,
            BracoDireitoCotoveloContracao = robo.BracoDireito.Cotovelo.ToString(),
            ProximosBracoDireitoCotoveloContracao = robo.ProximosBracoDireitoCotoveloContracao,
            BracoDireitoPulsoRotacao = robo.BracoDireito.Pulso.ToString(),
            ProximosBracoDireitoPulsoRotacao = robo.ProximosBracoDireitoPulsoRotacao,
            BracoEsquerdoCotoveloContracao = robo.BracoEsquerdo.Cotovelo.ToString(),
            ProximosBracoEsquerdoCotoveloContracao = robo.ProximosBracoEsquerdoCotoveloContracao,
            BracoEsquerdoPulsoRotacao = robo.BracoEsquerdo.Pulso.ToString(),
            ProximosBracoEsquerdoPulsoRotacao = robo.ProximosBracoEsquerdoPulsoRotacao
        };
    }
}
