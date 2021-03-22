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
using MahApps.Metro.IconPacks;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        private int ModeStyle = 0;
        public Login()
        {
            InitializeComponent();
            MahApps.Metro.ThemeManager.ChangeAppStyle(Application.Current,
                                              MahApps.Metro.ThemeManager.GetAccent("Blue"),
                                              MahApps.Metro.ThemeManager.GetAppTheme("BaseLight"));

        }
         

        private async void Btn_iniciar_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();


            if((txtUsuario.Text=="admin") && (pbPass.Password == "admin"))
            {
                
                await this.ShowMessageAsync("Bienvenido", "Usuario "+ txtUsuario.Text + "istrador");
                menu.Show();
                this.Hide();
            }
            else
            {
                await this.ShowMessageAsync("Error", "datos incorrectos");
            }

            
        }

        private void Btn_salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TxtPass_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
    
        }

        
        //alto contraste
        private void Contraste_Click(object sender, RoutedEventArgs e)
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