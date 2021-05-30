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

namespace ThreadApplication
{
    /// <summary>
    /// Lógica interna para ModalWindow.xaml
    /// </summary>
    public partial class ModalWindow : Window
    {
        public ModalWindow()
        {
            InitializeComponent();
        }

        private void bFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();            
        }

        private void bOK_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Clicou OK!");
        }
    }
}
