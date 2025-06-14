using XPTO.Domain.Entities;

namespace XPTO.Domain.Tests
{
    public class ClienteTest
    {
        [Fact(DisplayName = "Cliente Válido")]
        public void CriarCliente_ComPropriedadesValidas()
        {
            // Arrange 
            var cliente = new Cliente(
                "João Fernando",
                "joao@outlook.com",
                "21 9-9999-9999");

            // Act
            var result = cliente.EhValido();

            // Assert 
            Assert.True(result);

            Assert.Empty(cliente.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Cliente com Nome Inválido")]
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

        [Fact(DisplayName = "Cliente com Email Inválido")]
        [Trait("Categoria", "Cliente Trait Testes")]
        public void CriarCliente_EmailInvalido()
        {
            // Arrange
            var cliente = new Cliente(
                "João Fernando",
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
                x => Assert.Equal("O email é obrigatório.", x.ErrorMessage),
                x => Assert.Equal("O email deve ser um endereço de email válido.", x.ErrorMessage),
                x => Assert.Equal("O email deve ter entre 1 e 50 caracteres.", x.ErrorMessage));

        }

        [Fact(DisplayName = "Cliente com Telefone Inválido")]
        [Trait("Categoria", "Cliente Trait Testes")]
        public void CriarCliente_TelefoneInvalido()
        {
            // Arrange
            var cliente = new Cliente(
                "João Fernando",
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