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
using System.Xml;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para Agregar_con.xaml
    /// </summary>
    public partial class Agregar_con : MetroWindow
    {
        public Agregar_con()
        {
            InitializeComponent();
            llenar_Evento();
            dp_inicio.DisplayDateStart = DateTime.Now;
            dp_termino.DisplayDateStart = DateTime.Now;
            Mostarcontrato();
            reloj();
            restaurarCache();
        }

        private void Btn_volver_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Hide();
        }

        // Llena los eventos
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

        //Muestra todos los contratos en un datagrid
        private void Mostarcontrato()
        {
            Contrato con = new Contrato();
            dtg_contratos.ItemsSource = con.ReadAll();
            dtg_contratos.Items.Refresh();
        }

        //Limpia todos los campos de texto
        private void LimpiarDatos()
        {
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

        //Ingresar contrato
        private async void Bt_guardar_Click(object sender, RoutedEventArgs e)
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

                    Contrato con = new Contrato()
                    {
                        Numero = DateTime.Now.ToString("yyyyMMddHHmm"),
                        RutCliente = txt_rut.Text,
                        IdModalidad = (string)cb_modalidad.SelectedValue,
                        IdTipoEvento = (int)cb_tipo.SelectedValue,
                        Asistentes = int.Parse(txt_Asistentes.Text),
                        PersonalAdicional = int.Parse(txt_Personal.Text),
                        Observaciones = txt_Observaciones.Text,
                        Creacion = DateTime.Now,
                        Realizado = false,
                        FechaHoraInicio = fechaI,
                        FechaHoraTermino = fechaT,
                        ValorTotalContrato = FactoryContrato((int)cb_tipo.SelectedValue)
                    };

                    if (con.Create())
                    {
                        await this.ShowMessageAsync("Contrato Guardado", "corectamente");
                        Mostarcontrato();
                        LimpiarDatos();
                    }

                    else
                    {
                        await this.ShowMessageAsync("Contrato no pudo ser registrado", "Error");
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

        //Cambiar modalidad al seleccionar tipo evento
        private void Cb_tipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            llenar_Modalidad();
        }

        //Limitador de fecha
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

        //Guardar cache
        private Boolean guardarCache()
        {
            try
            {
                CacheContrato mem = new CacheContrato();
                CareTaker.Instance.Memento = mem.crearMemento();
                mem.rutCliente = txt_rut.Text;
                try
                {
                    mem.idEvento = Convert.ToString((int)cb_tipo.SelectedValue);
                }
                catch (Exception) { mem.idEvento = "0"; }
                try
                {
                    mem.idModalidad = (string)cb_modalidad.SelectedValue;
                }
                catch (Exception) { mem.idModalidad = ""; }
                mem.asistentes = txt_Asistentes.Text;
                mem.personal = txt_Personal.Text;
                mem.observaciones = txt_Observaciones.Text;
                mem.fechaInicio = dp_inicio.Text;
                mem.horaInicio = txt_hora_inicio.Text;
                mem.minutoInicio = txt_minuto_inicio.Text;
                mem.fechaFin = dp_termino.Text;
                mem.horaFin = txt_hora_termino.Text;
                mem.minutoFin = txt_minuto_termino.Text;

                GuardarCache.Guardar(mem, "cache.xml");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Cargar cache
        private void restaurarCache()
        {
            try
            {
                XmlTextReader cache = new XmlTextReader("cache.xml");
                if (cache.Read())
                {
                    MessageBoxResult recuperar = MessageBox.Show("¿Quiere recuperar los datos?", "Respaldo encontrado",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (recuperar == MessageBoxResult.Yes)
                    {
                        try
                        {
                            XmlTextReader xml = new XmlTextReader("cache.xml");

                            while (xml.Read())
                            {
                                if (xml.NodeType == XmlNodeType.Element && xml.Name == "rutCliente")
                                {
                                    txt_rut.Text = xml.ReadElementContentAsString();
                                }

                                if (xml.NodeType == XmlNodeType.Element && xml.Name == "idEvento")
                                {
                                    cb_tipo.SelectedValue = xml.ReadElementContentAsInt();
                                    llenar_Modalidad();
                                }

                                if (xml.NodeType == XmlNodeType.Element && xml.Name == "idModalidad")
                                {
                                    cb_modalidad.SelectedValue = xml.ReadElementContentAsString();
                                }

                                if (xml.NodeType == XmlNodeType.Element && xml.Name == "asistentes")
                                {
                                    txt_Asistentes.Text = xml.ReadElementContentAsString();
                                }

                                if (xml.NodeType == XmlNodeType.Element && xml.Name == "personal")
                                {
                                    txt_Personal.Text = xml.ReadElementContentAsString();
                                }

                                if (xml.NodeType == XmlNodeType.Element && xml.Name == "observaciones")
                                {
                                    txt_Observaciones.Text = xml.ReadElementContentAsString();
                                }

                                if (xml.NodeType == XmlNodeType.Element && xml.Name == "fechaInicio")
                                {
                                    dp_inicio.SelectedDate = DateTime.Parse(xml.ReadElementContentAsString());
                                }

                                if (xml.NodeType == XmlNodeType.Element && xml.Name == "horaInicio")
                                {
                                    txt_hora_inicio.Text = xml.ReadElementContentAsString();
                                }

                                if (xml.NodeType == XmlNodeType.Element && xml.Name == "minutoInicio")
                                {
                                    txt_minuto_inicio.Text = xml.ReadElementContentAsString();
                                }

                                if (xml.NodeType == XmlNodeType.Element && xml.Name == "fechaFin")
                                {
                                    dp_termino.SelectedDate = DateTime.Parse(xml.ReadElementContentAsString());
                                }

                                if (xml.NodeType == XmlNodeType.Element && xml.Name == "horaFin")
                                {
                                    txt_hora_termino.Text = xml.ReadElementContentAsString();
                                }

                                if (xml.NodeType == XmlNodeType.Element && xml.Name == "minutoFin")
                                {
                                    txt_minuto_termino.Text = xml.ReadElementContentAsString();
                                }
                            }
                        }
                        catch (Exception) { }
                    }
                }
                            }
            catch(Exception) { }

        }

        //Contador de tiempo
        private async void reloj()
        {
            DispatcherTimer minutos = new DispatcherTimer();

            minutos.Interval = new TimeSpan(0, 05, 00);
            minutos.Tick += async (a, b) =>
            {
                if (guardarCache())
                {
                    txt_respaldo.Text = "Respaldo realizado";
                    await Task.Delay(3000);
                    txt_respaldo.Text = "";
                }
                else
                {
                    txt_respaldo.Text = "Error al guardar respaldo";
                    await Task.Delay(3000);
                    txt_respaldo.Text = "";
                }

            };

            minutos.Start();
        }

        private async void Btn_cache_Click(object sender, RoutedEventArgs e)
        {
            if (guardarCache())
            {
                txt_respaldo.Text = "Respaldo realizado";
                await Task.Delay(3000);
                txt_respaldo.Text = "";
            }
            else
            {
                txt_respaldo.Text = "Error al guardar respaldo";
                await Task.Delay(3000);
                txt_respaldo.Text = "";
            }
        }
    }
}
