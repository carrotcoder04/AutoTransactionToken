using AutoTransactionToken.Log;
using AutoTransactionToken.Simulator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTransactionToken.AppController
{
    public partial class Controller
    {
        private async Task TransactionUltimaList(LDClient client, List<SmartWallet> smartWallets,bool fromMain)
        {
            foreach (SmartWallet smartWallet in smartWallets)
            {
                await TransactionUltima(client, smartWallet,fromMain);
                await Task.Delay(500);
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
            client.ClickPercent(10f, 75f);
            await Task.Delay(700);
            //Send
            bool flag = false;
            do
            {
                client.ClickPercent(75.5f, 56.5f);
                await Task.Delay(120);
                if (client.DumpAndCheckKey(BTN_DIALOG_OK))
                {
                    client.ClickPercent(50f,60f);
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
            if (!fromMain)
            {
                client.InputText(Config.TargetAddress);
            }
            else
            {
                client.InputText(wallet.Address);
            }
            await Task.Delay(200);
            if(Config.IsMaxUltima)
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
                client.InputText(Config.UltimaToken.ToString("F6"));

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
            wallet.UltimaState = UltimaTransactionState.UltimaSuccess;
            client.ClickPercent(50f, 60f);
            Report.WriteLine(JsonConvert.SerializeObject(wallet));
            await GoSmartHome(client);
        }

    }
}
