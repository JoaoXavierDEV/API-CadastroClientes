using XPTO.Domain.Entities;

namespace XPTO.Domain.Tests
{
    public class ClienteTest
    {
        [Fact(DisplayName = "Cliente V�lido")]
        public void CriarCliente_ComPropriedadesValidas()
        {
            // Arrange 
            var cliente = new Cliente(
                "Jo�o Fernando",
                "joao@outlook.com",
                "21 9-9999-9999");

            // Act
            var result = cliente.EhValido();

            // Assert 
            Assert.True(result);

            Assert.Empty(cliente.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Cliente com Nome Inv�lido")]
        [Trait("Categoria", "Cliente Trait Testes")]
        public void CriarCliente_NomeInvalido()
        {
            // Arrange
            var cliente = new Cliente(
                "",
                "joao@outlook.com",
                "21 9-9999-9999");

            // Act
            var result = cliente.EhValido();

            // Assert 
            Assert.False(result);

            Assert.NotEmpty(cliente.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Cliente com Email Inv�lido")]
        [Trait("Categoria", "Cliente Trait Testes")]
        public void CriarCliente_EmailInvalido()
        {
            // Arrange
            var cliente = new Cliente(
                "Jo�o Fernando",
                string.Empty,
                "21 9-9999-9999");

            // Act
            var result = cliente.EhValido();

            // Assert 
            Assert.False(result);

            Assert.NotEmpty(cliente.ValidationResult.Errors);

            Assert.Equal(3, cliente.ValidationResult.Errors.Count);

            // Todos os testes abaixo devem passar
            Assert.Collection(cliente.ValidationResult.Errors,
                x => Assert.Equal("O email � obrigat�rio.", x.ErrorMessage),
                x => Assert.Equal("O email deve ser um endere�o de email v�lido.", x.ErrorMessage),
                x => Assert.Equal("O email deve ter entre 1 e 50 caracteres.", x.ErrorMessage));

        }

        [Fact(DisplayName = "Cliente com Telefone Inv�lido")]
        [Trait("Categoria", "Cliente Trait Testes")]
        public void CriarCliente_TelefoneInvalido()
        {
            // Arrange
            var cliente = new Cliente(
                "Jo�o Fernando",
                "joao@outlook",
                "");

            // Act
            var result = cliente.EhValido();

            // Assert 
            Assert.False(result);

            Assert.NotEmpty(cliente.ValidationResult.Errors);
        }

    }
}