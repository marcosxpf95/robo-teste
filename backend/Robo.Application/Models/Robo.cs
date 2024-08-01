using Robo.Application.Enums;

namespace Robo.Application.Models;

public class Robo
{
    public required CabecaRotacao CabecaRotacao { get; set; }
    public string[]? ProximosCabecaRotacao { get; set; }
    public required CabecaInclinacao CabecaInclicacao { get; set; }
    public string[]? ProximosCabecaInclinacao { get; set; }
    public required CotoveloContracao BracoDireitoCotoveloContracao { get; set; }
    public string[]? ProximosBracoDireitoCotoveloContracao { get; set; }
    public required PulsoRotacao BracoDireitoPulsoRotacao { get; set; }
    public string[]? ProximosBracoDireitoPulsoRotacao { get; set; }
    public required CotoveloContracao BracoEsquerdoCotoveloContracao { get; set; }
    public string[]? ProximosBracoEsquerdoCotoveloContracao { get; set; }
    public required PulsoRotacao BracoEsquerdoPulsoRotacao { get; set; }
    public string[]? ProximosBracoEsquerdoPulsoRotacao { get; set; }
}
