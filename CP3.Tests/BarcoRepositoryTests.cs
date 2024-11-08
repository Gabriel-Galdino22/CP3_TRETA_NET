using CP3.Data.AppData;
using CP3.Data.Repositories;
using CP3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CP3.Tests
{
    public class BarcoRepositoryTests
    {
        private readonly DbContextOptions<ApplicationContext> _options;
        private readonly ApplicationContext _context;
        private readonly BarcoRepository _barcoRepository;

        public BarcoRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _context = new ApplicationContext(_options);
            _barcoRepository = new BarcoRepository(_context);
        }

        [Fact]
        public void Adicionar_DeveAdicionarBarco()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 1, Nome = "Barco Teste", Modelo = "Modelo Teste", Ano = 2020, Tamanho = 28.5 };

            // Act
            var result = _barcoRepository.Adicionar(barco);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Barco Teste", result.Nome);
        }

        [Fact]
        public void ObterPorId_DeveRetornarBarco_QuandoIdExistir()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 1, Nome = "Barco Teste", Modelo = "Modelo Teste", Ano = 2020, Tamanho = 28.5 };
            _context.Barco.Add(barco);
            _context.SaveChanges();

            // Act
            var result = _barcoRepository.ObterPorId(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Barco Teste", result.Nome);
        }

        [Fact]
        public void Editar_DeveAtualizarBarco()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 1, Nome = "Barco Teste", Modelo = "Modelo Teste", Ano = 2020, Tamanho = 28.5 };
            _context.Barco.Add(barco);
            _context.SaveChanges();

            barco.Nome = "Barco Editado";

            // Act
            var result = _barcoRepository.Editar(barco);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Barco Editado", result.Nome);
        }

        [Fact]
        public void Remover_DeveRemoverBarco()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 1, Nome = "Barco Teste", Modelo = "Modelo Teste", Ano = 2020, Tamanho = 28.5 };
            _context.Barco.Add(barco);
            _context.SaveChanges();

            // Act
            var result = _barcoRepository.Remover(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
    }
}
