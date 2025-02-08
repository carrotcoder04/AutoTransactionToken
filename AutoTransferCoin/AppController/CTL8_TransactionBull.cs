using AutoTransactionToken.Log;
using AutoTransactionToken.Simulator;
using Newtonsoft.Json;

namespace AutoTransactionToken.AppController
{
    public partial class Controller
    {
        private async Task TransactionBullList(LDClient client, List<SmartWallet> wallets)
        {
            for (int i = 0; i < wallets.Count; i++)
            {
                var wallet = wallets[i];
                await TransactionBull(client, wallet);
            }
            IsStartTransactionBull = false;
        }
        private async Task TransactionBull(LDClient client, SmartWallet wallet)
        {

            if (wallet.SmartState != SmartTransactionState.SmartTransactionSuccess || wallet.EnergyState != EnergyTransactionState.EnergySuccess)
            {
                return;
            }
            if(wallet.BullState == BullTransactionState.BullSuccess)
            {
                return;
            }
            wallet.BullState = BullTransactionState.BullRunning;
            await Login(client, wallet);
            await Service.ClickElement(client.ID,BTN_SPLITS);
            await Task.Delay(300);
            client.Swipe(200, 200, 200, 50, 400);
            await Task.Delay(200);
            while(await Service.ClickElement(client.ID, BTN_BULL))
            {
                await Task.Delay(200);
            }
            await Task.Delay(700);
            bool flag = false;
            do
            {
                await Service.ClickElement(client.ID, BTN_SEND);
                await Task.Delay(440);
                if (await Service.ClickElement(client.ID, BTN_DIALOG_OK))
                {
                    await Task.Delay(450);
                }
                else if (!await Service.ContainElement(client.ID, BTN_SEND))
                {
                    flag = true;
                }
            }
            while (!flag);
            await Task.Delay(1200);
            
            await Service.SetTextElement2(client.ID,1,Config.TargetAddress);
            await Task.Delay(600);
            if (Config.IsMaxBull)
            {
                await Service.ClickElement(client.ID, BTN_MAX);
                await Task.Delay(200);
                await Service.ClickElement(client.ID, BTN_NEXT);
                await Task.Delay(1000);
                flag = false;
                while (await Service.ContainElement(client.ID, ATTENTION) && !flag)
                {
                    flag = await Service.ClickElement(client.ID, BTN_CONFIRM);
                    await Task.Delay(200);
                }
            }
            else
            {
                await Service.SetTextElement2(client.ID,0,Config.UltimaToken.ToString("F6"));
                await Task.Delay(200);
                await Service.ClickElement(client.ID, BTN_NEXT);
            }
            await Task.Delay(500);
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
            await Task.Delay(500);
            flag = false;
            do
            {
                client.Swipe(200, 400, 200, 50, 500);
                await Service.ClickElement(client.ID, CHECK_BOX_AGREE);
                await Task.Delay(300);
                flag = await Service.ClickElement(client.ID, BTN_CONFIRM_RISKS);
            }
            while (!flag);
            await Task.Delay(600);
            await Service.ClickElement(client.ID, BTN_NEXT2);
            await Task.Delay(800);
            await EnterPassWord(client);
            await EnterPassWord(client);
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
            wallet.BullState = BullTransactionState.BullSuccess;
            await GoSmartHome(client);
            client.ClickPercent(91f, 45f);
        }
    }
}
