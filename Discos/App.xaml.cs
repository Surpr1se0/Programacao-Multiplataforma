using Discos.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Discos
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ModelDiscos M_Discos { get; set; }
        public App()
        {
            M_Discos = new ModelDiscos();
        }
    }
}
