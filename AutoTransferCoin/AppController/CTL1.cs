
using AutoTransactionToken.Simulator;
using System.Diagnostics;

namespace AutoTransactionToken.AppController
{
    public partial class Controller
    {
        private async Task UpdateWallet()
        {
            while (true)
            {
                foreach (var wallet in Wallets)
                {
                    wallet.Update();
                }
                await Task.Delay(5000);
            }
        }

        public async Task Login(LDClient client, SmartWallet wallet,bool isNew = false)
        {
            if (!isNew)
            {
                await Service.ClickElement(client.ID, BTN_SETTINGS);
                await Task.Delay(400);
                await Service.ClickElement(client.ID, MY_WALLET);
                await Task.Delay(1000);
                while(await Service.ContainElement(client.ID, ADD_WALLET))
                {
                    await Service.ClickElement(client.ID, ADD_WALLET);
                    await Task.Delay(400);
                }
                await Service.ClickElement(client.ID, RESTORE_WALLET);
                await Task.Delay(400);
            }
            string text = wallet.SecretKey;
            string[] parts = text.Split(' ');
            foreach (var part in parts)
            {
                await Service.SetTextElement(client.ID, part);
                await Task.Delay(150);
                client.InputText(" ");
                await Task.Delay(150);
            }
            client.Swipe(200, 300, 200, 50, 300);
            await Service.ClickElement(client.ID, BTN_RESTORE);
            await Task.Delay(400);
            while (await Service.ContainElement(client.ID, BTN_NEXT))
            {
                await Service.ClickElement2(client.ID, BTN_NEXT);
                await Task.Delay(100);
            }
            bool flag = false;
            do
            {
                if (await Service.ContainElement(client.ID, SUCCESS))
                {
                    flag = await Service.ClickElement(client.ID, BTN_OK);
                }
                else if (await Service.ContainElement(client.ID, OOPS))
                {
                    flag = await Service.ClickElement(client.ID, BTN_CANCEL);
                }

                await Task.Delay(300);
            } while (!flag);
            await Task.Delay(500);
            await GoSmartHome(client);
            await Task.Delay(400);
        }

        private async Task EnterPassWord(LDClient client)
        {
            await Service.ClickElement2(client.ID, "1");
            await Task.Delay(200);
            await Service.ClickElement2(client.ID, "1");
            await Task.Delay(200);
            await Service.ClickElement2(client.ID, "1");
            await Task.Delay(200);
            await Service.ClickElement2(client.ID, "1");
            await Task.Delay(200);
            await Service.ClickElement2(client.ID, "1");
            await Task.Delay(200);
            await Service.ClickElement2(client.ID, "1");
            await Task.Delay(200);
            await Service.ClickElement2(client.ID, "1");
            await Task.Delay(200);
            await Service.ClickElement2(client.ID, "1");
            await Task.Delay(200);
            await Service.ClickElement2(client.ID, "1");
            await Task.Delay(200);
            await Service.ClickElement2(client.ID, "1");
        }
        private async Task GoSmartHome(LDClient client)
        {
            await Task.Delay(300);
            await Service.ClickElement(client.ID, BTN_WALLET);
            await Task.Delay(300);
            await Service.ClickElement(client.ID, BTN_ALL);
            await Task.Delay(300);
        }
        private List<SmartWallet>[] SplitTask2(Func<SmartWallet,bool> condition)
        {
            int tab = Config.LDTab;
            List<SmartWallet>[] splitTask = new List<SmartWallet>[tab];
            List<SmartWallet> tmp = new List<SmartWallet>();
            foreach(var wallet in Wallets)
            {
                if(condition(wallet))
                {
                    tmp.Add(wallet);
                }
            }
            int range = tmp.Count / tab;
            for (int i = 0; i < tab; i++)
            {
                int start = i * range;
                int end = (i + 1) * range;
                splitTask[i] = new List<SmartWallet>();
                for (int j = start; j < end; j++)
                {
                    splitTask[i].Add(tmp[j]);
                }
            }
            int last = tab * range - 1;
            for (int i = last; i < tmp.Count; i++)
            {
                try
                {
                    splitTask[tab - 1].Add(tmp[i]);
                }
                catch (Exception ex)
                {
                    MainForm.MessageBox($"{tab - 1} {splitTask.Length} {last} {i} {tmp.Count}");
                }
            }
            return splitTask;
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
                    await Task.Delay(500);
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
            List<SmartWallet>[] splitTask = SplitTask2(x => x.SmartState != SmartTransactionState.SmartTransactionSuccess);
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
            List<SmartWallet>[] splitTask = SplitTask2(x => x.EnergyState != EnergyTransactionState.EnergySuccess);
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
            List<SmartWallet>[] splitTask = SplitTask2(x => x.UltimaState != UltimaTransactionState.UltimaSuccess);
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
            List<SmartWallet>[] splitTask = SplitTask2(x => x.Ultima < Config.UltimaToken);
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
            List<SmartWallet>[] splitTask = SplitTask2(x => x.BullState != BullTransactionState.BullSuccess);
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
