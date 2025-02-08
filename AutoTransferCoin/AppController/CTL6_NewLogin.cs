using AutoTransactionToken.Simulator;

namespace AutoTransactionToken.AppController
{
    public partial class Controller
    {
        public async Task NewLogin(LDClient client)
        {
            await Task.Delay(6000);
            while(!await Service.ContainElement(client.ID,BTN_SELECT_LANGUAGE))
            {

            }
            await Task.Delay(200);
            await Service.ClickElement2(client.ID,BTN_NEXT);
            await Task.Delay(600);
            await Service.ClickElement(client.ID,CHECK_BOX_AGREE);
            await Task.Delay(200);
            await Service.ClickElement(client.ID,BTN_RESTORE);
            await Task.Delay(800);
            await Login(client,new SmartWallet() { SecretKey = Config.SecretKey},true);
            await Service.ClickElement(client.ID, BTN_SETTINGS);
            await Task.Delay(400);
            await Service.ClickElement(client.ID, SECURITY);
            await Task.Delay(400);
            await Service.ClickElement(client.ID, PIN);
            await EnterPassWord(client);
            await Task.Delay(400);
            await EnterPassWord(client);
            await Task.Delay(300);
            await Service.ClickElement(client.ID, Login_Confirmation);
            await Task.Delay(500);
            await EnterPassWord(client);
            await Task.Delay(500);
            await GoSmartHome(client);
            IsRestartApp = false;
        }
    }
}
