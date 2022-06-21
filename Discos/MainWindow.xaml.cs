using Discos.Models;
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

namespace Discos
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

            //Instância da App
            app = App.Current as App;

            //Metodos
            app.M_Discos.LeituraTerminada += M_Discos_LeituraTerminada;
            app.M_Discos.DiscoComprado += M_Discos_LeituraTerminada;
        }

        private void M_Discos_LeituraTerminada()
        {
            //Criar Itens na Treeview
            TreeViewItem comprados = new TreeViewItem();
            comprados.Header = "Comprados";

            TreeViewItem naoComprados = new TreeViewItem();
            naoComprados.Header = "Não Comprados";

            foreach(Disco di in app.M_Discos.Dados.Values)
            {
                if (di.Comprado == true)
                    comprados.Items.Add(di.Autor + "-" + di.Preco);
                if (di.Comprado == false)
                    comprados.Items.Add(di.Autor + "-" + di.Preco);
            }

            TVDiscos.Items.Clear();

            TVDiscos.Items.Add(comprados);
            TVDiscos.Items.Add(naoComprados);

            //Criar Itens da ListView
            foreach(Disco di in app.M_Discos.Dados.Values)
            {
                LVDiscos.Items.Add(new Disco { Id = di.Id, Titulo = di.Titulo, Autor = di.Autor, Ano = di.Ano });
            }
            LVDiscos.Items.Clear(); //adicionado
        }

        private void GuardarFicheiro_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void GuardarXML_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Ficheiros XML|*.xml|Todos os ficheiros|*-*";

            if (dlg.ShowDialog() == true)
                //invocacao de metodos do model
                app.M_Discos.EscritaXML(dlg.FileName);
        }

        private void LerFicheiro_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LerXML_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Ficheiros XML|*.xml|Todos os ficheiros|*-*";

            if (dlg.ShowDialog() == true)
                //invocacao de metodos do model
                app.M_Discos.LeituraXML(dlg.FileName);
        }

        private void SobreNos_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Programador: Francisco Gouveia", "Sobre a aplicação", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
