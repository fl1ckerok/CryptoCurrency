using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrency.Models
{
    public class CryptoList
    {
        public ObservableCollection<Cryptocurrency> Data { get; }

        public void GetData()
        {
            foreach (var item in Data)
            {
                Console.WriteLine($"#{item.Rank} Name: {item.Name}, Price: {item.PriceUsd} $");
            }
        }
    }
}
