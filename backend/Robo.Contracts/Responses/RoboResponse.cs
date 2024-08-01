namespace Robo.Contracts.Responses;

public class RoboResponse
{
    public required string CabecaRotacao { get; init; }
    public string[]? ProximosCabecaRotacao { get; set; }
    public required string CabecaInclinacao { get; init; }
    public string[]? ProximosCabecaInclinacao { get; set; }
    public required string BracoDireitoCotoveloContracao { get; init; }
    public string[]? ProximosBracoDireitoCotoveloContracao { get; set; }
    public required string BracoDireitoPulsoRotacao { get; init; }
    public string[]? ProximosBracoDireitoPulsoRotacao { get; set; }
    public required string BracoEsquerdoCotoveloContracao { get; init; }
    public string[]? ProximosBracoEsquerdoCotoveloContracao { get; set; }
    public required string BracoEsquerdoPulsoRotacao { get; init; }
    public string[]? ProximosBracoEsquerdoPulsoRotacao { get; set; }
}
