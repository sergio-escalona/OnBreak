using Onbreak;
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
namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : MetroWindow
    {
        public Menu()
        {
            InitializeComponent();
        }

        public static ClienteCollection listaClientes = new ClienteCollection();
        public static ContratoCollection listaContrato = new ContratoCollection();
        public static TipoEventoCollection listaEventos = new TipoEventoCollection();

        public int ModeStyle { get; private set; }

        private void Btn_agregar_Click(object sender, RoutedEventArgs e)
        {
            Agregar_cli agregar = new Agregar_cli();
            agregar.Show();
            this.Hide();
        }

        private void Btn_buscar_Click(object sender, RoutedEventArgs e)
        {
            Buscar_cli buscar = new Buscar_cli();
            buscar.Show();
            this.Hide();
        }

        private void Btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            Login inicio = new Login();
            inicio.Show();
            this.Hide();
        }

        private void Btn_agregar_con_Click(object sender, RoutedEventArgs e)
        {
            Agregar_con agregar_con = new Agregar_con();
            agregar_con.Show();
            this.Hide();
        }

        private void Btn_buscar_con_Click(object sender, RoutedEventArgs e)
        {
            Buscar_con buscar_con = new Buscar_con();
            buscar_con.Show();
            this.Hide();
        }

        private void Alto_contraste1_Click(object sender, RoutedEventArgs e)
        {
            if (ModeStyle == 0)
            {

                ModeStyle = 1;
                MahApps.Metro.ThemeManager.ChangeAppStyle(Application.Current,
                                 MahApps.Metro.ThemeManager.GetAccent("Blue"),
                                 MahApps.Metro.ThemeManager.GetAppTheme("BaseDark"));





            }
            else
            {

                ModeStyle = 0;
                MahApps.Metro.ThemeManager.ChangeAppStyle(Application.Current,
                                    MahApps.Metro.ThemeManager.GetAccent("Blue"),
                                    MahApps.Metro.ThemeManager.GetAppTheme("BaseLight"));

            }
        }
    }
}
