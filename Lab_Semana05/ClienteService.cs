using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Semana05
{
    public class ClienteService
    {
        private readonly ClienteRepository repository;

        public ClienteService(ClienteRepository repository)
        {
            this.repository = repository;
        }

        public void AgregarCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.IdCliente))
                throw new ArgumentException("El ID del cliente es obligatorio.");
            if (string.IsNullOrWhiteSpace(cliente.NombreCompañia))
                throw new ArgumentException("El nombre de la compañía es obligatorio.");

            repository.AgregarCliente(cliente);
        }

        public List<Cliente> ObtenerClientes()
        {
            return repository.ObtenerClientes();
        }

        public void ActualizarCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.IdCliente))
                throw new ArgumentException("El ID del cliente es obligatorio.");
            if (string.IsNullOrWhiteSpace(cliente.NombreCompañia))
                throw new ArgumentException("El nombre de la compañía es obligatorio.");

            repository.ActualizarCliente(cliente);
        }

        public void EliminarCliente(string idCliente)
        {
            if (string.IsNullOrWhiteSpace(idCliente))
                throw new ArgumentException("El ID del cliente es obligatorio.");

            repository.EliminarCliente(idCliente);
        }
    }
}
