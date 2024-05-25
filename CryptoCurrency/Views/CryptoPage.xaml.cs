using CryptoCurrency.Models;
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
    /// Interaction logic for CryptoPage.xaml
    /// </summary>
    public partial class CryptoPage : UserControl
    {
        public CryptoPage()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is Cryptocurrency selectedCrypto)
            {
                var detailPage = new CryptoPageDetailed
                {
                    DataContext = selectedCrypto
                };
                Content = detailPage;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var assetID = SearchBar.Text.ToString();
            Cryptocurrency crypto;
            try
            {
                crypto = await CoinCapApi.ReturnList(assetID);
            }
            catch 
            {
                SearchBar.Text = $"There is no such a crypto like {assetID}";
                return;
            }
            if (crypto != null)
            {
                var selectedCrypto = crypto;
                var detailPage = new CryptoPageDetailed
                {
                    DataContext = selectedCrypto
                };
                Content = detailPage;
            }
            else
            {
                SearchBar.Text = $"There is no such a crypto like {assetID}";
            }
        }
    }
}
