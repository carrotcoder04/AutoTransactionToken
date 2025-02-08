using AutoTransactionToken.Log;
using AutoTransactionToken.Simulator;
using Newtonsoft.Json;

namespace AutoTransactionToken.AppController
{
    public partial class Controller
    {
        private async Task TransactionUltimaList(LDClient client, List<SmartWallet> smartWallets,bool fromMain)
        {
            foreach (SmartWallet smartWallet in smartWallets)
            {
                await TransactionUltima(client, smartWallet,fromMain);
                await Task.Delay(100);
            }
            IsStartTransactionUltima = false;
        }
        private async Task TransactionUltima(LDClient client, SmartWallet wallet,bool fromMain)
        {
            if(!fromMain)
            {
                if (wallet.SmartState != SmartTransactionState.SmartTransactionSuccess || wallet.EnergyState != EnergyTransactionState.EnergySuccess)
                {
                    return;
                }
                if (wallet.UltimaState == UltimaTransactionState.UltimaSuccess)
                {
                    return;
                }
            }
            else
            {
                if(wallet.Ultima >= Config.UltimaToken)
                {
                    return;
                }
            }

            wallet.UltimaState = UltimaTransactionState.UltimaRunning;
            if(!fromMain)
            {
                await Login(client, wallet);
            }
            await Service.ClickElement(client.ID, BTN_ULTIMA);
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
            if (!fromMain)
            {
                await Service.SetTextElement2(client.ID,1,Config.TargetAddress);
            }
            else
            {
                await Service.SetTextElement2(client.ID,1,wallet.Address);
            }
            await Task.Delay(800);
            if (Config.IsMaxUltima)
            {
                await Service.ClickElement(client.ID, BTN_MAX);
                await Task.Delay(200);
                await Service.ClickElement(client.ID, BTN_NEXT);
                await Task.Delay(1000);
                while (await Service.ContainElement(client.ID, ATTENTION))
                {
                    await Service.ClickElement(client.ID, BTN_CONFIRM);
                    await Task.Delay(200);
                }
            }
            else
            {
                await Service.SetTextElement2(client.ID,0,Config.UltimaToken.ToString("F6"));
                await Task.Delay(500);
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
            await Task.Delay(200);
            await Service.ClickElement(client.ID, BTN_NEXT);
            await Task.Delay(800);
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

            wallet.UltimaState = UltimaTransactionState.UltimaSuccess;
            await GoSmartHome(client);
            await Service.ClickElement(client.ID, BTN_ALL);
        }

    }
}
