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
    /// Lógica de interacción para Buscar_con.xaml
    /// </summary>
    public partial class Buscar_con : MetroWindow
    {
        public Buscar_con()
        {
            InitializeComponent();
            llenar_Evento();
            llenar_Modalidad();
            MostrarContratos();
            dp_inicio.DisplayDateStart = DateTime.Now;
            dp_termino.DisplayDateStart = DateTime.Now;
        }

        private void Btn_volver_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Hide();
        }

        //Muestra todos los contratos en un datagrid
        private void MostrarContratos()
        {
            Contrato con = new Contrato();
            dtg_contratos.ItemsSource = con.ReadAll();
            dtg_contratos.Items.Refresh();
        }

        //Muestra en el datagrid los contratos que corresponda al rut
        private void BuscarRut()
        {
            dtg_contratos.ItemsSource = new Contrato().ReadRut(txt_rut.Text);
            dtg_contratos.Items.Refresh();           
        }

        //Muestra en el datagrid un contrato en especifico
        private void BuscarContrato()
        {
            dtg_contratos.ItemsSource = new Contrato().ReadNumero(txt_contrato.Text);
            dtg_contratos.Items.Refresh();
        }

        //Muestra en el datagrid los contratos que sea de un tipo de evento
        private void BuscarTipo()
        {
            dtg_contratos.ItemsSource = new Contrato().ReadTipo((int)cb_tipo.SelectedValue);
            dtg_contratos.Items.Refresh();
        }

        //Vacia los campos de texto
        private void LimpiarDatos()
        {
            txt_contrato.Text = String.Empty;
            txt_rut.Text = String.Empty;
            cb_tipo.Text = String.Empty;
            cb_modalidad.Text = String.Empty;
            txt_Asistentes.Text = String.Empty;
            txt_Personal.Text = String.Empty;
            txt_Observaciones.Text = String.Empty;
            dp_inicio.Text = String.Empty;
            dp_termino.Text = String.Empty;
            txt_hora_inicio.Text = String.Empty;
            txt_hora_termino.Text = String.Empty;
            txt_minuto_inicio.Text = String.Empty;
            txt_minuto_termino.Text = String.Empty;
        }

        //numeracion tipo evento

        private void llenar_Evento()
        {
            TipoEvento ti_ev = new TipoEvento();
            cb_tipo.ItemsSource = ti_ev.ReadAll();

            cb_tipo.DisplayMemberPath = "Descripcion";
            cb_tipo.SelectedValuePath = "IdTipoEvento";

            cb_tipo.SelectedIndex = -1;
        }

        // Llena las modalidades
        private void llenar_Modalidad()
        {
            try
            {
                ModalidadServicio ms = new ModalidadServicio();
                cb_modalidad.ItemsSource = ms.ReadTipo((int)cb_tipo.SelectedValue);

                cb_modalidad.DisplayMemberPath = "Nombre";
                cb_modalidad.SelectedValuePath = "IdModalidad";

                cb_modalidad.SelectedIndex = -1;
            }
            catch (Exception ex) { }
        }

        //Muestra la razon social del cliente al ingresar su rut
        private void Txt_rut_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Cliente cli = new Cliente()
                {
                    RutCliente = txt_rut.Text
                };

                if (cli.Read())
                {
                    txt_razon.Text = cli.RazonSocial;
                }

                else
                {
                    txt_razon.Text = String.Empty;
                }

            }
            catch (Exception)
            {
                txt_razon.Text = String.Empty;
            }
        }

        //Calculador de valor de contrato
        private double FactoryContrato(int id_evento)
        {
            CalculoContrato calculo;
            try
            {
                if (id_evento == 10)
                {
                    calculo = new CalculoContrato(new Coffee_break());
                    return calculo.ValorizaPago(double.Parse(txt_Asistentes.Text), double.Parse(txt_Personal.Text), (string)cb_modalidad.SelectedValue);
                }

                else if (id_evento == 20)
                {
                    calculo = new CalculoContrato(new Cocktail());
                    return calculo.ValorizaPago(double.Parse(txt_Asistentes.Text), double.Parse(txt_Personal.Text), (string)cb_modalidad.SelectedValue);
                }

                else if (id_evento == 30)
                {
                    calculo = new CalculoContrato(new Cena());
                    return calculo.ValorizaPago(double.Parse(txt_Asistentes.Text), double.Parse(txt_Personal.Text), (string)cb_modalidad.SelectedValue);
                }

                else { return 0; }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Buscador de contratos
        private async void btn_buscar_Click(object sender, RoutedEventArgs e)
        {
            // Buscador por rut
            if ((bool)rbRut.IsChecked)
            {
                if (String.IsNullOrEmpty(txt_rut.Text)) //Si el campo esta vacio muestra todos los contratos
                {

                    MostrarContratos();
                }
                else if (txt_rut.Text != String.Empty)
                {
                    try
                    {
                        BuscarRut();
                    }
                    catch (Exception)
                    {

                        await this.ShowMessageAsync("Error al buscar Contrato", "Error desconocido.");



                    }

                }
                else
                {
                    await this.ShowMessageAsync("Error al ingresar RUT", "Ingrese un rut valido.");

                }
            }

            // Buscador por numero de contrato
            else if ((bool)rbContrato.IsChecked)
            {
                if (String.IsNullOrEmpty(txt_contrato.Text))
                {
                    MostrarContratos();
                }
                else if (txt_contrato.Text != String.Empty) //Si el campo esta vacio muestra todos los contratos
                {
                    try
                    {
                        BuscarContrato();
                    }
                    catch (Exception)
                    {

                        await this.ShowMessageAsync("Error al buscar contratos", "Error desconocido.");


                    }

                }
                else
                {
                    await this.ShowMessageAsync("Error al ingresar contrato", "Ingrese una actividad");

                }
            }

            //Buscador por tipo evento
            else if ((bool)rbTipo.IsChecked)
            {
                if (String.IsNullOrEmpty(cb_tipo.Text)) //Si el campo esta vacio muestra todos los contratos
                {
                    MostrarContratos();
                }
                else if (cb_tipo.Text != String.Empty)
                {
                    try
                    {
                        BuscarTipo();
                    }
                    catch (Exception)
                    {

                        await this.ShowMessageAsync("Error al buscar contrato", "Error desconocido.");



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
        private async void btn_actualizar_Click(object sender, RoutedEventArgs e)
        {
            int horaI = 0, minutoI = 0, horaT = 0, minutoT = 0;
            DateTime fechaI, fechaT;

            // Valida que hora y minutos sean valores reales
            if (txt_hora_inicio.Text != String.Empty && txt_minuto_inicio.Text != String.Empty &&
                txt_hora_termino.Text != String.Empty && txt_minuto_termino.Text != String.Empty &&
                int.Parse(txt_hora_inicio.Text) >= 0 && int.Parse(txt_hora_inicio.Text) <= 23 &&
                int.Parse(txt_minuto_inicio.Text) >= 0 && int.Parse(txt_minuto_inicio.Text) <= 59 &&
                int.Parse(txt_hora_termino.Text) >= 0 && int.Parse(txt_hora_termino.Text) <= 23 &&
                int.Parse(txt_minuto_termino.Text) >= 0 && int.Parse(txt_minuto_termino.Text) <= 59)
            {
                //Valida que los campos de texto no esten vacios
                if (txt_razon.Text != String.Empty && cb_tipo.Text != String.Empty &&
                    txt_Asistentes.Text != String.Empty && txt_Personal.Text != String.Empty && txt_Observaciones.Text != String.Empty &&
                    txt_Observaciones.Text != String.Empty && txt_hora_inicio.Text != String.Empty && txt_minuto_inicio.Text != String.Empty &&
                    dp_inicio.Text != String.Empty && txt_hora_termino.Text != String.Empty && txt_minuto_termino.Text != String.Empty &&
                    dp_termino.Text != String.Empty)
                {

                    horaI = int.Parse(txt_hora_inicio.Text);
                    minutoI = int.Parse(txt_minuto_inicio.Text);
                    fechaI = DateTime.Parse(dp_inicio.Text);
                    fechaI = fechaI.AddHours(horaI).AddMinutes(minutoI);
                    horaT = int.Parse(txt_hora_termino.Text);
                    minutoT = int.Parse(txt_minuto_termino.Text);
                    fechaT = DateTime.Parse(dp_termino.Text);
                    fechaT = fechaT.AddHours(horaT).AddMinutes(minutoT);
                    double cbo_index = (int)cb_tipo.SelectedValue;
                    TipoEvento te = new TipoEvento();

                    Contrato cnt = new Contrato()
                    {
                        Numero = txt_contrato.Text
                    };
                    if (cnt.Read())
                    {
                        Contrato con = new Contrato()
                        {
                            Numero = txt_contrato.Text,
                            RutCliente = txt_rut.Text,
                            IdModalidad = (string)cb_modalidad.SelectedValue,
                            IdTipoEvento = (int)cb_tipo.SelectedValue,
                            Asistentes = int.Parse(txt_Asistentes.Text),
                            PersonalAdicional = int.Parse(txt_Personal.Text),
                            Observaciones = txt_Observaciones.Text,
                            Creacion = cnt.Creacion,
                            Realizado = cnt.Realizado,
                            Termino = cnt.Termino,
                            FechaHoraInicio = fechaI,
                            FechaHoraTermino = fechaT,
                            ValorTotalContrato = FactoryContrato((int)cb_tipo.SelectedValue)
                        };

                        if (con.Update())
                        {
                            await this.ShowMessageAsync("Contrato Actualizado", "corectamente");
                            MostrarContratos();
                            LimpiarDatos();
                        }

                        else
                        {
                            await this.ShowMessageAsync("Contrato no pudo ser actualizado", "Error");
                        }
                    }
                        

                }

                else
                {
                    await this.ShowMessageAsync("Todos los parámetros son obligatorios.", "Error");
                }
            }

            else
            {
                await this.ShowMessageAsync("Error al ingresar hora y minuto", "Hora debe estar entre 00 y 23, minuto entre 00 y 59.");
            }
            
        }

        //Cierre de contrato
        private async void btn_terminar_Click(object sender, RoutedEventArgs e)
        {
            
                

            MessageBoxResult terminar = MessageBox.Show("¿Esta seguro de terminar el contrato?", "Confirmar",
            MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (terminar == MessageBoxResult.Yes)
            {
                Contrato cnt = new Contrato()
                {
                    Numero = txt_contrato.Text
                };
                if (cnt.Read())
                {
                    Contrato con = new Contrato
                    {
                        Numero = txt_contrato.Text,
                        Creacion = cnt.Creacion,
                        Termino = DateTime.Now,
                        RutCliente = cnt.RutCliente,
                        IdModalidad = cnt.IdModalidad,
                        IdTipoEvento = cnt.IdTipoEvento,
                        FechaHoraInicio = cnt.FechaHoraInicio,
                        FechaHoraTermino = cnt.FechaHoraTermino,
                        Asistentes = cnt.Asistentes,
                        PersonalAdicional = cnt.PersonalAdicional,
                        Realizado = true,
                        ValorTotalContrato = cnt.ValorTotalContrato,
                        Observaciones = cnt.Observaciones
                    };
                    if (con.Update())
                    {
                        MostrarContratos();
                        LimpiarDatos();
                        await this.ShowMessageAsync("Contrato Terminado", "Exitosamente.");
                    }

                    else
                    {
                        await this.ShowMessageAsync("Contrato no pudo ser actualizado", "Error");
                    }
                }

                    
                    
            }
            
            
        }

        //Traspasa la información de una fila a los campos de texto al hacer doble click
        private async void Row_DoubleClick (object sender, MouseButtonEventArgs e)
        {
            try
            {
                Contrato fila = (Contrato)dtg_contratos.SelectedItem;
                string num_contrato = fila.Numero.Trim();

                Contrato con = new Contrato()
                {
                    Numero = num_contrato
                };

                if (con.Read())
                {
                    txt_contrato.Text = con.Numero;
                    txt_rut.Text = con.RutCliente;
                    cb_tipo.SelectedValue = con.IdTipoEvento;
                    llenar_Modalidad();
                    cb_modalidad.SelectedValue = con.IdModalidad;
                    txt_Asistentes.Text = con.Asistentes.ToString();
                    txt_Personal.Text = con.PersonalAdicional.ToString();
                    txt_Observaciones.Text = con.Observaciones;
                    dp_inicio.SelectedDate = con.FechaHoraInicio;
                    txt_hora_inicio.Text = con.FechaHoraInicio.ToString("HH");
                    txt_minuto_inicio.Text = con.FechaHoraInicio.ToString("mm");
                    dp_termino.SelectedDate = con.FechaHoraTermino;
                    txt_hora_termino.Text = con.FechaHoraTermino.ToString("HH");
                    txt_minuto_termino.Text = con.FechaHoraTermino.ToString("mm");
                }
                
            }

            catch (Exception)
            {
                await this.ShowMessageAsync("Error al seleccionar contrato", "Seleccione una fila que no se encuentre vacia");
            }
        }

        private void Cb_tipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            llenar_Modalidad();
        }

        private void Dp_inicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                dp_termino.Text = String.Empty;
                if ((int)cb_tipo.SelectedValue == 30)
                {
                    dp_termino.DisplayDateStart = DateTime.Parse(dp_inicio.Text);
                    dp_termino.DisplayDateEnd = DateTime.Parse(dp_inicio.Text).AddDays(1);
                }

                else
                {
                    dp_termino.DisplayDateStart = DateTime.Parse(dp_inicio.Text);
                    dp_termino.DisplayDateEnd = DateTime.Parse(dp_inicio.Text);
                }
            }
            catch (Exception ex)
            {
                dp_inicio.Text = String.Empty;
            }
        }
    }
 }

