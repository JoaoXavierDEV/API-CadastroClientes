using System.Text;
using XPTO.Domain.Entities;

namespace XPTO.Domain.Tests
{
    [CollectionDefinition("DomainCollection")]
    [Collection("DomainCollection")]
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

        [Fact(DisplayName = "Inverter String usando For Decrescente")]
        public void InverterString_ValidarResultado()
        {
            // Arrange
            var original = "XPTO";

            var esperado = "OTPX";
            // Act
            var resultado = new string(original.Reverse().ToArray());

            var novaString = new StringBuilder();



            for (int i = original.Length - 1; i >= 0; i--)
            {
                novaString.Append(original[i]);
            }

            esperado = novaString.ToString();

            // Assert
            Assert.Equal(esperado, resultado);
        }

        [Fact(DisplayName = "Inverter string recursivamente")]
        public void InverterString_Recursivo()
        {
            static string InverterRecursivo(string s)
            {
                if (string.IsNullOrEmpty(s)) return s;
                var novaString = s.Substring(1) + s[0];
                return InverterRecursivo(s.Substring(1)) + s[0];
            }

            string original = "XPTO";
            var esperado = "OTPX";

            string resultado = InverterRecursivo(original);

            Assert.Equal(esperado, resultado);
        }


    }
}