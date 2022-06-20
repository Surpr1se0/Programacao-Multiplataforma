using _11e12.Models;
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

namespace _11e12
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

            //Subscrição dos métodos
            app.M_Inscricoes.LeituraTerminada += M_Inscricoes_LeituraTerminada;
            app.M_Inscricoes.InscricaoAtualizada += M_Inscricoes_LeituraTerminada;
        }

        private void M_Inscricoes_LeituraTerminada()
        {
            //Criar Itens da TreeView
            TreeViewItem inscritos = new TreeViewItem();
            inscritos.Header = "Inscritos";

            TreeViewItem naoinscritos = new TreeViewItem();
            naoinscritos.Header = "Não Inscritos";

            //dados do Model
            foreach(Alunos al in app.M_Inscricoes.Dados.Values)
            {
                //Colocar no sitio correto da Treeview
                if (al.Inscrito == true)
                    inscritos.Items.Add(al.Numero + "-" + al.Nome + "-" + al.Curso);
                else
                    naoinscritos.Items.Add(al.Numero + "-" + al.Nome + "-" + al.Curso);
            }

            //Apagar dados antigos da treeview
            tvAlunos.Items.Clear();

            //Adicionar Inscritos e nao inscritos à treeview
            tvAlunos.Items.Add(inscritos);
            tvAlunos.Items.Add(naoinscritos);
        }

        private void AbrirTexto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Ficheiros de texto|*.txt|Todos os ficheiros|*-*";

            if (dlg.ShowDialog() == true)
                //invocacao de metodos do model
                app.M_Inscricoes.LeituraFicheiroTxt(dlg.FileName);
        }

        private void AbrirXML_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Ficheiros XML|*.xml|Todos os ficheiros|*-*";

            if (dlg.ShowDialog() == true)
                //invocacao de metodos do model
                app.M_Inscricoes.LeituraXML(dlg.FileName);
        }

        private void GuardarTexto_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Ficheiros de texto|*.txt|Todos os ficheiros|*-*";

            if (dlg.ShowDialog() == true)
                //invocacao de metodos do model
                app.M_Inscricoes.EscritaFicheiroTxt(dlg.FileName);
        }

        private void GuardarXML_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Ficheiros XML|*.xml|Todos os ficheiros|*-*";

            if (dlg.ShowDialog() == true)
                //invocacao de metodos do model
                app.M_Inscricoes.EscritaXML(dlg.FileName);
        }

        private void tvAlunos_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tvAlunos.SelectedValue != null)
            {
                app.M_Inscricoes.AlteraInscricao(tvAlunos.SelectedValue.ToString().Split("-")[0]);
            }
        }
    }
}
