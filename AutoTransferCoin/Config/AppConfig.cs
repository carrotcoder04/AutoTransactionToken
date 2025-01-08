using Newtonsoft.Json;
namespace AutoTransactionToken.Config
{
    public class AppConfig
    {
        private static AppConfig instance;
        public static AppConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = JsonConvert.DeserializeObject<AppConfig>(File.ReadAllText("config.json"));
                }
                return instance;
            }
        }

        public string ADBFolder { get; init; }
        public string LDPlayerConsolePath { get; init; }
        public string PackageName { get; init; }
        public double SmartToken { get; init; }
        public double Energy { get;init; }
        public double UltimaToken { get; init; }
        public double BullToken { get; init; }
        public int LDTab { get; init; }
        public string TargetAddress { get; init; }
        public string SecretKey { get;init; }
        public bool IsStrictCondition { get; init; }
        public bool IsMaxUltima => UltimaToken <= 0;
        public bool IsMaxBull => BullToken <= 0;
    }
}
