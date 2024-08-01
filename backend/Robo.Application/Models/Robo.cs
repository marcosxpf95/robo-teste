using Robo.Application.Enums;
using Robo.Contracts.Requests;

namespace Robo.Application.Models;


public class Robo
{
    public Braco BracoEsquerdo { get; set; }
    public Braco BracoDireito { get; set; }
    public Cabeca Cabeca { get; set; }
    public string[]? ProximosCabecaRotacao { get; set; }
    public string[]? ProximosCabecaInclinacao { get; set; }
    public string[]? ProximosBracoDireitoCotoveloContracao { get; set; }
    public string[]? ProximosBracoDireitoPulsoRotacao { get; set; }
    public string[]? ProximosBracoEsquerdoCotoveloContracao { get; set; }
    public string[]? ProximosBracoEsquerdoPulsoRotacao { get; set; }

    public Robo()
    {
        BracoEsquerdo = new Braco();
        BracoDireito = new Braco();
        Cabeca = new Cabeca();
    }

    public ValidacaoResponse EnviarComando(string comando)
    {
        ValidacaoResponse validacaoResponse;
        var partes = comando.Split(':');

        if (partes.Length < 3)
        {
            throw new ArgumentException("Comando inválido.");
        }

        var membro = partes[0];
        var movimento = partes[1];
        var estado = partes[2];

        switch (membro)
        {
            case "Cabeca":
                validacaoResponse = AtualizarCabeca(movimento, estado);
                break;
            case "BracoEsquerdo":
                validacaoResponse = AtualizarBracoEsquerdo(movimento, estado);
                break;
            case "BracoDireito":
                validacaoResponse = AtualizarBracoDireito(movimento, estado);
                break;
            default:
                throw new ArgumentException("Componente desconhecido.");
        }

        return validacaoResponse;
    }

    public void DefinirProximosEstados()
    {
        ProximosCabecaRotacao = ObterProximosEstados<Cabeca.RotacaoEstados>(this.Cabeca.Rotacao);
        ProximosCabecaInclinacao = ObterProximosEstados<Cabeca.InclinacaoEstados>(this.Cabeca.Inclinacao);
        ProximosBracoDireitoCotoveloContracao = ObterProximosEstados<Braco.CotoveloEstados>(this.BracoDireito.Cotovelo);
        ProximosBracoDireitoPulsoRotacao = ObterProximosEstados<Braco.PulsoEstados>(this.BracoDireito.Pulso);
        ProximosBracoEsquerdoCotoveloContracao = ObterProximosEstados<Braco.CotoveloEstados>(this.BracoEsquerdo.Cotovelo);
        ProximosBracoEsquerdoPulsoRotacao = ObterProximosEstados<Braco.PulsoEstados>(this.BracoEsquerdo.Pulso);
    }

    private ValidacaoResponse AtualizarCabeca(string movimento, string estado)
    {
        switch (movimento)
        {
            case "Rotacao":
                if (Enum.TryParse<Cabeca.RotacaoEstados>(estado, out var rotacao))
                {
                    return AtualizarCabecaRotacao(estado, rotacao);
                }
                else
                {
                    return new ValidacaoResponse
                    {
                        Success = true,
                        Message = "Estado de rotação inválido."
                    };
                }
            case "Inclinacao":
                if (Enum.TryParse<Cabeca.InclinacaoEstados>(estado, out var inclinacao))
                {
                    return AtualizarInclinacao(estado, inclinacao);
                }
                else
                {
                    return new ValidacaoResponse
                    {
                        Success = false,
                        Message = "Estado de inclinação inválido."
                    };
                }
            default:
                return new ValidacaoResponse
                {
                    Success = false,
                    Message = "Movimento de cabeça desconhecido"
                };
        }
    }

    private ValidacaoResponse AtualizarCabecaRotacao(string estado, Cabeca.RotacaoEstados rotacao)
    {
        if (this.Cabeca.Inclinacao == Cabeca.InclinacaoEstados.ParaBaixo)
        {
            return new ValidacaoResponse
            {
                Success = false,
                Message = "Não é possível rotacionar a cabeça enquanto a inclinação da cabeça for para baixo."
            };
        }

        var proximosEstadosPossiveis = ObterProximosEstados<Cabeca.RotacaoEstados>(this.Cabeca!.Rotacao);

        if (!IsEstadoValido(estado, proximosEstadosPossiveis))
        {
            return new ValidacaoResponse
            {
                Success = false,
                Message = $"O valor fornecido para rotacionar a cabeça não é válido. Valores possíveis: {string.Join(", ", proximosEstadosPossiveis)}."
            };
        }

        Cabeca.Rotacao = rotacao;

        return new ValidacaoResponse
        {
            Success = true,
            Message = "Rotação cabeça atualizado com sucesso."
        };
    }

    private ValidacaoResponse AtualizarInclinacao(string estado, Cabeca.InclinacaoEstados inclinacao)
    {
        var proximosEstadosPossiveis = ObterProximosEstados<Cabeca.InclinacaoEstados>(this.Cabeca!.Inclinacao);

        if (!IsEstadoValido(estado, proximosEstadosPossiveis))
        {
            return new ValidacaoResponse
            {
                Success = false,
                Message = $"O valor fornecido para inclinar a cabeça não é válido. Valores possíveis: {string.Join(", ", proximosEstadosPossiveis)}."
            };
        }

        Cabeca.Inclinacao = inclinacao;

        return new ValidacaoResponse
        {
            Success = true,
            Message = "Inclinação cabeça atualizado com sucesso."
        };
    }

    private ValidacaoResponse AtualizarBracoEsquerdo(string movimento, string estado)
    {
        return AtualizarBraco(this.BracoEsquerdo, movimento, estado);
    }

    private ValidacaoResponse AtualizarBracoDireito(string movimento, string estado)
    {
        return AtualizarBraco(this.BracoDireito, movimento, estado);
    }

    private ValidacaoResponse AtualizarBraco(Braco braco, string tipo, string estado)
    {
        switch (tipo)
        {
            case "Cotovelo":
                return AtualizarCotovelo(braco, estado);
            case "Pulso":
                return AtualizarPulso(braco, estado);
            default:
                return new ValidacaoResponse
                {
                    Success = false,
                    Message = $"Tipo de braço desconhecido."
                };
        }
    }

    private ValidacaoResponse AtualizarCotovelo(Braco braco, string estado)
    {
        if (Enum.TryParse<Braco.CotoveloEstados>(estado, out var cotovelo))
        {
            var proximosEstadosPossiveis = ObterProximosEstados<Braco.CotoveloEstados>(braco.Cotovelo);

            if (!IsEstadoValido(estado, proximosEstadosPossiveis))
            {
                return new ValidacaoResponse
                {
                    Success = false,
                    Message = $"O valor fornecido para o estado do cotovelo não é válido. Valores possíveis: {string.Join(", ", proximosEstadosPossiveis)}"
                };
            }

            braco.Cotovelo = cotovelo;

            return new ValidacaoResponse
            {
                Success = true,
                Message = $"Estado do cotovelo atualizado com sucesso."
            };
        }
        else
        {
            return new ValidacaoResponse
            {
                Success = false,
                Message = $"Estado de cotovelo inválido."
            };
        }
    }

    private ValidacaoResponse AtualizarPulso(Braco braco, string estado)
    {
        if (Enum.TryParse<Braco.PulsoEstados>(estado, out var pulso))
        {
            if (braco.Cotovelo != Braco.CotoveloEstados.FortementeContraido)
            {
                return new ValidacaoResponse
                {
                    Success = false,
                    Message = $"Não é possível rotacionar o pulso se o cotovelo não estiver fortemente contraído."
                };
            }

            var proximosEstadosPossiveis = ObterProximosEstados<Braco.PulsoEstados>(braco.Pulso);

            if (!IsEstadoValido(estado, proximosEstadosPossiveis))
            {
                return new ValidacaoResponse
                {
                    Success = false,
                    Message = $"O valor fornecido para o estado do pulso não é válido. Valores possíveis: {string.Join(", ", proximosEstadosPossiveis)}"
                };
            }

            braco.Pulso = pulso;

            return new ValidacaoResponse
            {
                Success = true,
                Message = $"Estado do pulso atualizado com sucesso."
            };
        }
        else
        {
            return new ValidacaoResponse
            {
                Success = false,
                Message = $"Estado de pulso inválido."
            };
        }
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

    private bool IsEstadoValido(string? estado, string[]? proximosEstadosValidos)
    {
        return string.IsNullOrEmpty(estado) || (proximosEstadosValidos?.Contains(estado) ?? false);
    }
}
