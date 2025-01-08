
using AutoTransactionToken.Config;
using Newtonsoft.Json;

namespace AutoTransactionToken
{
    public class SmartWallet
    {

        [Newtonsoft.Json.JsonIgnore]
        public int Index { get; set; }
        public string Address { get; set; }
        public string SecretKey { get; set; }
        public bool IsComplete => UltimaState == UltimaTransactionState.UltimaSuccess && BullState == BullTransactionState.BullSuccess;
        private double smart;
        public double Smart
        {
            get => smart;
            set
            {
                this.smart = value;
                if(value >= AppConfig.Instance.SmartToken - 0.01f)
                {
                    SmartState = SmartTransactionState.SmartTransactionSuccess;
                }
            }
        }
        private double energy = 0;
        public double Energy
        {
            get => energy;
            set
            {
                this.energy = value;
                if(value >= AppConfig.Instance.Energy - 1000)
                {
                    EnergyState = EnergyTransactionState.EnergySuccess;
                }
            }
        }
        private double ultima;
        private double bull;
        public double Bull
        {
            get => bull;
            set
            {
                bull = value;
                if (value < 0.00001f)
                {
                    BullState = BullTransactionState.BullSuccess;
                }
            }
        }
        private string BALANCE_SMART_ULTIMA_URL => $"https://api.smartexplorer.com/api/account/tokens?address={Address}&page=0&size=5";
        private string ENEGY_URL => $"https://api.smartexplorer.com/api/accountv2?address={Address}";

        public double Ultima
        {
            get => ultima;
            set
            {
                this.ultima = value;
                if(value < 0.0001f)
                {
                    UltimaState = UltimaTransactionState.UltimaSuccess;
                }
            }
        }
        public SmartTransactionState SmartState { get; set; }
        public EnergyTransactionState EnergyState { get; set; }
        public UltimaTransactionState UltimaState { get; set; }
        public BullTransactionState BullState { get; set; }
        public SmartWallet()
        {

        }
        public override string ToString()
        {
            return $"Address: {Address}, Smart: {Smart}, Energy: {Energy}, Ultima: {Ultima.ToString("F6")}, Bull: {Bull}, SmartState: {SmartState}, EnergyState: {EnergyState}, UltimaState: {UltimaState}, BullState: {BullState}";
        }

        public void Set(SmartWallet wallet)
        {
            this.Address = wallet.Address;
            this.SecretKey = wallet.SecretKey;
            this.SmartState = wallet.SmartState;
            this.EnergyState = wallet.EnergyState;
        }

        public SmartWallet(int index, string address, string secretKey)
        {
            Index = index;
            Address = address;
            SecretKey = secretKey;
            SmartState = SmartTransactionState.None;
            EnergyState = EnergyTransactionState.None;
            UltimaState = UltimaTransactionState.None;
        }
        public async Task Update()
        {
            string content = await Service.Get(BALANCE_SMART_ULTIMA_URL);
            SmartAndUltima smartAndUltima = JsonConvert.DeserializeObject<SmartAndUltima>(content);
            if (smartAndUltima != null)
            {
                foreach (var c in smartAndUltima.data.content)
                {
                    if (c.tokenAbbr == "ULTIMA")
                    {
                        this.Ultima = c.amount;
                    }
                    else if (c.tokenAbbr == "SMART")
                    {
                        this.Smart = c.amount;
                    }
                    else if(c.tokenAbbr == "BULL")
                    {
                        this.Bull = c.amount;
                    }
                }
            }
            content = await Service.Get(ENEGY_URL);
            var e = JsonConvert.DeserializeObject<Model.Energy>(content);
            if (e != null)
            {
                this.Energy = e.data.bandwidth.energyRemaining;
            }
        }
    }
}
