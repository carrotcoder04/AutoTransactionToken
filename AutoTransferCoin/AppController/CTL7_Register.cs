using AutoTransactionToken.Log;
using AutoTransactionToken.Simulator;
using System.Xml.Serialization;
using static Model;

namespace AutoTransactionToken.AppController
{
    public partial class Controller
    {
        private async Task RegisterLoop(LDClient client)
        {
            while (true)
            {
                await Register(client);
                await Task.Delay(500);
            }
        }
        private async Task Register(LDClient client)
        {
            await Service.ClickElement(client.ID, BTN_WALLET_SETTINGS);
            await Task.Delay(300);
            while (await Service.ContainElement(client.ID, ADD_WALLET))
            {
                await Service.ClickElement(client.ID, ADD_WALLET);
                await Task.Delay(400);
            }
            await Task.Delay(300);
            await Service.ClickElement(client.ID, ITEM_CREATE_WALLET);
            await Task.Delay(700);
            client.Swipe(200, 300, 200, 150, 500);
            await Task.Delay(500);
            await Service.ClickElement(client.ID, BTN_BASIC_SECURITY);
            await Task.Delay(300);
            await Service.ClickElement(client.ID, BTN_CONTINUE);
            await Task.Delay(600);
            string result = await Service.GetKeyRegister(client.ID);
            string[] keys = result.Split(' ');
            await Service.ClickElement(client.ID, CHECK_BOX_AGREE);
            await Task.Delay(300);
            await Service.ClickElement(client.ID, BTN_NEXT2);
            await Task.Delay(600);
            foreach (string key in keys)
            {
                await Service.ClickElement2(client.ID, key.ToUpper());
                await Task.Delay(200);
            }
            await Task.Delay(300);
            await Service.ClickElement(client.ID, BTN_NEXT2);
            await Task.Delay(800);
            await Service.ClickElement(client.ID, BTN_DIALOG_OK);
            await Task.Delay(500);
            await GoSmartHome(client);
            await Task.Delay(200);
            while (await Service.ClickElement(client.ID, BTN_SMART))
            {
                await Task.Delay(200);
            }
            await Task.Delay(1000);
            string address = await Service.GetAddress(client.ID);
            bool flag = char.IsLetter(result[0]) && char.IsLetter(address[0]) && address != "FAIL";
            if(flag)
            {
                RegisterData.Register(result + " " + address);
            }
            await GoSmartHome(client);
        }
    }
}
