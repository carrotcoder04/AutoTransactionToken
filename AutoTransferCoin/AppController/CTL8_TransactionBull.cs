using AutoTransactionToken.Log;
using AutoTransactionToken.Simulator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
            client.ClickPercent(77f, 46f);
            await Task.Delay(300);
            client.Swipe(200, 200, 200, 10, 500);
            string xml = client.DumpXML();
            XmlSerializer serializer = new XmlSerializer(typeof(Hierarchy));
            using (StringReader reader = new StringReader(xml))
            {
                var hierarchy = (Hierarchy)serializer.Deserialize(reader);
                Nodes node = Helper.FindNodesWithContentDesc(hierarchy.Node, x => x.Contains("BULL"))[0];
                int[] bound = Helper.GetBound(node.Bounds);
                int x = (bound[0] + bound[2]) / 2;
                int y = (bound[1] + bound[3]) / 2;
                client.Click(x, y);
            }
            await Task.Delay(700);
            //Send
            bool flag = false;
            do
            {
                client.ClickPercent(75.5f, 56.5f);
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
            client.InputText(Config.TargetAddress);
            await Task.Delay(200);
            if (Config.IsMaxBull)
            {
                client.ClickPercent(92f, 32f);
                await Task.Delay(200);
                client.ClickPercent(50f, 85f);
                await Task.Delay(1200);
                flag = false;
                do
                {
                    client.ClickPercent(50f, 65f);
                    if (!client.DumpAndCheckKey(BTN_CONFIRM))
                    {
                        flag = true;
                    }
                }
                while (!flag);
            }
            else
            {
                client.ClickPercent(50f, 30f);
                await Task.Delay(200);
                client.InputText(Config.BullToken.ToString("F6"));

            }
            await Task.Delay(500);
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
            await Task.Delay(500);
            client.Swipe(200, 400, 200, 50, 500);
            flag = false;
            do
            {
                client.ClickPercent(8f, 70f);
                await Task.Delay(300);
                client.ClickPercent(50f, 78f);
                await Task.Delay(300);
                flag = !client.DumpAndCheckKey("btn_back_confirm_transfer_risks");
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
            wallet.BullState = BullTransactionState.BullSuccess;
            client.ClickPercent(50f, 60f);
            Report.WriteLine(JsonConvert.SerializeObject(wallet));
            await GoSmartHome(client);
            client.ClickPercent(91f, 45f);
        }
    }
}
