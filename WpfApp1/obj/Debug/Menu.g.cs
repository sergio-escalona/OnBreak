#pragma checksum "..\..\Menu.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CDF1DF0FB321CB4B807C940FAC5AB29794A86E6C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.Behaviours;
using MahApps.Metro.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfApp1;


namespace WpfApp1 {
    
    
    /// <summary>
    /// Menu
    /// </summary>
    public partial class Menu : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\Menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_agregar;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_buscar;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_cerrar;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\Menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_agregar_con;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\Menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_buscar_con;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\Menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button alto_contraste1;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/menu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Menu.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btn_agregar = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\Menu.xaml"
            this.btn_agregar.Click += new System.Windows.RoutedEventHandler(this.Btn_agregar_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btn_buscar = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\Menu.xaml"
            this.btn_buscar.Click += new System.Windows.RoutedEventHandler(this.Btn_buscar_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btn_cerrar = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\Menu.xaml"
            this.btn_cerrar.Click += new System.Windows.RoutedEventHandler(this.Btn_cerrar_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btn_agregar_con = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\Menu.xaml"
            this.btn_agregar_con.Click += new System.Windows.RoutedEventHandler(this.Btn_agregar_con_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btn_buscar_con = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\Menu.xaml"
            this.btn_buscar_con.Click += new System.Windows.RoutedEventHandler(this.Btn_buscar_con_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.alto_contraste1 = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\Menu.xaml"
            this.alto_contraste1.Click += new System.Windows.RoutedEventHandler(this.Alto_contraste1_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

