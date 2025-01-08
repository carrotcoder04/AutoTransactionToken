using AutoTransactionToken.Log;
using AutoTransactionToken.Simulator;
using Newtonsoft.Json;

namespace AutoTransactionToken.AppController
{
    public partial class Controller
    {
        private async Task EnergyDelegateList(LDClient client, List<SmartWallet> smartWallets)
        {
            foreach (SmartWallet smartWallet in smartWallets)
            {
                await EnergyDelegate(client, smartWallet);
                await Task.Delay(500);
            }
            IsStartDelegateEnergy = false;
        }
        private async Task EnergyDelegate(LDClient client, SmartWallet wallet)
        {
            if (Config.IsStrictCondition)
            {
                if (wallet.IsComplete)
                {
                    return;
                }
                if (wallet.EnergyState == EnergyTransactionState.EnergySuccess)
                {
                    return;
                }
            }
            wallet.EnergyState = EnergyTransactionState.EnergyRunning;
            client.ClickPercent(11f, 56.5f);
            await Task.Delay(700);
            bool flag = false;
            do
            {
                client.ClickPercent(25f, 36.5f);
                await Task.Delay(100);
                flag = client.DumpAndCheckKey(BTN_MANAGE);
            }
            while (!flag);
            client.ClickPercent(50f, 42f);
            await Task.Delay(300);
            client.ClickPercent(50f, 87f);
            await Task.Delay(300);
            client.ClickPercent(50f, 63f);
            await Task.Delay(300);
            client.InputText(wallet.Address);
            await Task.Delay(300);
            client.ClickPercent(50f, 50f);
            await Task.Delay(300);
            client.InputText(((int)Config.Energy).ToString());
            await Task.Delay(300);
            client.ClickPercent(50f, 90f);
            await Task.Delay(300);
            while(client.DumpAndCheckKey("btn_confirm"))
            {
                client.ClickPercent(50f, 85f);
                await Task.Delay(100);
            }
            await Task.Delay(800);
            await EnterPassWord(client);
            flag = false;
            do
            {
                if (client.DumpAndCheckKey(SUCCESS))
                {
                    flag = true;
                }
                await Task.Delay(500);
            }
            while (!flag);
            wallet.EnergyState = EnergyTransactionState.EnergySuccess;
            client.ClickPercent(50f, 85f);
            Report.WriteLine(JsonConvert.SerializeObject(wallet));
            await GoSmartHome(client);
        }

    }
}
