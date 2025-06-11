using System;

namespace XPTO.Application.DTOs;


public class EnderecoDTO
{
    /// <summary>
    /// Represents a data transfer object for an address.
    /// </summary>
    public class EnderecoDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the address.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the street name of the address.
        /// </summary>
        public string Rua { get; set; }
        /// <summary>
        /// Gets or sets the house number of the address.
        /// </summary>
        public string Numero { get; set; }
        /// <summary>
        /// Gets or sets the city of the address.
        /// </summary>
        public string Cidade { get; set; }
        /// <summary>
        /// Gets or sets the state of the address.
        /// </summary>
        public string Estado { get; set; }
        /// <summary>
        /// Gets or sets the postal code of the address.
        /// </summary>
        public string Cep { get; set; }
    }
}
