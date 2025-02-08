using AutoTransactionToken.Simulator;

namespace AutoTransactionToken.AppController
{
    public partial class Controller
    {


        private async Task EnergyRecovery(LDClient client)
        {
            int index = 0;
            while(IsStartRecoveryEnerggy)
            {
                if (index == 0)
                {
                    await Service.ClickElement(client.ID,BTN_SMART);
                    await Task.Delay(1000);
                }
                while (!await Service.ClickElement(client.ID, BTN_STAKE))
                {
                    await Task.Delay(300);
                }
                await Task.Delay(500);
                while (await Service.ContainElement(client.ID, BTN_MANAGE))
                {
                    await Service.ClickElement(client.ID, BTN_MANAGE);
                    await Task.Delay(300);
                }
                await Task.Delay(500);
                int cnt = 0;
                while (await Service.ContainElement(client.ID, BTN_RECLAIM))
                {
                    cnt++;
                    if (cnt >= 100)
                    {
                        IsStartRecoveryEnerggy = false;
                        break;
                    }
                    await Service.ClickElement(client.ID, BTN_RECLAIM);
                    await Task.Delay(300);
                }
                client.ClickPercent(50f, 80f);
                while (!await Service.ContainElement(client.ID, DELEGATED_LIST))
                {
                    await Task.Delay(240);
                }
                await Task.Delay(440);
                await Service.ClickElement3(client.ID, index % 3, BTN_RECLAIM);
                await Task.Delay(500);
                await Service.ClickElement(client.ID, BTN_MAX);
                await Task.Delay(200);
                await Service.ClickElement(client.ID, BTN_NEXT2);
                await Task.Delay(200);
                await Service.ClickElement(client.ID, BTN_CONFIRM);
                await Task.Delay(400);
                await EnterPassWord(client);
                bool flag = false;
                do
                {
                    flag = await Service.ClickElement(client.ID, BTN_OK) ||
                           await Service.ClickElement(client.ID, BTN_DIALOG_OK);
                    await Task.Delay(300);
                }
                while(!flag);
                index++;
                // await GoSmartHome(client);
                await Task.Delay(500);
            }
        }
    }
}
