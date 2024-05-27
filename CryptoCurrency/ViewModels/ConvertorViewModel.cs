using CryptoCurrency.Models;
using CryptoCurrency.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CryptoCurrency.ViewModels
{
    class ConvertorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _crypto1;
        private string _crypto2;
        private string _amount;
        private string _result;

        public string Crypto1
        {
            get { return _crypto1; }
            set
            {
                _crypto1 = value;
                OnPropertyChanged("Crypto1");
            }
        }
        public string Crypto2
        {
            get { return _crypto2; }
            set
            {
                _crypto2 = value;
                OnPropertyChanged("Crypto2");
            }
        }
        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged("Amount");
            }
        }
        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }

        public ICommand ConvertCommand { get; }
        public ConvertorViewModel()
        {
            ConvertCommand = new RelayCommand(async () => await ConvertButton());
        }

        private double ConvertToDouble_Dot(string value)
        {
            double returnin_number;
            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out returnin_number))
            {
                return returnin_number;
            }
            else return -1;
        }

        private async Task ConvertButton()
        {
            //var crypto1 = Crypto1.Text.ToLower();
            //var crypto2 = Crypto2.Text.ToLower();
            //var amount = Convert.ToDouble(Amount.Text);

            //var crypto_1 = await CoinCapApi.GetCryptoRate(crypto1);
            //var crypto_2 = await CoinCapApi.GetCryptoRate(crypto2);

            //Result.Text = (crypto_1.Rate * amount) / crypto_rate2;
            try
            {
                var ID_crypto1 = Crypto1.ToLower();
                var ID_crypto2 = Crypto2.ToLower();
                var amount = Convert.ToDouble(Amount);

                var coin_1 = await CoinCapApi.GetCryptoRate(ID_crypto1);
                var coin_2 = await CoinCapApi.GetCryptoRate(ID_crypto2);

                var result_string = (ConvertToDouble_Dot(coin_1.Rate) * amount) / ConvertToDouble_Dot(coin_2.Rate);
                Result = result_string.ToString();
            }
            catch
            {
                Result = "ERROR";
            }
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter) => _execute();
    }
}
