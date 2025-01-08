using AutoTransactionToken.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTransactionToken.AppController
{
    public partial class Controller
    {


        private async Task EnergyRecovery(LDClient client)
        {
            while(IsStartRecoveryEnerggy)
            {
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
                await Task.Delay(200);
                if (client.DumpAndCheckKey("\"btn_reclaim\" checkable=\"false\" checked=\"false\" clickable=\"false\""))
                {
                    IsStartRecoveryEnerggy = false;
                    return;
                }
                client.ClickPercent(50f, 80f);
                while (!client.DumpAndCheckKey("Delegated List"))
                {

                }
                await Task.Delay(240);
                client.ClickPercent(50f, 36f);
                await Task.Delay(400);
                client.ClickPercent(90f, 40f);
                await Task.Delay(200);
                client.ClickPercent(50f, 85f);
                await Task.Delay(400);
                client.ClickPercent(50f, 85f);
                await Task.Delay(500);
                await EnterPassWord(client);
                flag = false;
                do
                {
                    string xml = client.DumpXML();
                    if(xml.Contains("btn_ok"))
                    {
                        client.ClickPercent(50f, 85f);
                        flag = true;
                        break;
                    }
                    if(xml.Contains(BTN_DIALOG_OK))
                    {
                        client.ClickPercent(50f, 50f);
                        flag = true;
                        break;
                    }
                }
                while(!flag);
                await GoSmartHome(client);
                await Task.Delay(3000);
            }
        }
    }
}
