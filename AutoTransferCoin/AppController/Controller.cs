using AutoTransactionToken.Config;
using AutoTransactionToken.Simulator;
namespace AutoTransactionToken.AppController
{
    public partial class Controller
    {
        public readonly static Controller Instance = new Controller();
        public bool IsStartTransactionSmart { get; private set; }
        public bool IsStartDelegateEnergy { get; private set; }
        public bool IsStartTransactionUltima { get; private set; }
        public bool IsStartRecoveryEnerggy { get; private set; }
        public bool IsRestartApp { get; private set; }
        public bool IsStartRegister { get; private set; }
        public bool IsStartTransactionBull { get; private set; }
        public AppConfig Config => AppConfig.Instance;
        private LDClient[] clients;
        private const string BTN_DIALOG_OK = "btn_dialog_ok";
        private const string BTN_MANAGE = "btn_manage";
        private const string BTN_CONFIRM = "btn_confirm";
        private const string SUCCESS = "Success";
        public List<SmartWallet> Wallets => MyData.Wallets;
        public void Init()
        {
            clients = new LDClient[Config.LDTab];
            MyData.Init();
            Service.Init();
            LDPlayerController.SetADBPath(Config.ADBFolder);
            LDPlayerController.SetPath(Config.LDPlayerConsolePath);
            Task.Run(() => UpdateWallet());
        }

    }
}
