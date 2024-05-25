using CryptoCurrency.ViewModels;
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

namespace CryptoCurrency.Views
{
    /// <summary>
    /// Interaction logic for CryptoPageDetailed.xaml
    /// </summary>
    public partial class CryptoPageDetailed : UserControl
    {
        public CryptoPageDetailed()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.Content = new CryptoPage();
            }
        }
    }
}
