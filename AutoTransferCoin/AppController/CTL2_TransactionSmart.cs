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
                await Task.Delay(100);
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
            await Service.ClickElement(client.ID,BTN_SMART);
            await Task.Delay(700);
            bool flag = false;
            do
            {
                await Service.ClickElement(client.ID,BTN_SEND);
                await Task.Delay(440);
                if (await Service.ClickElement(client.ID,BTN_DIALOG_OK))
                {
                    await Task.Delay(450);
                }
                else if(!await Service.ContainElement(client.ID, BTN_SEND))
                {
                    flag = true;
                }
            }
            while (!flag);
            await Task.Delay(1200);
            await Service.SetTextElement2(client.ID,1,wallet.Address);
            await Task.Delay(600);
            await Service.SetTextElement2(client.ID,0,((int)Config.SmartToken).ToString());
            await Task.Delay(400);
            flag = false;
            do
            {
                await Service.ClickElement(client.ID, BTN_NEXT);
                await Task.Delay(440);
                if (await Service.ClickElement(client.ID, BTN_DIALOG_OK))
                {
                    await Task.Delay(450);
                }
                else if (!await Service.ContainElement(client.ID, BTN_NEXT))
                {
                    flag = true;
                }
            }
            while (!flag);
            await Task.Delay(200);
            await Service.ClickElement(client.ID, BTN_NEXT);
            await Task.Delay(800);
            await EnterPassWord(client);
            await Task.Delay(800);
            flag = false;
            do
            {
                if (await Service.ContainElement(client.ID, SUCCESS))
                {
                    flag = await Service.ClickElement(client.ID, BTN_DIALOG_OK);
                }
                await Task.Delay(120);
            }
            while (!flag);
            wallet.SmartState = SmartTransactionState.SmartTransactionSuccess;
            await GoSmartHome(client);
        }
    }
}
