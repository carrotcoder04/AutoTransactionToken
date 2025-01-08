using AutoTransactionToken.Log;
using AutoTransactionToken.Simulator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AutoTransactionToken.AppController
{
    public partial class Controller
    {
        private async Task UpdateWallet()
        {
            foreach(var wallet in Wallets)
            {
                wallet.Update();
            }
            while (true)
            {
                foreach (var wallet in Wallets)
                {
                    await wallet.Update();
                }
                await Task.Delay(3000);
            }
        }
        private async Task Login(LDClient client, SmartWallet wallet)
        {
            await Task.Delay(200);
            client.ClickPercent(83f,95f);
            await Task.Delay(400);
            client.ClickPercent(70f, 20f);
            await Task.Delay(600);
            client.ClickPercent(75.5f, 24.7f);
            await Task.Delay(300);
            client.ClickPercent(75.5f, 24.7f);
            await Task.Delay(500);
            client.ClickPercent(50f, 40f);
            await Task.Delay(200);
            string text = wallet.SecretKey;
            text += " ";
            for (int i = 0; i < text.Length; i++)
            {
                client.InputText(text[i].ToString());
            }
            await Task.Delay(500);
            client.ClickPercent(50.4f, 91.7f);
            await Task.Delay(700);
            await EnterPassWord(client);
            await Task.Delay(500);
            bool flag = false;
            do
            {
                client.ClickPercent(48.7f, 86.4f);
                flag = !client.DumpAndCheckKey("Next");
                await Task.Delay(200);
            }
            while (!flag);
            flag = false;
            do
            {
                string xml = client.DumpXML();
                if(xml.Contains(SUCCESS))
                {
                    flag = true;
                    client.ClickPercent(15f, 95f);
                }
                else if(xml.Contains("Oops"))
                {
                    flag = true;
                    client.ClickPercent(15f, 95f);
                }

            }
            while (!flag);
            await Task.Delay(500);
            await GoSmartHome(client);
        }
        private async Task EnterPassWord(LDClient client)
        {
            bool flag = false;
            do
            {
                await Task.Delay(400);
                client.ClickPercent(24.3f, 58.7f);
                client.ClickPercent(24.3f, 58.7f);
                client.ClickPercent(24.3f, 58.7f);
                client.ClickPercent(24.3f, 58.7f);
                client.ClickPercent(24.3f, 58.7f);
                client.ClickPercent(24.3f, 58.7f);
                await Task.Delay(400);
                if (!client.DumpAndCheckKey("Enter your PIN"))
                {
                    flag = true;
                }
            }
            while (!flag);

        }
        private async Task GoSmartHome(LDClient client)
        {
            await Task.Delay(300);
            client.ClickPercent(14.2f, 95.8f);
            await Task.Delay(300);
        }
        private List<SmartWallet>[] SplitTask()
        {
            int tab = Config.LDTab;
            List<SmartWallet>[] splitTask = new List<SmartWallet>[tab];
            int range = Wallets.Count / tab;
            for (int i = 0; i < tab; i++)
            {
                int start = i * range;
                int end = (i + 1) * range;
                splitTask[i] = new List<SmartWallet>(range);
                for (int j = start; j < end; j++)
                {
                    splitTask[i].Add(Wallets[j]);
                }
            }
            int last = tab * range - 1;
            for (int i = last; i < Wallets.Count; i++)
            {
                splitTask[tab - 1].Add(Wallets[i]);
            }
            return splitTask;
        }
        public void Open()
        {
            Task.Run(async () =>
            {
                for (int i = 0; i < Config.LDTab; i++)
                {
                    LDClient client = LDPlayerController.Open(i + 1);
                    clients[i] = client;
                    await Task.Delay(1000);
                    LDPlayerController.SortWindow();
                }
            });
        }
        public void StartApp()
        {
            for (int i = 0; i < Config.LDTab; i++)
            {
                clients[i].OpenApp(Config.PackageName);
            }
        }
        public void OnSortWindowClick()
        {
            LDPlayerController.SortWindow();
        }
        public void OnSmartTransactionClick()
        {
            if (IsStartTransactionSmart) return;
            IsStartTransactionSmart = true;
            List<SmartWallet>[] splitTask = SplitTask();
            int tab = Config.LDTab;
            for (int i = 0; i < tab; i++)
            {
                LDClient client = clients[i];
                List<SmartWallet> task = splitTask[i];
                Task.Run(() => TransactionSmartList(client, task));
            }
        }
        public void OnEnergyDelegateClick()
        {
            if (IsStartDelegateEnergy) return;
            IsStartDelegateEnergy = true;
            List<SmartWallet>[] splitTask = SplitTask();
            int tab = Config.LDTab;
            for (int i = 0; i < tab; i++)
            {
                LDClient client = clients[i];
                List<SmartWallet> task = splitTask[i];
                Task.Run(() => EnergyDelegateList(client, task));
            }
        }
        public void OnTransactionUltimaClick()
        {
            if (IsStartTransactionUltima) return;
            IsStartTransactionUltima = true;
            List<SmartWallet>[] splitTask = SplitTask();
            int tab = Config.LDTab;
            for (int i = 0; i < tab; i++)
            {
                LDClient client = clients[i];
                List<SmartWallet> task = splitTask[i];
                Task.Run(() => TransactionUltimaList(client, task,false));
            }
        }
        public void OnTransactionUltimaFromMainClick()
        {
            if (IsStartTransactionUltima) return;
            IsStartTransactionUltima = true;
            List<SmartWallet>[] splitTask = SplitTask();
            int tab = Config.LDTab;
            for (int i = 0; i < tab; i++)
            {
                LDClient client = clients[i];
                List<SmartWallet> task = splitTask[i];
                Task.Run(() => TransactionUltimaList(client, task, true));
            }
        }
        public void OnEnergyRecoveryClick()
        {
            if (IsStartRecoveryEnerggy) return;
            IsStartRecoveryEnerggy = true;
            int tab = Config.LDTab;
            for (int i = 0; i < tab; i++)
            {
                LDClient client = clients[i];
                Task.Run(() => EnergyRecovery(client));
            }
        }
        public void OnDumpXMLClick()
        {
            clients[0].DumpXMLAndSave();
        }
        public void OnClearAppDataAndReloginClick()
        {
            if (IsRestartApp) return;
            IsRestartApp = true;
            int tab = Config.LDTab;
            for (int i = 0; i < tab; i++)
            {
                LDClient client = clients[i];
                client.ClearData(Config.PackageName);
            }
            StartApp();
            for (int i = 0; i < tab; i++)
            {
                LDClient client = clients[i];
                Task.Run(() => NewLogin(client));
            }
        }
        public void OnStopAppClick()
        {
            int tab = Config.LDTab;
            for (int i = 0; i < tab; i++)
            {
                LDClient client = clients[i];
                client.CloseApp();
            }
        }
        public void OnRegisterClick()
        {
            if(IsStartRegister) return;
            IsStartRegister = false;
            int tab = Config.LDTab;
            for(int i = 0; i < tab;i++)
            {
                LDClient client = clients[i];
                Task.Run(() => RegisterLoop(client));
            }
        }
        public void OnTransactionBullClick()
        {
            if (IsStartTransactionBull) return;
            IsStartTransactionBull = false;
            List<SmartWallet>[] splitTask = SplitTask();
            int tab = Config.LDTab;
            for (int i = 0; i < tab; i++)
            {
                LDClient client = clients[i];
                List<SmartWallet> task = splitTask[i];
                Task.Run(() => TransactionBullList(client, task));
            }
        }
    }
}
