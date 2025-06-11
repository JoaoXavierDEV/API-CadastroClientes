using System;
using System.Collections.Generic;
using XPTO.Application.Dtos;

namespace XPTO.Application.Interfaces
{
    public interface IRegistroCliente
    {
        IEnumerable<Cliente> ObterTodosClientes();
    }
}

