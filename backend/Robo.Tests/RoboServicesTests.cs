namespace Robo.Tests;
using Xunit;
using Moq;
using Robo.Application.Repositories;
using Robo.Application.Services;
using FluentAssertions;
using Robo.Application.Models;

public class RoboServicesTests
{
    private readonly Mock<IRoboRepository> _mockRoboRepository;
    private readonly RoboServices _roboServices;

    public RoboServicesTests()
    {
        _mockRoboRepository = new Mock<IRoboRepository>();
        _roboServices = new RoboServices(_mockRoboRepository.Object);
    }

    #region testes cabeça

    [Fact]
    public void Get_RoboExistente_DeveRetornarRobo()
    {
        // Arrange
        var expectedRobo = new Robo();
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Act
        var result = _roboServices.Get();

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void Cabeca_Deve_Rotacionar_Quando_CabecaEstiverInclinadaParaCima()
    {
        // Arrange
        var expectedRobo = new Robo();
        expectedRobo.Cabeca.Inclinacao = Cabeca.InclinacaoEstados.ParaCima;
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Act
        var result = _roboServices.EnviarComando("Cabeca:Rotacao:RotacaoMenos45");

        // Assert
        expectedRobo.Cabeca.Rotacao.ToString().Should().Be(Cabeca.RotacaoEstados.RotacaoMenos45.ToString());
        result.Success.Should().Be(true);
        result.Message.Should().Be("Rotação cabeça atualizado com sucesso.");
    }

    [Fact]
    public void Cabeca_Deve_Rotacionar_Quando_CabecaEstiverEmRepouso()
    {
        // Arrange
        var expectedRobo = new Robo();
        expectedRobo.Cabeca.Inclinacao = Cabeca.InclinacaoEstados.Repouso;
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Act
        var result = _roboServices.EnviarComando("Cabeca:Rotacao:Rotacao45");

        // Assert
        expectedRobo.Cabeca.Rotacao.ToString().Should().Be(Cabeca.RotacaoEstados.Rotacao45.ToString());
        result.Success.Should().Be(true);
        result.Message.Should().Be("Rotação cabeça atualizado com sucesso.");
    }

    [Fact]
    public void Cabeca_NaoDeve_Rotacionar_Quando_CabecaEstiverInclinadaParaBaixo()
    {
        // Arrange
        var expectedRobo = new Robo();
        expectedRobo.Cabeca.Inclinacao = Cabeca.InclinacaoEstados.ParaBaixo;
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Act
        var result = _roboServices.EnviarComando("Cabeca:Rotacao:Rotacao45");

        // Assert
        expectedRobo.Cabeca.Rotacao.ToString().Should().Be(Cabeca.RotacaoEstados.Repouso.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("Não é possível rotacionar a cabeça enquanto a inclinação da cabeça for para baixo.");
    }

    [Fact]
    public void Cabeca_Deve_Inclinar_ParaCima_Quando_EstiverEmRepouso()
    {
        // Arrange
        var expectedRobo = new Robo();
        expectedRobo.Cabeca.Inclinacao = Cabeca.InclinacaoEstados.Repouso;
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Act
        var result = _roboServices.EnviarComando("Cabeca:Inclinacao:ParaCima");

        // Assert
        expectedRobo.Cabeca.Inclinacao.ToString().Should().Be(Cabeca.InclinacaoEstados.ParaCima.ToString());
        result.Success.Should().Be(true);
        result.Message.Should().Be("Inclinação cabeça atualizado com sucesso.");
    }

    [Fact]
    public void Cabeca_Nao_Deve_Inclinar_ParaCima_Quando_EstiverParaBaixo()
    {
        // Arrange
        var expectedRobo = new Robo();
        expectedRobo.Cabeca.Inclinacao = Cabeca.InclinacaoEstados.ParaBaixo;
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Act
        var result = _roboServices.EnviarComando("Cabeca:Inclinacao:ParaCima");

        // Assert
        expectedRobo.Cabeca.Inclinacao.ToString().Should().Be(Cabeca.InclinacaoEstados.ParaBaixo.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("O valor fornecido para inclinar a cabeça não é válido. Valores possíveis: Repouso.");
    }

    [Fact]
    public void Cabeca_Nao_Deve_InclinarParaBaixo_Quando_EstiverParaCima()
    {
        // Arrange
        var expectedRobo = new Robo();
        expectedRobo.Cabeca.Inclinacao = Cabeca.InclinacaoEstados.ParaCima;
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Act
        var result = _roboServices.EnviarComando("Cabeca:Inclinacao:ParaBaixo");

        // Assert
        expectedRobo.Cabeca.Inclinacao.ToString().Should().Be(Cabeca.InclinacaoEstados.ParaCima.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("O valor fornecido para inclinar a cabeça não é válido. Valores possíveis: Repouso.");
    }

    #endregion

    #region testes braco esquerdo
    [Fact]
    public void BracoEsquerdoCotovelo_Deve_Contrair_Quando_ComandoForValido()
    {
        // Arrange
        var expectedRobo = new Robo();
        expectedRobo.BracoEsquerdo.Cotovelo = Braco.CotoveloEstados.Repouso;
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Act
        var result = _roboServices.EnviarComando("BracoEsquerdo:Cotovelo:LevementeContraido");

        // Assert
        expectedRobo.BracoEsquerdo.Cotovelo.ToString().Should().Be(Braco.CotoveloEstados.LevementeContraido.ToString());
        result.Success.Should().Be(true);
    }

    [Fact]
    public void BracoEsquerdoCotovelo_NaoDeve_ContrairParaFortementeContraido_Quando_BracoEsquerdoCotoveloEstiverEmRepouso()
    {
        // Arrange
        var expectedRobo = new Robo();
        expectedRobo.BracoEsquerdo.Cotovelo = Braco.CotoveloEstados.Repouso;
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Act
        var result = _roboServices.EnviarComando("BracoEsquerdo:Cotovelo:FortementeContraido");

        // Assert
        expectedRobo.BracoEsquerdo.Cotovelo.ToString().Should().Be(Braco.CotoveloEstados.Repouso.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("O valor fornecido para o estado do cotovelo não é válido. Valores possíveis: LevementeContraido");
    }

    [Fact]
    public void BracoEsquerdoCotovelo_NaoDeve_ContrairParaRepouso_Quando_BracoEsquerdoCotoveloEstiverFortementeContraido()
    {
        // Arrange
        var expectedRobo = new Robo();
        expectedRobo.BracoEsquerdo.Cotovelo = Braco.CotoveloEstados.LevementeContraido;
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Act
        var result = _roboServices.EnviarComando("BracoEsquerdo:Cotovelo:FortementeContraido");

        // Assert
        expectedRobo.BracoEsquerdo.Cotovelo.ToString().Should().Be(Braco.CotoveloEstados.LevementeContraido.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("O valor fornecido para o estado do cotovelo não é válido. Valores possíveis: Repouso, Contraido");
    }

    [Fact]
    public void BracoEsquerdoPulso_Deve_Rotacionar_Quando_BracoEsquerdoCotoveloEsquerdoEstiverFortementeContraido()
    {
        // Arrange
        var expectedRobo = new Robo();
        expectedRobo.BracoEsquerdo.Cotovelo = Braco.CotoveloEstados.FortementeContraido;
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Act
        var result = _roboServices.EnviarComando("BracoEsquerdo:Pulso:Rotacao45");

        // Assert
        expectedRobo.BracoEsquerdo.Pulso.ToString().Should().Be(Braco.PulsoEstados.Rotacao45.ToString());
        result.Success.Should().Be(true);
    }

    [Fact]
    public void BracoEsquerdoPulso_NaoDeve_Rotacionar_Quando_BracoEsquerdoCotoveloNaoEstiverFortementeContraido()
    {
        // Arrange
        var expectedRobo = new Robo();
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Act
        var result = _roboServices.EnviarComando("BracoEsquerdo:Pulso:Rotacao45");

        // Assert
        expectedRobo.BracoEsquerdo.Pulso.ToString().Should().Be(Braco.PulsoEstados.Repouso.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("Não é possível rotacionar o pulso se o cotovelo não estiver fortemente contraído.");
    }

    [Fact]
    public void BracoEsquerdoPulso_NaoDeve_Rotacionar90_Quando_BracoEsquerdoPulsoEstiverEmRepouso()
    {
        // Arrange
        var expectedRobo = new Robo();
        expectedRobo.BracoEsquerdo.Cotovelo = Braco.CotoveloEstados.FortementeContraido;
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Arrage
        var result = _roboServices.EnviarComando("BracoEsquerdo:Pulso:Rotacao90");

        // Arrage
        expectedRobo.BracoEsquerdo.Pulso.ToString().Should().Be(Braco.PulsoEstados.Repouso.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("O valor fornecido para o estado do pulso não é válido. Valores possíveis: RotacaoMenos45, Rotacao45");
    }

    [Fact]
    public void BracoEsquerdoPulso_NaoDeve_RotacionarMenos90_Quando_BracoEsquerdoPulsoEstiverEmRepouso()
    {
        // Arrange
        var expectedRobo = new Robo();
        expectedRobo.BracoEsquerdo.Cotovelo = Braco.CotoveloEstados.FortementeContraido;
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Arrage
        var result = _roboServices.EnviarComando("BracoEsquerdo:Pulso:RotacaoMenos90");

        // Arrage
        expectedRobo.BracoEsquerdo.Pulso.ToString().Should().Be(Braco.PulsoEstados.Repouso.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("O valor fornecido para o estado do pulso não é válido. Valores possíveis: RotacaoMenos45, Rotacao45");
    }

    #endregion

    #region testes braco direito

    [Fact]
    public void BracoDireitoCotovelo_Deve_Contrair_Quando_ComandoForValido()
    {
        // Arrange
        var expectedRobo = new Robo();
        expectedRobo.BracoDireito.Cotovelo = Braco.CotoveloEstados.Repouso;
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Act
        var result = _roboServices.EnviarComando("BracoDireito:Cotovelo:LevementeContraido");

        // Assert
        expectedRobo.BracoDireito.Cotovelo.ToString().Should().Be(Braco.CotoveloEstados.LevementeContraido.ToString());
        result.Success.Should().Be(true);
    }

    [Fact]
    public void BracoDireitoCotovelo_NaoDeve_ContrairParaFortementeContraido_Quando_BracoDireitoCotoveloEstiverEmRepouso()
    {
        // Arrange
        var expectedRobo = new Robo();
        expectedRobo.BracoDireito.Cotovelo = Braco.CotoveloEstados.Repouso;
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Act
        var result = _roboServices.EnviarComando("BracoDireito:Cotovelo:FortementeContraido");

        // Assert
        expectedRobo.BracoDireito.Cotovelo.ToString().Should().Be(Braco.CotoveloEstados.Repouso.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("O valor fornecido para o estado do cotovelo não é válido. Valores possíveis: LevementeContraido");
    }

    [Fact]
    public void BracoDireitoCotovelo_NaoDeve_ContrairParaRepouso_Quando_BracoDireitoCotoveloEstiverFortementeContraido()
    {
        // Arrange
        var expectedRobo = new Robo();
        expectedRobo.BracoDireito.Cotovelo = Braco.CotoveloEstados.LevementeContraido;
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Act
        var result = _roboServices.EnviarComando("BracoDireito:Cotovelo:FortementeContraido");

        // Assert
        expectedRobo.BracoDireito.Cotovelo.ToString().Should().Be(Braco.CotoveloEstados.LevementeContraido.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("O valor fornecido para o estado do cotovelo não é válido. Valores possíveis: Repouso, Contraido");
    }

    [Fact]
    public void BracoDireitoPulso_Deve_Rotacionar_Quando_BracoCotoveloDireitoEstiverFortementeContraido()
    {
        // Arrange
        var expectedRobo = new Robo();
        expectedRobo.BracoDireito.Cotovelo = Braco.CotoveloEstados.FortementeContraido;
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Act
        var result = _roboServices.EnviarComando("BracoDireito:Pulso:Rotacao45");

        // Assert
        expectedRobo.BracoDireito.Pulso.ToString().Should().Be(Braco.PulsoEstados.Rotacao45.ToString());
        result.Success.Should().Be(true);
    }

    [Fact]
    public void BracoDireitoPulso_NaoDeve_Rotacionar_Quando_BracoDireitoCotoveloNaoEstiverFortementeContraido()
    {
        // Arrange
        var expectedRobo = new Robo();
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Act
        var result = _roboServices.EnviarComando("BracoDireito:Pulso:Rotacao45");

        // Assert
        expectedRobo.BracoDireito.Pulso.ToString().Should().Be(Braco.PulsoEstados.Repouso.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("Não é possível rotacionar o pulso se o cotovelo não estiver fortemente contraído.");
    }

    [Fact]
    public void BracoDireitoPulso_NaoDeve_Rotacionar90_Quando_BracoDireitoPulsoEstiverEmRepouso()
    {
        // Arrange
        var expectedRobo = new Robo();
        expectedRobo.BracoDireito.Cotovelo = Braco.CotoveloEstados.FortementeContraido;
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Arrage
        var result = _roboServices.EnviarComando("BracoDireito:Pulso:Rotacao90");

        // Arrage
        expectedRobo.BracoDireito.Pulso.ToString().Should().Be(Braco.PulsoEstados.Repouso.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("O valor fornecido para o estado do pulso não é válido. Valores possíveis: RotacaoMenos45, Rotacao45");
    }

    [Fact]
    public void BracoDireitoPulso_NaoDeve_RotacionarMenos90_Quando_BracoDireitoPulsoEstiverEmRepouso()
    {
        // Arrange
        var expectedRobo = new Robo();
        expectedRobo.BracoDireito.Cotovelo = Braco.CotoveloEstados.FortementeContraido;
        _mockRoboRepository.Setup(r => r.Get()).Returns(expectedRobo);

        // Arrage
        var result = _roboServices.EnviarComando("BracoDireito:Pulso:RotacaoMenos90");

        // Arrage
        expectedRobo.BracoDireito.Pulso.ToString().Should().Be(Braco.PulsoEstados.Repouso.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("O valor fornecido para o estado do pulso não é válido. Valores possíveis: RotacaoMenos45, Rotacao45");
    }
    #endregion
}