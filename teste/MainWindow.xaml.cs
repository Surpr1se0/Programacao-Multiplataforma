using Microsoft.Win32;
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
using teste.Models;

namespace teste
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private App app;
        public MainWindow()
        {
            InitializeComponent();

            //App
            app = App.Current as App;

            //Métodos 
            app.M_Edificio.LeituraTerminada += M_Edificio_LeituraTerminada;
            app.M_Edificio.SalaReservada += M_Edificio_LeituraTerminada;
        }

        private void M_Edificio_LeituraTerminada()
        {
            //Criar Itens da TreeView
            TreeViewItem reservada = new TreeViewItem();
            reservada.Header = "Reservados";

            TreeViewItem naoReservada = new TreeViewItem();
            naoReservada.Header = "Nao Reservada";

            //Dados do Model
            foreach(Sala sal in app.M_Edificio.Salas)
            {
                //Colocar no Sitio Certo 
                if (sal.Ocupada == true)
                    reservada.Items.Add(sal.Id + "-" + sal.Piso + "-" + sal.Capacidade);
                else
                    naoReservada.Items.Add(sal.Id + "-" + sal.Piso + "-" + sal.Capacidade);
            }

            tvSalas.Items.Clear();

            //Adicionar Elementos
            tvSalas.Items.Add(reservada);
            tvSalas.Items.Add(naoReservada);
        }

        private void AbrirXML_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Ficheiros XML|*.xml|Todos os ficheiros|*-*";

            if (dlg.ShowDialog() == true)
                //invocacao de metodos do model
                app.M_Edificio.LeituraXML(dlg.FileName);
        }

        private void GuardarXML_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Ficheiros XML|*.xml|Todos os ficheiros|*-*";

            if (dlg.ShowDialog() == true)
                //invocacao de metodos do model
                app.M_Edificio.EscritaXML(dlg.FileName);
        }

        private void btnReservar_Click(object sender, RoutedEventArgs e)
        {
            if(tvSalas.SelectedItem != null)
            {
                app.M_Edificio.ReservarSala(tvSalas.SelectedValue.ToString().Split("-")[0]);
            }
        }
    }
}
