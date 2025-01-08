using AutoTransactionToken.Log;
using AutoTransactionToken.Simulator;
using Newtonsoft.Json;
namespace AutoTransactionToken.AppController
{
    public partial class Controller
    {
        private async Task TransactionSmartList(LDClient client, List<SmartWallet> smartWallets)
        {
            foreach (SmartWallet smartWallet in smartWallets)
            {
                await TransactionSmart(client, smartWallet);
                await Task.Delay(500);
            }
            IsStartTransactionSmart = false;
        }

        private async Task TransactionSmart(LDClient client, SmartWallet wallet)
        {
            if (Config.IsStrictCondition)
            {
                if (wallet.IsComplete)
                {
                    return;
                }
                if (wallet.SmartState == SmartTransactionState.SmartTransactionSuccess)
                {
                    return;
                }
            }
            wallet.SmartState = SmartTransactionState.SmartTransactionRunning;
            client.ClickPercent(11f, 56.5f);
            await Task.Delay(700);
            //Send
            bool flag = false;
            do
            {
                client.ClickPercent(75f,50f);
                await Task.Delay(120);
                client.ClickPercent(75f, 46f);
                await Task.Delay(120);
                if (client.DumpAndCheckKey(BTN_DIALOG_OK))
                {
                    client.ClickPercent(50f, 60f);
                    await Task.Delay(250);
                }
                else
                {
                    flag = true;
                }
            }
            while (!flag);
            client.ClickPercent(50f, 50f);
            await Task.Delay(150);
            client.InputText(wallet.Address);
            await Task.Delay(200);
            client.ClickPercent(47f, 30f);
            await Task.Delay(200);
            client.InputText(((int)Config.SmartToken).ToString());
            await Task.Delay(200);

            ///Next
            flag = false;
            do
            {
                client.ClickPercent(50f, 85f);
                await Task.Delay(120);
                if (client.DumpAndCheckKey(BTN_DIALOG_OK))
                {
                    client.ClickPercent(50f, 60f);
                    await Task.Delay(250);
                }
                else
                {
                    flag = true;
                }
            }
            while (!flag);
            client.ClickPercent(50f, 85f);
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
            wallet.SmartState = SmartTransactionState.SmartTransactionSuccess;
            client.ClickPercent(50f, 60f);
            Report.WriteLine(JsonConvert.SerializeObject(wallet));
            await GoSmartHome(client);
        }
    }
}
