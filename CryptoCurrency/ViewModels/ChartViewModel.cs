using CryptoCurrency.Models;
using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrency.ViewModels
{
    class ChartViewModel : INotifyPropertyChanged
    {
        private Cryptocurrency _selectedCrypto;
        private SeriesCollection _seriesCollection;
        private List<string> _labels;

        public SeriesCollection SeriesCollection
        {
            get { return _seriesCollection; }
            set
            {
                _seriesCollection = value;
                OnPropertyChanged("SeriesCollection");
            }
        }
        public List<string> Labels
        {
            get { return _labels; }
            set
            {
                _labels = value;
                OnPropertyChanged("Labels");
            }
        }
        public Cryptocurrency SelectedCrypto
        {
            get { return _selectedCrypto; }
            set
            {
                _selectedCrypto = value;
                OnPropertyChanged("SelectedCrypto");
            }
        }

        public ChartViewModel(Cryptocurrency selectedCrypto)
        {
            SelectedCrypto = selectedCrypto;
            LoadChartDataAsync(SelectedCrypto);
        }

        private async Task LoadChartDataAsync(Cryptocurrency asset)
        {
            var apiUrl = $"https://api.coincap.io/v2/assets/{asset.Id}/history?interval=d1";
            var prices = new List<double>();
            var dates = new List<DateTime>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<CryptoHistory>(jsonString);

                    foreach (var data in result.Data)
                    {
                        double price;
                        if (double.TryParse(data.PriceUsd, NumberStyles.Any, CultureInfo.InvariantCulture, out price))
                        {
                            prices.Add(price);
                        }
                        else Console.WriteLine("Error, smth bad");
                        dates.Add(DateTime.Parse(data.Date));
                    }

                    var labels = new List<string>();
                    foreach (var date in dates)
                    {
                        labels.Add(date.ToString("d"));
                    }

                    SeriesCollection = new SeriesCollection
                    {
                        new LineSeries
                        {
                            Title = "Price",
                            Values = new ChartValues<double>(prices)
                        }
                    };

                    Labels = labels;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
