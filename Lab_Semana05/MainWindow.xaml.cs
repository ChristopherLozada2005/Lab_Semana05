using Microsoft.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab_Semana05
{
    public partial class MainWindow : Window
    {
        private string connectionString =
            "Data Source=LAB1502-005\\SQLEXPRESS;" +
            "Initial Catalog=Neptuno;" +
            "User ID=userHugo;" +
            "Password=12345678;" +
            "TrustServerCertificate=True";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            var cliente = ObtenerClienteFormulario();
            AgregarCliente(cliente);
            ListarClientes();
            LimpiarFormulario();
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            var cliente = ObtenerClienteFormulario();
            ActualizarCliente(cliente);
            ListarClientes();
            LimpiarFormulario();
        }

        // Evento Eliminar
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtIdCliente.Text))
            {
                EliminarCliente(txtIdCliente.Text);
                ListarClientes();
                LimpiarFormulario();
            }
        }

        private void btnListar_Click(object sender, RoutedEventArgs e)
        {
            ListarClientes();
        }

        private void dgClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClientes.SelectedItem is Cliente cliente)
            {
                txtIdCliente.Text = cliente.IdCliente;
                txtNombreCompañia.Text = cliente.NombreCompañia;
                txtNombreContacto.Text = cliente.NombreContacto;
                txtCargoContacto.Text = cliente.CargoContacto;
                txtDireccion.Text = cliente.Direccion;
                txtCiudad.Text = cliente.Ciudad;
                txtRegion.Text = cliente.Region;
                txtCodPostal.Text = cliente.CodPostal;
                txtPais.Text = cliente.Pais;
                txtTelefono.Text = cliente.Telefono;
                txtFax.Text = cliente.Fax;
            }
        }

        private Cliente ObtenerClienteFormulario()
        {
            return new Cliente
            {
                IdCliente = txtIdCliente.Text,
                NombreCompañia = txtNombreCompañia.Text,
                NombreContacto = txtNombreContacto.Text,
                CargoContacto = txtCargoContacto.Text,
                Direccion = txtDireccion.Text,
                Ciudad = txtCiudad.Text,
                Region = txtRegion.Text,
                CodPostal = txtCodPostal.Text,
                Pais = txtPais.Text,
                Telefono = txtTelefono.Text,
                Fax = txtFax.Text
            };
        }

        private void LimpiarFormulario()
        {
            txtIdCliente.Text = "";
            txtNombreCompañia.Text = "";
            txtNombreContacto.Text = "";
            txtCargoContacto.Text = "";
            txtDireccion.Text = "";
            txtCiudad.Text = "";
            txtRegion.Text = "";
            txtCodPostal.Text = "";
            txtPais.Text = "";
            txtTelefono.Text = "";
            txtFax.Text = "";
        }

        private void ListarClientes()
        {
            dgClientes.ItemsSource = ObtenerClientes();
        }

        private void AgregarCliente(Cliente cliente)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO clientes 
                        (idCliente, NombreCompañia, NombreContacto, CargoContacto, Direccion, Ciudad, Region, CodPostal, Pais, Telefono, Fax)
                        VALUES (@idCliente, @NombreCompañia, @NombreContacto, @CargoContacto, @Direccion, @Ciudad, @Region, @CodPostal, @Pais, @Telefono, @Fax)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar cliente: " + ex.Message);
            }
        }

        private List<Cliente> ObtenerClientes()
        {
            var clientes = new List<Cliente>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM clientes";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener clientes: " + ex.Message);
            }
            return clientes;
        }

        private void ActualizarCliente(Cliente cliente)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE clientes SET 
                        NombreCompañia=@NombreCompañia, NombreContacto=@NombreContacto, CargoContacto=@CargoContacto, 
                        Direccion=@Direccion, Ciudad=@Ciudad, Region=@Region, CodPostal=@CodPostal, 
                        Pais=@Pais, Telefono=@Telefono, Fax=@Fax
                        WHERE idCliente=@idCliente";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar cliente: " + ex.Message);
            }
        }

        private void EliminarCliente(string idCliente)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM clientes WHERE idCliente=@idCliente";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idCliente", idCliente);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar cliente: " + ex.Message);
            }
        }
    }
}