using CP3.Application.Services;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;
using Moq;
using Xunit;

namespace CP3.Tests
{
    public class BarcoApplicationServiceTests
    {
        private readonly Mock<IBarcoRepository> _repositoryMock;
        private readonly BarcoApplicationService _barcoService;

        public BarcoApplicationServiceTests()
        {
            _repositoryMock = new Mock<IBarcoRepository>();
            _barcoService = new BarcoApplicationService(_repositoryMock.Object);
        }

        [Fact]
        public void ObterTodosBarcos_DeveRetornarListaDeBarcos()
        {
            // Arrange
            var barcos = new List<BarcoEntity>
            {
                new BarcoEntity { Id = 1, Nome = "Barco A", Modelo = "Modelo A", Ano = 2020, Tamanho = 30.5 },
                new BarcoEntity { Id = 2, Nome = "Barco B", Modelo = "Modelo B", Ano = 2021, Tamanho = 25.0 }
            };
            _repositoryMock.Setup(r => r.ObterTodos()).Returns(barcos);

            // Act
            var result = _barcoService.ObterTodosBarcos();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void ObterBarcoPorId_DeveRetornarBarco_QuandoIdExistir()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 1, Nome = "Barco A", Modelo = "Modelo A", Ano = 2020, Tamanho = 30.5 };
            _repositoryMock.Setup(r => r.ObterPorId(1)).Returns(barco);

            // Act
            var result = _barcoService.ObterBarcoPorId(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Barco A", result.Nome);
        }

        [Fact]
        public void AdicionarBarco_DeveAdicionarENaoRetornarNulo()
        {
            // Arrange
            var dto = new Mock<IBarcoDto>();
            dto.Setup(d => d.Nome).Returns("Barco A");
            dto.Setup(d => d.Modelo).Returns("Modelo A");
            dto.Setup(d => d.Ano).Returns(2020);
            dto.Setup(d => d.Tamanho).Returns(30.5);

            var barco = new BarcoEntity { Nome = "Barco A", Modelo = "Modelo A", Ano = 2020, Tamanho = 30.5 };
            _repositoryMock.Setup(r => r.Adicionar(It.IsAny<BarcoEntity>())).Returns(barco);

            // Act
            var result = _barcoService.AdicionarBarco(dto.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Barco A", result.Nome);
        }

        [Fact]
        public void EditarBarco_DeveAtualizarDados_QuandoIdExistir()
        {
            // Arrange
            var dto = new Mock<IBarcoDto>();
            dto.Setup(d => d.Nome).Returns("Barco Editado");
            dto.Setup(d => d.Modelo).Returns("Modelo Editado");
            dto.Setup(d => d.Ano).Returns(2021);
            dto.Setup(d => d.Tamanho).Returns(35.0);

            var barco = new BarcoEntity { Id = 1, Nome = "Barco A", Modelo = "Modelo A", Ano = 2020, Tamanho = 30.5 };
            _repositoryMock.Setup(r => r.ObterPorId(1)).Returns(barco);
            _repositoryMock.Setup(r => r.Editar(It.IsAny<BarcoEntity>())).Returns(barco);

            // Act
            var result = _barcoService.EditarBarco(1, dto.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Barco Editado", result.Nome);
        }

        [Fact]
        public void RemoverBarco_DeveRemoverBarco_QuandoIdExistir()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 1, Nome = "Barco A", Modelo = "Modelo A", Ano = 2020, Tamanho = 30.5 };
            _repositoryMock.Setup(r => r.ObterPorId(1)).Returns(barco);
            _repositoryMock.Setup(r => r.Remover(1)).Returns(barco);

            // Act
            var result = _barcoService.RemoverBarco(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
    }
}
