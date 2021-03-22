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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
using Onbreak;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para Buscar.xaml
    /// </summary>
    public partial class Buscar_cli : MetroWindow
    {
        public Buscar_cli()
        {
            InitializeComponent();
            llenartipo();
            llenarempresa();
            mostrarClientes();
        }

        public enum Actividad
        {
            Agropecuaria,
            Minería,
            Manufactura,
            Comercio,
            Hoteleria,
            Alimentos,
            Transporte,
            Servicios
        }
        //numeracion tipo empresa
        public enum Tipo_Empresa
        {
            SPA,
            EIRL,
            Limitada,
            Sociedad_anonima
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

        //Vacia los campos de texto
        private void limpiarClientes()
        {
            txtRUT.Text = String.Empty;
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

        //Muestra en el datagrid el cliente que corresponda al rut
        private void mostarRut()
        {
            dtg_clientes.ItemsSource = new Cliente().ReadRut(txtRUT.Text);
            dtg_clientes.Items.Refresh();            
        }

        //Muestra en el datagrid los clientes que realicen una actividad en especifico
        private void mostrarActividad()
        {
            dtg_clientes.ItemsSource = new Cliente().ReadActividad((int)cbActividad.SelectedValue);
            dtg_clientes.Items.Refresh();            
        }

        //Muestra en el datagrid los clientes que sea de un tipo de empresa
        private void mostrarTipo()
        {
            dtg_clientes.ItemsSource = new Cliente().ReadEmpresa((int)cbTipo.SelectedValue);
            dtg_clientes.Items.Refresh();
        }

        private void Btn_volver_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Hide();
        }

        //Buscador de clientes
        private async void Btn_buscar_Click(object sender, RoutedEventArgs e)
        {
            // Buscador por rut
            if ((bool)rbRut.IsChecked)
            {
                if (String.IsNullOrEmpty(txtRUT.Text)) //Si el campo esta vacio muestra todos los clientes
                {
                    mostrarClientes();
                }
                else if (txtRUT.Text != String.Empty)
                {
                    try
                    {
                        mostarRut();
                    }
                    catch (Exception)
                    {

                        await this.ShowMessageAsync("Error al buscar cliente", "Error desconocido.");
                       
                        

                    }

                }
                else
                {
                    await this.ShowMessageAsync("Error al ingresar RUT", "Ingrese un rut valido.");
                   
                }
            }

            // Buscador por tipo de actividad
            else if ((bool)rbActividad.IsChecked)
            {
                if (String.IsNullOrEmpty(cbActividad.Text)) //Si el campo esta vacio muestra todos los clientes
                {
                    mostrarClientes();
                }
                else if (cbActividad.Text != String.Empty)
                {
                    try
                    {
                        mostrarActividad();
                    }
                    catch (Exception)
                    {

                        await this.ShowMessageAsync("Error al buscar clientes", "Error desconocido.");
                        

                    }

                }
                else
                {
                    await this.ShowMessageAsync("Error al ingresar Actividad", "Ingrese una actividad");
                    
                }
            }

            //Buscador por tipo empresa
            else if ((bool)rbTipo.IsChecked)
            {
                if (String.IsNullOrEmpty(cbTipo.Text)) //Si el campo esta vacio muestra todos los clientes
                {
                    mostrarClientes();
                }
                else if (cbTipo.Text != String.Empty)
                {
                    try
                    {
                        mostrarTipo();
                    }
                    catch (Exception)
                    {

                        await this.ShowMessageAsync("Error al buscar clientes", "Error desconocido.");
                       


                    }

                }
                else
                {
                    await this.ShowMessageAsync("Error al ingresar Tipo", "Ingrese un tipo.");
                    
                }
            }

            //Error en caso que no se elija un criterio de busqueda
            else
            {
                await this.ShowMessageAsync("Ingrese criterio a buscar", "Error desconocido.");
                
            }

        }

        //Actualizador de datos
        private async void Btn_actualizar_Click(object sender, RoutedEventArgs e)
        {
            //Valida que no hayan campos vacios
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

                if (cli.Update())
                {
                    mostrarClientes();
                    limpiarClientes();
                    await this.ShowMessageAsync("Cliente actualizado ", "Éxitosamente");
                }

                else
                {
                    await this.ShowMessageAsync("Cliente no pudo ser actualizado", "Error");
                }

            }

            else
            {
                await this.ShowMessageAsync("Todos los parámetros son obligatorios.", "Ingrese los parámetros .");
                
            }
        }

        //Eliminador de cliente
        private async void Btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            Contrato con = new Contrato()
            {
                RutCliente = txtRUT.Text
            };
            if (con.ReadRut(txtRUT.Text).Count() == 0) //Verifica que el cliente no tenga contratos
            {
                
                Cliente cli = new Cliente()
                {
                    RutCliente = txtRUT.Text
                };

                MessageBoxResult eliminar = MessageBox.Show("¿Esta seguro de eliminar?", "Confirmar",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (eliminar == MessageBoxResult.Yes)
                {
                    if (cli.Delete())
                    {
                        Menu.listaClientes.Remove(cli);
                        mostrarClientes();
                        limpiarClientes();
                        await this.ShowMessageAsync("Cliente eliminado", "Exitosamente.");
                    }

                }
            }
            else
            {
                await this.ShowMessageAsync("No se puede eliminar cliente", "Cliente presenta contrato(s).");
            }
            
        }

        //Traspasa la información de una fila a los campos de texto al hacer doble click
        private async void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Cliente fila = (Cliente)dtg_clientes.SelectedItem;
                string num_rut = fila.RutCliente.Trim();

                Cliente cli = new Cliente()
                {
                    RutCliente = num_rut
                };

                if (cli.Read())
                {
                    txtRUT.Text = cli.RutCliente;
                    txtNombre.Text = cli.NombreContacto;
                    txtMail.Text = cli.MailContacto;
                    txtRazonS.Text = cli.RazonSocial;
                    txtDir.Text = cli.Direccion;
                    txtFono.Text = cli.Telefono.ToString();
                    cbActividad.SelectedValue = cli.IdActividadEmpresa;
                    cbTipo.SelectedValue = cli.IdTipoEmpresa;
                }
                
            }

            catch (Exception)
            {
                await this.ShowMessageAsync("Error al seleccionar cliente", "Seleccione una fila que no se encuentre vacia");
            }            
        }
    }
}
