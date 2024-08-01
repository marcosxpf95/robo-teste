namespace Robo.Application.Services;

using Robo.Application.Models;
using Robo.Contracts.Requests;

public interface IRoboServices
{
    Robo? Get();
    ValidacaoResponse EnviarComando(string? comando);
}
