namespace Robo.Application.Services;

using Robo.Application.Models;
using Robo.Application.Repositories;
using Robo.Contracts.Requests;
public class RoboServices : IRoboServices
{
    private readonly IRoboRepository _roboRepository;
    public RoboServices(IRoboRepository roboRepository)
    {
        _roboRepository = roboRepository;
    }

    public Robo? Get()
    {
        var robo = _roboRepository.Get();

        if (robo is null)
            return null;

        robo.DefinirProximosEstados();
        return robo!;
    }

    public ValidacaoResponse EnviarComando(string? comando)
    {
        var robo = Get();

        return robo!.EnviarComando(comando!);
    }
}
