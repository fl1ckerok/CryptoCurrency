using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CryptoCurrency.Models;
using CryptoCurrency.Services;

namespace CryptoCurrency.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<Cryptocurrency> _cryptoList;
        public ObservableCollection<Cryptocurrency> CryptoList
        {
            get { return _cryptoList; }
            set
            {
                if (_cryptoList != value)
                {
                    _cryptoList = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainViewModel()
        {
            LoadData();
        }

        private async void LoadData()
        {
            var cryptos = await CoinCapApi.ReturnList();
            CryptoList = cryptos.Data;
            foreach (var crypto in CryptoList)
            {
                Console.WriteLine($"Name: {crypto.Name}, Price: {crypto.PriceUsd}");
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
