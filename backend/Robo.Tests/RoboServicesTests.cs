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

    #region cabeça

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

    // #region braco_esquerdo

    // [Fact]
    // public void BracoEsquerdo_Deve_Rotacionar_Quando_CotoveloEstiverFortementeContraido()
    // {
    //     // Arrage
    //     var robo = RoboInitializer.CriarInitializedRobo();
    //     robo.BracoEsquerdoCotoveloContracao = CotoveloContracao.FortementeContraido;
    //     _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

    //     // Act
    //     var result = _roboServices.RatacionarBracoEsquerdo("Rotacao45");

    //     // Assert
    //     robo.BracoEsquerdoPulsoRotacao.ToString().Should().Be(PulsoRotacao.Rotacao45.ToString());
    //     result.Success.Should().Be(true);
    // }


    // [Fact]
    // public void BracoEsquerdo_NaoDeve_Rotacionar_Quando_CotoveloNaoEstiverFortementeContraido()
    // {
    //     // Arrage
    //     var robo = RoboInitializer.CriarInitializedRobo();
    //     _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

    //     // Act
    //     var result = _roboServices.RatacionarBracoEsquerdo("Rotacao45");

    //     // Assert
    //     robo.BracoEsquerdoPulsoRotacao.ToString().Should().Be(PulsoRotacao.EmRepouso.ToString());
    //     result.Success.Should().Be(false);
    //     result.Message.Should().Be("Não é possível rotacionar o braço se o cotovelo não estiver fortemente contraído.");
    // }

    // [Fact]
    // public void BracoEsquerdo_NaoDeve_Rotacionar90_Quando_PulsoEstiverEmRepouso()
    // {
    //     // Arrage
    //     var robo = RoboInitializer.CriarInitializedRobo();
    //     robo.BracoEsquerdoCotoveloContracao = CotoveloContracao.FortementeContraido;
    //     _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

    //     // Arrage
    //     var result = _roboServices.RatacionarBracoEsquerdo("Rotacao90");

    //     // Arrage
    //     robo.BracoEsquerdoPulsoRotacao.ToString().Should().Be(PulsoRotacao.EmRepouso.ToString());
    //     result.Success.Should().Be(false);
    //     result.Message.Should().Be("O valor fornecido para a rotação do pulso esquerdo não é válido. Valores possíveis: RotacaoMenos45, Rotacao45");
    // }

    // [Fact]
    // public void BracoEsquerdo_NaoDeve_RotacionarMenos90_Quando_PulsoEstiverEmRepouso()
    // {
    //     // Arrage
    //     var robo = RoboInitializer.CriarInitializedRobo();
    //     robo.BracoEsquerdoCotoveloContracao = CotoveloContracao.FortementeContraido;
    //     _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

    //     // Arrage
    //     var result = _roboServices.RatacionarBracoEsquerdo("RotacaoMenos90");

    //     // Arrage
    //     robo.BracoEsquerdoPulsoRotacao.ToString().Should().Be(PulsoRotacao.EmRepouso.ToString());
    //     result.Success.Should().Be(false);
    //     result.Message.Should().Be("O valor fornecido para a rotação do pulso esquerdo não é válido. Valores possíveis: RotacaoMenos45, Rotacao45");
    // }

    // #endregion
    // #region braco_direito
    // [Fact]
    // public void BracoDireito_Deve_Rotacionar_Quando_CotoveloEstiverFortementeContraido()
    // {
    //     // Arrage
    //     var robo = RoboInitializer.CriarInitializedRobo();
    //     robo.BracoDireitoCotoveloContracao = CotoveloContracao.FortementeContraido;
    //     _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

    //     // Act
    //     var result = _roboServices.RatacionarBracoDireito("Rotacao45");

    //     // Assert
    //     robo.BracoDireitoPulsoRotacao.ToString().Should().Be(PulsoRotacao.Rotacao45.ToString());
    //     result.Success.Should().Be(true);
    // }

    // [Fact]
    // public void BracoDireito_NaoDeve_Rotacionar_Quando_CotoveloNaoEstiverFortementeContraido()
    // {
    //     // Arrage
    //     var robo = RoboInitializer.CriarInitializedRobo();
    //     _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

    //     // Act
    //     var result = _roboServices.RatacionarBracoDireito("Rotacao45");

    //     // Assert
    //     robo.BracoDireitoPulsoRotacao.ToString().Should().Be(PulsoRotacao.EmRepouso.ToString());
    //     result.Success.Should().Be(false);
    //     result.Message.Should().Be("Não é possível rotacionar o braço se o cotovelo não estiver fortemente contraído.");
    // }

    // [Fact]
    // public void BracoDireito_NaoDeve_Rotacionar90_Quando_PulsoEstiverEmRepouso()
    // {
    //     // Arrage
    //     var robo = RoboInitializer.CriarInitializedRobo();
    //     robo.BracoDireitoCotoveloContracao = CotoveloContracao.FortementeContraido;
    //     _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

    //     // Arrage
    //     var result = _roboServices.RatacionarBracoDireito("Rotacao90");

    //     // Arrage
    //     robo.BracoDireitoPulsoRotacao.ToString().Should().Be(PulsoRotacao.EmRepouso.ToString());
    //     result.Success.Should().Be(false);
    //     result.Message.Should().Be("O valor fornecido para a rotação do pulso direito não é válido. Valores possíveis: RotacaoMenos45, Rotacao45");
    // }

    // [Fact]
    // public void BracoDireito_NaoDeve_RotacionarMenos90_Quando_PulsoEstiverEmRepouso()
    // {
    //     // Arrage
    //     var robo = RoboInitializer.CriarInitializedRobo();
    //     robo.BracoDireitoCotoveloContracao = CotoveloContracao.FortementeContraido;
    //     _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

    //     // Arrage
    //     var result = _roboServices.RatacionarBracoDireito("RotacaoMenos90");

    //     // Arrage
    //     robo.BracoDireitoPulsoRotacao.ToString().Should().Be(PulsoRotacao.EmRepouso.ToString());
    //     result.Success.Should().Be(false);
    //     result.Message.Should().Be("O valor fornecido para a rotação do pulso direito não é válido. Valores possíveis: RotacaoMenos45, Rotacao45");
    // }
    // #endregion
}