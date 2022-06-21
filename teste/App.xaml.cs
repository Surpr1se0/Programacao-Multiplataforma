using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using teste.Models;

namespace teste
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ModelEdificio M_Edificio { get; set; }

        public App()
        {
            M_Edificio = new ModelEdificio();
        }
    }
}
