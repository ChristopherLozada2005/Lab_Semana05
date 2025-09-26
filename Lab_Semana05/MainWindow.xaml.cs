using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Lab_Semana05
{
    public partial class MainWindow : Window
    {
            private readonly ClienteService clienteService;

        public MainWindow()
        {
            InitializeComponent();
            var repository = new ClienteRepository(
                "Data Source=LAB1502-005\\SQLEXPRESS;" +
                "Initial Catalog=Neptuno;" +
                "User ID=userHugo;" +
                "Password=12345678;" +
                "TrustServerCertificate=True"
            );
            clienteService = new ClienteService(repository);
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cliente = ObtenerClienteFormulario();
                clienteService.AgregarCliente(cliente);
                ListarClientes();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cliente = ObtenerClienteFormulario();
                clienteService.ActualizarCliente(cliente);
                ListarClientes();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtIdCliente.Text))
                {
                    clienteService.EliminarCliente(txtIdCliente.Text);
                    ListarClientes();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtBuscar.Text.Trim();
            dgClientes.ItemsSource = clienteService.BuscarClientesPorNombre(nombre);
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
            dgClientes.ItemsSource = clienteService.ObtenerClientes();
        }
    }
}