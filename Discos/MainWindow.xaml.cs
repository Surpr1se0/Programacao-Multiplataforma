using Discos.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

            app.M_Discos.EscritaTerminada += M_Discos_EscritaTerminada;

            app.Disco.TextoValido += Disco_TextoValido;

        }

        private void M_Discos_EscritaTerminada()
        {
            MessageBox.Show("Escrito com sucesso!");
        }

        private void Disco_TextoValido(string str)
        {
            LVDiscos.Items.Add(new Disco(){ Id = str});
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
                    naoComprados.Items.Add(di.Autor + "-" + di.Preco);
            }

            TVDiscos.Items.Clear();

            TVDiscos.Items.Add(comprados);
            TVDiscos.Items.Add(naoComprados);


            //Criar Itens da ListView
            foreach(Disco di in app.M_Discos.Dados.Values)
            {
                LVDiscos.Items.Add(new Disco { Id = di.Id, Titulo = di.Titulo, Autor = di.Autor, Ano = di.Ano });
            }
        }

        private void GuardarFicheiro_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Ficheiros de texto|*.txt|Todos os ficheiros|*-*";

            if (dlg.ShowDialog() == true)
                //invocacao de metodos do model
                app.M_Discos.EscritaTexto(dlg.FileName);
        }

        private void GuardarXML_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Ficheiros XML|*.xml|Todos os ficheiros|*-*";

                if (dlg.ShowDialog() == true)
                    //invocacao de metodos do model
                    app.M_Discos.EscritaXML(dlg.FileName);
            }
            catch (FileNotFoundException erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void LerFicheiro_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Ficheiros de texto|*.txt|Todos os ficheiros|*-*";

            if (dlg.ShowDialog() == true)
                //invocacao de metodos do model
                app.M_Discos.LeituraTexto(dlg.FileName);
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
            TVDiscos.Items.Remove(TVDiscos.SelectedItem);
            TVDiscos.ItemsSource = app.M_Discos.Dados;
            TVDiscos.Items.Refresh();
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        { 
            //Exceção Criada para o ID do Disco
            try
            {
                app.Disco.ValidarTexto(tbID.Text);
            }
            catch (TextoInvalidoException erro)
            {
                //Mensagem exibida ao utilizador com a mensagem do erro que ocorreu
                MessageBox.Show(erro.Message);
            }


            string autor = tbArtista.Text;
            string ano = tbAno.Text;
            string nome = tbTitulo.Text;
            string id = tbID.Text;
            string preco = tbPreco.Text;
             
            //LVDiscos.Items.Add(new Disco() { Id= id, Titulo = nome, Autor = autor, Ano = ano});

            Disco newDisco = new Disco(id, nome, autor, ano, preco);
            app.M_Discos.Dados.Add(id, newDisco);

            //-----------------------------------
            TVDiscos.ItemsSource = app.M_Discos.Dados;
            TVDiscos.Items.Refresh();








            //-------------------------------------

            //Adicionar de Volta à treeView mas com o elemento novo
            TreeViewItem comprados = new TreeViewItem();
            comprados.Header = "Comprados";

            TreeViewItem naoComprados = new TreeViewItem();
            naoComprados.Header = "Não Comprados";

            foreach (Disco di in app.M_Discos.Dados.Values)
            {
                if (di.Comprado == true)
                    comprados.Items.Add(di.Autor + "-" + di.Preco);
                else
                    naoComprados.Items.Add(di.Autor + "-" + di.Preco);
            }

            TVDiscos.Items.Clear();

            TVDiscos.Items.Add(comprados);
            TVDiscos.Items.Add(naoComprados);
        }


        private void tbItem_Loaded(object sender, RoutedEventArgs e)
        {
            int count = 0;
            foreach (Disco di in app.M_Discos.Dados.Values)
            {
                count++;
            }

            tbItem.Text = "No. de Discos: " + count;
        }
    }
}
