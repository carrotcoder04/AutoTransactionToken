using AutoTransactionToken.Simulator;

namespace AutoTransactionToken.AppController
{
    public partial class Controller
    {
        public async Task NewLogin(LDClient client)
        {
            while(!client.DumpAndCheckKey("btn_select_language"))
            {

            }
            await Task.Delay(400);
            client.ClickPercent(50f, 94f);
            await Task.Delay(600);
            client.LongPress(31, 428, 1000);
            await Task.Delay(200);
            client.ClickPercent(50f, 90f);
            await Login(client,new SmartWallet() { SecretKey = Config.SecretKey});
            client.ClickPercent(83f, 96f);
            await Task.Delay(400);
            client.ClickPercent(50f, 30f);
            await Task.Delay(400);
            client.ClickPercent(85f, 20f);
            await EnterPassWord(client);
            await EnterPassWord(client);
            await Task.Delay(300);
            client.ClickPercent(85f, 40f);
            await EnterPassWord(client);
            await GoSmartHome(client);
            IsRestartApp = false;
        }
    }
}
