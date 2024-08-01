namespace Robo.Application.Services;

using Robo.Application.Models;
using Robo.Contracts.Requests;

public interface IRoboServices
{
    Robo? Get();
    ValicaoResponse InclinarCabeca(string comando);
    ValicaoResponse RotacionarCabeca(string comando);
    ValicaoResponse ContrairBracoDireito(string comando);
    ValicaoResponse RatacionarBracoDireito(string comando);
    ValicaoResponse ContrairBracoEsquerdo(string comando);
    ValicaoResponse RatacionarBracoEsquerdo(string comando);
}
