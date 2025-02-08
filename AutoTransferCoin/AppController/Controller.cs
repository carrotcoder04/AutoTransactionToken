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
        public LDClient[] clients;
        private const string BTN_WALLET = "bn_wallet";
        private const string BTN_SEND = "btn_send";
        private const string BTN_SETTINGS = "bn_settings";
        private const string BTN_SMART = "currency_card_SMART";
        private const string BTN_STAKE = "Stake";
        private const string BTN_ULTIMA = "Token SRC20";
        private const string BTN_BULL = "currency_card_BULL";
        private const string MY_WALLET = "My Wallets";
        private const string ADD_WALLET = "+ ADD WALLET";
        private const string RESTORE_WALLET = "Restore Wallet";
        private const string BTN_DIALOG_OK = "btn_dialog_ok";
        private const string BTN_OK = "btn_ok";
        private const string BTN_MAX = "btn_max";
        private const string BTN_MANAGE = "btn_manage";
        private const string BTN_RESTORE = "btn_restore_wallet";
        private const string ATTENTION = "Attention";
        private const string BTN_CONFIRM = "btn_confirm";
        private const string BTN_NEXT = "Next";
        private const string BTN_NEXT2 = "btn_next";
        private const string BTN_SELECT_LANGUAGE = "btn_select_language";
        private const string SUCCESS = "Success";
        private const string BTN_CREATE_WALLET = "btn_create_wallet";
        private const string ITEM_CREATE_WALLET = "item_create_wallet";
        private const string CHECK_BOX_AGREE = "tbtn_agreement";
        private const string SECURITY = "Security";
        private const string PIN = "PIN";
        private const string Login_Confirmation = "Login Confirmation";
        private const string OOPS = "Oops";
        private const string BTN_CONFIRM_RISKS = "btn_confirm_transfer_risks";
        private const string BTN_FREEZE = "Freeze";
        private const string BTN_CANCEL = "btn_cancel";
        private const string BTN_DELEGATE = "btn_delegate";
        private const string BTN_CONTINUE = "btn_continue";
        private const string BTN_ALL = "btn_all";
        private const string BTN_RECLAIM = "btn_reclaim";
        private const string DELEGATED_LIST = "Delegated List";
        private const string BTN_BASIC_SECURITY = "btn_basic_security";
        private const string BTN_WALLET_SETTINGS = "btn_wallets_settings";
        private const string BTN_SPLITS = "btn_splits";
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
