using System.Resources;
namespace Robo.Tests;
using Xunit;
using Moq;
using Robo.Application.Repositories;
using Robo.Application.Services;
using Robo.Application.Data;
using FluentAssertions;
using Robo.Application.Enums;

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
        var robo = RoboInitializer.CriarInitializedRobo();
        _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

        // Act
        var result = _roboServices.Get();

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void Cabeca_Deve_Rotacionar_Quando_CabecaEstiverInclinadaParaCima()
    {
        // Arrange
        var robo = RoboInitializer.CriarInitializedRobo();
        robo.CabecaInclicacao = CabecaInclinacao.ParaCima;
        _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

        // Act
        var result = _roboServices.RotacionarCabeca("RotacaoMenos45");

        // Assert
        robo.CabecaRotacao.ToString().Should().Be(CabecaRotacao.RotacaoMenos45.ToString());
        result.Success.Should().Be(true);
    }

    [Fact]
    public void Cabeca_Deve_Rotacionar_Quando_CabecaEstiverEmRepouso()
    {
        // Arrange
        var robo = RoboInitializer.CriarInitializedRobo();
        robo.CabecaInclicacao = CabecaInclinacao.EmRepouso;
        _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

        // Act
        var result = _roboServices.RotacionarCabeca("Rotacao45");

        // Assert
        robo.CabecaRotacao.ToString().Should().Be(CabecaRotacao.Rotacao45.ToString());
        result.Success.Should().Be(true);
    }

    [Fact]
    public void Cabeca_NaoDeve_Rotacionar_Quando_CabecaEstiverInclinadaParaBaixo()
    {
        // Arrange
        var robo = RoboInitializer.CriarInitializedRobo();
        robo.CabecaInclicacao = CabecaInclinacao.ParaBaixo;
        _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

        // Act
        var result = _roboServices.RotacionarCabeca("Rotacao45");

        // Assert
        robo.CabecaRotacao.ToString().Should().Be(CabecaRotacao.EmRepouso.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("Não é possível rotacionar a cabeça enquanto a inclinação da cabeça for para baixo");
    }

    [Fact]
    public void Cabeca_Deve_Inclinar_ParaCima_Quando_EstiverEmRepouso()
    {
        // Arrange
        var robo = RoboInitializer.CriarInitializedRobo();
        robo.CabecaInclicacao = CabecaInclinacao.EmRepouso;
        _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

        // Act
        var result = _roboServices.InclinarCabeca("ParaCima");

        // Assert
        robo.CabecaInclicacao.ToString().Should().Be(CabecaInclinacao.ParaCima.ToString());
        result.Success.Should().Be(true);
    }

    [Fact]
    public void Cabeca_Nao_Deve_Inclinar_ParaCima_Quando_EstiverParaBaixo()
    {
        // Arrange
        var robo = RoboInitializer.CriarInitializedRobo();
        robo.CabecaInclicacao = CabecaInclinacao.ParaBaixo;
        _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

        // Act
        var result = _roboServices.InclinarCabeca("ParaCima");

        // Assert
        robo.CabecaInclicacao.ToString().Should().Be(CabecaInclinacao.ParaBaixo.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("O valor fornecido para a inclinação da cabeça não é válido. Valores possíveis: EmRepouso");
    }

    [Fact]
    public void Cabeca_Nao_Deve_InclinarParaBaixo_Quando_EstiverParaCima()
    {
        // Arrange
        var robo = RoboInitializer.CriarInitializedRobo();
        robo.CabecaInclicacao = CabecaInclinacao.ParaCima;
        _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

        // Act
        var result = _roboServices.InclinarCabeca("ParaBaixo");

        // Assert
        robo.CabecaInclicacao.ToString().Should().Be(CabecaInclinacao.ParaCima.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("O valor fornecido para a inclinação da cabeça não é válido. Valores possíveis: EmRepouso");
    }

    #endregion

    #region braco_esquerdo

    [Fact]
    public void BracoEsquerdo_Deve_Rotacionar_Quando_CotoveloEstiverFortementeContraido()
    {
        // Arrage
        var robo = RoboInitializer.CriarInitializedRobo();
        robo.BracoEsquerdoCotoveloContracao = CotoveloContracao.FortementeContraido;
        _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

        // Act
        var result = _roboServices.RatacionarBracoEsquerdo("Rotacao45");

        // Assert
        robo.BracoEsquerdoPulsoRotacao.ToString().Should().Be(PulsoRotacao.Rotacao45.ToString());
        result.Success.Should().Be(true);
    }


    [Fact]
    public void BracoEsquerdo_NaoDeve_Rotacionar_Quando_CotoveloNaoEstiverFortementeContraido()
    {
        // Arrage
        var robo = RoboInitializer.CriarInitializedRobo();
        _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

        // Act
        var result = _roboServices.RatacionarBracoEsquerdo("Rotacao45");

        // Assert
        robo.BracoEsquerdoPulsoRotacao.ToString().Should().Be(PulsoRotacao.EmRepouso.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("Não é possível rotacionar o braço se o cotovelo não estiver fortemente contraído.");
    }

       [Fact]
    public void BracoEsquerdo_NaoDeve_Rotacionar90_Quando_PulsoEstiverEmRepouso()
    {
        // Arrage
        var robo = RoboInitializer.CriarInitializedRobo();
        robo.BracoEsquerdoCotoveloContracao = CotoveloContracao.FortementeContraido;
        _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

        // Arrage
        var result = _roboServices.RatacionarBracoEsquerdo("Rotacao90");

        // Arrage
        robo.BracoEsquerdoPulsoRotacao.ToString().Should().Be(PulsoRotacao.EmRepouso.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("O valor fornecido para a rotação do pulso esquerdo não é válido. Valores possíveis: RotacaoMenos45, Rotacao45");
    }

    [Fact]
    public void BracoEsquerdo_NaoDeve_RotacionarMenos90_Quando_PulsoEstiverEmRepouso()
    {
        // Arrage
        var robo = RoboInitializer.CriarInitializedRobo();
        robo.BracoEsquerdoCotoveloContracao = CotoveloContracao.FortementeContraido;
        _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

        // Arrage
        var result = _roboServices.RatacionarBracoEsquerdo("RotacaoMenos90");

        // Arrage
        robo.BracoEsquerdoPulsoRotacao.ToString().Should().Be(PulsoRotacao.EmRepouso.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("O valor fornecido para a rotação do pulso esquerdo não é válido. Valores possíveis: RotacaoMenos45, Rotacao45");
    }

    #endregion
    #region braco_direito
    [Fact]
    public void BracoDireito_Deve_Rotacionar_Quando_CotoveloEstiverFortementeContraido()
    {
        // Arrage
        var robo = RoboInitializer.CriarInitializedRobo();
        robo.BracoDireitoCotoveloContracao = CotoveloContracao.FortementeContraido;
        _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

        // Act
        var result = _roboServices.RatacionarBracoDireito("Rotacao45");

        // Assert
        robo.BracoDireitoPulsoRotacao.ToString().Should().Be(PulsoRotacao.Rotacao45.ToString());
        result.Success.Should().Be(true);
    }

    [Fact]
    public void BracoDireito_NaoDeve_Rotacionar_Quando_CotoveloNaoEstiverFortementeContraido()
    {
        // Arrage
        var robo = RoboInitializer.CriarInitializedRobo();
        _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

        // Act
        var result = _roboServices.RatacionarBracoDireito("Rotacao45");

        // Assert
        robo.BracoDireitoPulsoRotacao.ToString().Should().Be(PulsoRotacao.EmRepouso.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("Não é possível rotacionar o braço se o cotovelo não estiver fortemente contraído.");
    }

    [Fact]
    public void BracoDireito_NaoDeve_Rotacionar90_Quando_PulsoEstiverEmRepouso()
    {
        // Arrage
        var robo = RoboInitializer.CriarInitializedRobo();
        robo.BracoDireitoCotoveloContracao = CotoveloContracao.FortementeContraido;
        _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

        // Arrage
        var result = _roboServices.RatacionarBracoDireito("Rotacao90");

        // Arrage
        robo.BracoDireitoPulsoRotacao.ToString().Should().Be(PulsoRotacao.EmRepouso.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("O valor fornecido para a rotação do pulso direito não é válido. Valores possíveis: RotacaoMenos45, Rotacao45");
    }

    [Fact]
    public void BracoDireito_NaoDeve_RotacionarMenos90_Quando_PulsoEstiverEmRepouso()
    {
        // Arrage
        var robo = RoboInitializer.CriarInitializedRobo();
        robo.BracoDireitoCotoveloContracao = CotoveloContracao.FortementeContraido;
        _mockRoboRepository.Setup(r => r.Get()).Returns(robo);

        // Arrage
        var result = _roboServices.RatacionarBracoDireito("RotacaoMenos90");

        // Arrage
        robo.BracoDireitoPulsoRotacao.ToString().Should().Be(PulsoRotacao.EmRepouso.ToString());
        result.Success.Should().Be(false);
        result.Message.Should().Be("O valor fornecido para a rotação do pulso direito não é válido. Valores possíveis: RotacaoMenos45, Rotacao45");
    }
    #endregion
}