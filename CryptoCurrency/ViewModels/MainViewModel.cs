using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CryptoCurrency.Models;

namespace CryptoCurrency.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // TODO: Зробити так, щоб забіндити ліст
        CryptoList Cryptos = CoinCapApi.GetResultFromTask(CoinCapApi.ReturnList());

        private ObservableCollection<Cryptocurrency> _cryptoList;
        public ObservableCollection<Cryptocurrency> CryptoList
        {
            get { return _cryptoList; }
            set
            {
                _cryptoList = value;
                OnPropertyChanged(nameof(CryptoList));
            }
        }

        public MainViewModel()
        {
            CryptoList = Cryptos.Data;
        }

    }
}
