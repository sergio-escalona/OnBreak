using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
using Onbreak;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class Agregar_cli : MetroWindow
    {
        public Agregar_cli()
        {
            InitializeComponent();
            llenartipo();
            llenarempresa();
            mostrarClientes();
        }
                       
        private void llenartipo()
        {
            TipoActividad ti_ac = new TipoActividad();
            cbActividad.ItemsSource = ti_ac.ReadAll();

            cbActividad.DisplayMemberPath = "Descripcion";
            cbActividad.SelectedValuePath = "IdActividadEmpresa";

            cbActividad.SelectedIndex = -1;
        }

        private void llenarempresa()
        {
            TipoEmpresa ti_em = new TipoEmpresa();
            cbTipo.ItemsSource = ti_em.ReadAll();

            cbTipo.DisplayMemberPath = "Descripcion";
            cbTipo.SelectedValuePath = "IdTipoEmpresa";

            cbTipo.SelectedIndex = -1;
        }

        
        //Ingreso de cliente
        private async void Btn_guardar_Click(object sender, RoutedEventArgs e)
        {
                //Valida que los campos de texto no esten vacios
                if (txtRUT.Text != String.Empty && txtNombre.Text != String.Empty && txtMail.Text != String.Empty &&
                    txtRazonS.Text != String.Empty && txtDir.Text != String.Empty && txtFono.Text != String.Empty &&
                    cbActividad.Text != String.Empty && cbTipo.Text != String.Empty)
                {

                    Cliente cli = new Cliente()
                    {
                        RutCliente = txtRUT.Text,
                        NombreContacto = txtNombre.Text,
                        MailContacto = txtMail.Text,
                        RazonSocial = txtRazonS.Text,
                        Direccion = txtDir.Text,
                        Telefono = txtFono.Text,
                        IdActividadEmpresa = (int)cbActividad.SelectedValue,
                        IdTipoEmpresa = (int)cbTipo.SelectedValue
                    };

                        if (cli.Create())
                        {
                            mostrarClientes();
                            limpiarClientes();
                            await this.ShowMessageAsync("Cliente registrado ", "Éxitosamente");
                        }

                        else
                        {
                            await this.ShowMessageAsync("Cliente no pudo ser registrado", "Error");
                        }
  
                }

                else
                {
                    await this.ShowMessageAsync("Todos los parámetros son obligatorios.", "complete los parámetros");
                }
               
        }
        
        //Limpia campos de texto
        private void limpiarClientes()
        {
           txtRUT.Text=String.Empty;
           txtNombre.Text = String.Empty;
           txtMail.Text = String.Empty;
           txtRazonS.Text = String.Empty;
           txtDir.Text = String.Empty;
           txtFono.Text = String.Empty;
           cbActividad.Text = String.Empty;
           cbTipo.Text = String.Empty;
        }

        //Muestra todos los clientes en un datagrid
        private void mostrarClientes()
        {
            Cliente cli = new Cliente();
            dtg_clientes.ItemsSource = cli.ReadAll();
            dtg_clientes.Items.Refresh();
        }
                
        private void Btn_volver_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Hide();
        }
    }
}

