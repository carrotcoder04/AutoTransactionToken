

using AutoTransactionToken.Log;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace AutoTransactionToken
{
    public static class MyData
    {
        private static string fileName = "wallet.txt";
        public static List<SmartWallet> Wallets { get; private set; }
        public static void Init()
        {
            Wallets = new List<SmartWallet>();
            if(!File.Exists(fileName))
            {
                File.Create(fileName).Close();
            }
            string content = File.ReadAllText(fileName);
            string[] parts = content.Split('\n');
            foreach(var part in parts)
            {
                if(string.IsNullOrEmpty(part)) continue;
                try
                {
                    string[] words = Regex.Split(part, "\\s+");
                    StringBuilder keyBuilder = new StringBuilder();
                    for (int i = 0; i < words.Length - 3; i++)
                    {
                        keyBuilder.Append(words[i] + ' ');
                    }
                    keyBuilder.Append(words[words.Length - 3]);
                    int index = Wallets.Count;
                    string key = keyBuilder.ToString();
                    string address = words[words.Length - 2];
                    var wallet = FindWalletByAddress(address);
                    if (wallet != null)
                    {
                        continue;
                    }
                    Wallets.Add(new SmartWallet(index, address, key));
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(ex.ToString());
                }
            }
        }
        public static SmartWallet FindWalletByKey(string key)
        {
            for (int i = 0; i < Wallets.Count; i++)
            {
                if (Wallets[i].SecretKey == key)
                {
                    return Wallets[i];
                }
            }
            return null;
        }
        public static SmartWallet FindWalletByAddress(string address)
        {
            for(int i = 0; i < Wallets.Count; i++)
            {
                if (Wallets[i].Address == address)
                {
                    return Wallets[i];
                }
            }
            return null;
        }
    }
}
