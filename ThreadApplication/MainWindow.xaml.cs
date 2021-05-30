using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using ThreadAppBusiness;

namespace ThreadApplication
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        MainApp _main_app;
        ModalWindow modalWindow;

        delegate void AppEventHandler(object sender, EventArgs e);

        public MainWindow()
        {
            InitializeComponent();
            lblTexto.Content = "Inicio";
            _main_app = new MainApp();            

            // Assina o evento que é disparado pelo app
            _main_app.AppEvent += _main_app_AppEvent;            
        }

        private void _main_app_AppEvent(object sender, EventArgs evt)
        {
            // Verifca se vc está no Dispatcher da thread
            if (!Dispatcher.CheckAccess()) 
            {
                Dispatcher.Invoke(new AppEventHandler(this._main_app_AppEvent), new Object[] { sender, evt });                
                return;
            }

            // Preenche com o nome da thread a cada 2s
            lblTexto.Content = _main_app.ThreadName();
        }

        private void bOK_Click(object sender, RoutedEventArgs e)
        {

            // Lock nos objetos
            if (_main_app.SetLock())
            {
                try
                {             
                    modalWindow = new ModalWindow();
                    modalWindow.ShowDialog();                                                                       
                }
                finally
                {
                    _main_app.ReleaseLock();
                }
            }          
        }
   

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _main_app.AbortThreads();
        }
    }
}
