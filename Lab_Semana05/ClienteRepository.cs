using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Semana05
{
    public class ClienteRepository
    {
        private readonly string connectionString;

        public ClienteRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AgregarCliente(Cliente cliente)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            using var cmd = new SqlCommand("USP_AgregarCliente", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idCliente", cliente.IdCliente);
            cmd.Parameters.AddWithValue("@NombreCompañia", cliente.NombreCompañia);
            cmd.Parameters.AddWithValue("@NombreContacto", (object?)cliente.NombreContacto ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CargoContacto", (object?)cliente.CargoContacto ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Direccion", (object?)cliente.Direccion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Ciudad", (object?)cliente.Ciudad ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Region", (object?)cliente.Region ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CodPostal", (object?)cliente.CodPostal ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Pais", (object?)cliente.Pais ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Telefono", (object?)cliente.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Fax", (object?)cliente.Fax ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Enable", true);

            cmd.ExecuteNonQuery();
        }

        public List<Cliente> ObtenerClientes()
        {
            var clientes = new List<Cliente>();

            using var conn = new SqlConnection(connectionString);
            conn.Open();

            using var cmd = new SqlCommand("USP_ListarClientes", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                clientes.Add(new Cliente
                {
                    IdCliente = reader["idCliente"].ToString(),
                    NombreCompañia = reader["NombreCompañia"].ToString(),
                    NombreContacto = reader["NombreContacto"] as string,
                    CargoContacto = reader["CargoContacto"] as string,
                    Direccion = reader["Direccion"] as string,
                    Ciudad = reader["Ciudad"] as string,
                    Region = reader["Region"] as string,
                    CodPostal = reader["CodPostal"] as string,
                    Pais = reader["Pais"] as string,
                    Telefono = reader["Telefono"] as string,
                    Fax = reader["Fax"] as string
                });
            }

            return clientes;
        }

        public void ActualizarCliente(Cliente cliente)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            using var cmd = new SqlCommand("USP_ActualizarCliente", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idCliente", cliente.IdCliente);
            cmd.Parameters.AddWithValue("@NombreCompañia", cliente.NombreCompañia);
            cmd.Parameters.AddWithValue("@NombreContacto", (object?)cliente.NombreContacto ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CargoContacto", (object?)cliente.CargoContacto ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Direccion", (object?)cliente.Direccion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Ciudad", (object?)cliente.Ciudad ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Region", (object?)cliente.Region ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CodPostal", (object?)cliente.CodPostal ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Pais", (object?)cliente.Pais ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Telefono", (object?)cliente.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Fax", (object?)cliente.Fax ?? DBNull.Value);

            cmd.ExecuteNonQuery();
        }

        public void EliminarCliente(string idCliente)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            using var cmd = new SqlCommand("USP_EliminarCliente", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idCliente", idCliente);

            cmd.ExecuteNonQuery();
        }

        public List<Cliente> BuscarClientesPorNombre(string nombreCompania)
        {
            var clientes = new List<Cliente>();

            using var conn = new SqlConnection(connectionString);
            conn.Open();

            using var cmd = new SqlCommand("USP_BuscarClientesPorNombre", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre", nombreCompania);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                clientes.Add(new Cliente
                {
                    IdCliente = reader["idCliente"].ToString(),
                    NombreCompañia = reader["NombreCompañia"].ToString(),
                    NombreContacto = reader["NombreContacto"] as string,
                    CargoContacto = reader["CargoContacto"] as string,
                    Direccion = reader["Direccion"] as string,
                    Ciudad = reader["Ciudad"] as string,
                    Region = reader["Region"] as string,
                    CodPostal = reader["CodPostal"] as string,
                    Pais = reader["Pais"] as string,
                    Telefono = reader["Telefono"] as string,
                    Fax = reader["Fax"] as string
                });
            }

            return clientes;
        }
    }
}