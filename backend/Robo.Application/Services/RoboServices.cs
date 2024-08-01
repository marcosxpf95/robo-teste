namespace Robo.Application.Services;

using Robo.Application.Enums;
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

        robo = DefinirProximosEstados(robo);
        return robo!;
    }

    public ValicaoResponse InclinarCabeca(string? comando)
    {
        var robo = _roboRepository.Get();
        var proximosComandosPossiveis = ObterProximosEstados<CabecaInclinacao>(robo!.CabecaInclicacao);

        if (!IsStateValid(comando, proximosComandosPossiveis))
        {
            return new ValicaoResponse
            {
                Success = false,
                Message = $"O valor fornecido para a inclinação da cabeça não é válido. Valores possíveis: {string.Join(", ", proximosComandosPossiveis)}"
            };
        }

        robo!.CabecaInclicacao = ParseEnum<CabecaInclinacao>(comando!.ToString());

        return new ValicaoResponse { Success = true };
    }

    public ValicaoResponse RotacionarCabeca(string comando)
    {
        var robo = _roboRepository.Get();

        if (robo!.CabecaInclicacao == CabecaInclinacao.ParaBaixo)
        {
            return new ValicaoResponse
            {
                Success = false,
                Message = $"Não é possível rotacionar a cabeça enquanto a inclinação da cabeça for para baixo"
            };
        }

        var proximosComandosPossiveis = ObterProximosEstados<CabecaRotacao>(robo!.CabecaRotacao);

        if (!IsStateValid(comando, proximosComandosPossiveis))
        {
            return new ValicaoResponse
            {
                Success = false,
                Message = $"O valor fornecido para rotacionar a cabeça não é válido. Valores possíveis: {string.Join(", ", proximosComandosPossiveis)}"
            };
        }

        robo!.CabecaRotacao = ParseEnum<CabecaRotacao>(comando!.ToString());

        return new ValicaoResponse { Success = true };
    }

    public ValicaoResponse ContrairBracoDireito(string comando)
    {
        var robo = _roboRepository.Get();
        var proximosComandosPossiveis = ObterProximosEstados<CotoveloContracao>(robo!.BracoDireitoCotoveloContracao);

        if (!IsStateValid(comando, proximosComandosPossiveis))
        {
            return new ValicaoResponse
            {
                Success = false,
                Message = $"O valor fornecido para a contração do braço direito não é válido. Valores possíveis: {string.Join(", ", proximosComandosPossiveis)}"
            };
        }

        robo!.BracoDireitoCotoveloContracao = ParseEnum<CotoveloContracao>(comando!.ToString());

        return new ValicaoResponse { Success = true };
    }

    public ValicaoResponse RatacionarBracoDireito(string comando)
    {
        var robo = _roboRepository.Get();
        if (robo!.BracoDireitoCotoveloContracao != CotoveloContracao.FortementeContraido)
        {
            return new ValicaoResponse
            {
                Success = false,
                Message = $"Não é possível rotacionar o braço se o cotovelo não estiver fortemente contraído."
            };
        }

        var proximosComandosPossiveis = ObterProximosEstados<PulsoRotacao>(robo!.BracoDireitoPulsoRotacao);

        if (!IsStateValid(comando, proximosComandosPossiveis))
        {
            return new ValicaoResponse
            {
                Success = false,
                Message = $"O valor fornecido para a rotação do pulso direito não é válido. Valores possíveis: {string.Join(", ", proximosComandosPossiveis)}"
            };
        }

        robo!.BracoDireitoPulsoRotacao = ParseEnum<PulsoRotacao>(comando!.ToString());

        return new ValicaoResponse { Success = true };
    }

    public ValicaoResponse ContrairBracoEsquerdo(string comando)
    {
        var robo = _roboRepository.Get();
        var proximosComandosPossiveis = ObterProximosEstados<CotoveloContracao>(robo!.BracoEsquerdoCotoveloContracao);

        if (!IsStateValid(comando, proximosComandosPossiveis))
        {
            return new ValicaoResponse
            {
                Success = false,
                Message = $"O valor fornecido para a contração do braço esquerdo não é válido. Valores possíveis: {string.Join(", ", proximosComandosPossiveis)}"
            };
        }

        robo!.BracoEsquerdoCotoveloContracao = ParseEnum<CotoveloContracao>(comando!.ToString());

        return new ValicaoResponse { Success = true };
    }

    public ValicaoResponse RatacionarBracoEsquerdo(string comando)
    {
        var robo = _roboRepository.Get();

        if (robo!.BracoEsquerdoCotoveloContracao != CotoveloContracao.FortementeContraido)
        {
            return new ValicaoResponse
            {
                Success = false,
                Message = $"Não é possível rotacionar o braço se o cotovelo não estiver fortemente contraído."
            };
        }

        var proximosComandosPossiveis = ObterProximosEstados<PulsoRotacao>(robo!.BracoEsquerdoPulsoRotacao);

        if (!IsStateValid(comando, proximosComandosPossiveis))
        {
            return new ValicaoResponse
            {
                Success = false,
                Message = $"O valor fornecido para a rotação do pulso esquerdo não é válido. Valores possíveis: {string.Join(", ", proximosComandosPossiveis)}"
            };
        }

        robo!.BracoEsquerdoPulsoRotacao = ParseEnum<PulsoRotacao>(comando!.ToString());

        return new ValicaoResponse { Success = true };
    }

    private bool IsStateValid(string? requestedState, string[]? validStates)
    {
        return string.IsNullOrEmpty(requestedState) || (validStates?.Contains(requestedState) ?? false);
    }

    private Robo? DefinirProximosEstados(Robo robo)
    {

        robo.ProximosCabecaRotacao = ObterProximosEstados<CabecaRotacao>(robo.CabecaRotacao);
        robo.ProximosCabecaInclinacao = ObterProximosEstados<CabecaInclinacao>(robo.CabecaInclicacao);
        robo.ProximosBracoDireitoCotoveloContracao = ObterProximosEstados<CotoveloContracao>(robo.BracoDireitoCotoveloContracao);
        robo.ProximosBracoDireitoPulsoRotacao = ObterProximosEstados<PulsoRotacao>(robo.BracoDireitoPulsoRotacao);
        robo.ProximosBracoEsquerdoCotoveloContracao = ObterProximosEstados<CotoveloContracao>(robo.BracoEsquerdoCotoveloContracao);
        robo.ProximosBracoEsquerdoPulsoRotacao = ObterProximosEstados<PulsoRotacao>(robo.BracoEsquerdoPulsoRotacao);

        return robo;
    }

    private string[] ObterProximosEstados<TEnum>(TEnum estadoAtual) where TEnum : Enum
    {
        var estados = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToArray();
        int indexAtual = Array.IndexOf(estados, estadoAtual);

        int indexAnterior = indexAtual > 0 ? (indexAtual - 1) : -1;
        int indexProximo = indexAtual < estados.Length - 1 ? (indexAtual + 1) : -1;

        var resultado = new List<string>();

        if (indexAnterior != -1)
        {
            resultado.Add(estados[indexAnterior].ToString());
        }

        if (indexProximo != -1)
        {
            resultado.Add(estados[indexProximo].ToString());
        }

         return resultado.ToArray();
    }

    private TEnum ParseEnum<TEnum>(string? value) where TEnum : struct, Enum
    {
        if (Enum.TryParse(value, out TEnum result))
        {
            return result;
        }
        throw new ArgumentException($"O valor '{value}' não é válido para o tipo enum '{typeof(TEnum).Name}'.");
    }
}
