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
            await Service.ClickElement(client.ID,BTN_SMART);
            await Task.Delay(1000);
            while (!await Service.ClickElement(client.ID, BTN_STAKE))
            {
                await Task.Delay(100);
            }
            await Task.Delay(500);
            while (await Service.ContainElement(client.ID, BTN_MANAGE))
            {
                await Service.ClickElement(client.ID, BTN_MANAGE);
                await Task.Delay(100);
            }
            await Task.Delay(500);
            while (await Service.ContainElement(client.ID, BTN_DELEGATE))
            {
                await Service.ClickElement(client.ID, BTN_DELEGATE);
                await Task.Delay(300);
            }
            await Task.Delay(1200);
            await Service.SetTextElement2(client.ID,1,wallet.Address);
            await Task.Delay(600);
            await Service.SetTextElement2(client.ID,0,Config.Energy.ToString());
            await Task.Delay(400);
            await Service.ClickElement(client.ID,BTN_CONTINUE);
            await Task.Delay(600);
            while(await Service.ClickElement(client.ID, BTN_CONFIRM))
            {
                await Task.Delay(200);
                await Service.ClickElement(client.ID, BTN_OK);
                await Service.ClickElement(client.ID, BTN_DIALOG_OK);
            }
            await Task.Delay(600);
            await EnterPassWord(client);
            bool flag = false;
            do
            {
                if (await Service.ContainElement(client.ID, SUCCESS))
                {
                    flag = await Service.ClickElement(client.ID, BTN_OK);
                }
                await Task.Delay(120);
            }
            while (!flag);
            wallet.EnergyState = EnergyTransactionState.EnergySuccess;
            await GoSmartHome(client);
        }

    }
}
