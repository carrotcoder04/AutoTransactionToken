using Auto_LDPlayer;
using Auto_LDPlayer.Enums;

namespace AutoTransactionToken.Simulator
{
    public static class LDPlayerController
    {
        public static void SetPath(string path)
        {
            LDPlayer.SetPathLD(path);
        }
        public static void SetADBPath(string path)
        {
            LDPlayer.SetADBPFolder(path);
        }

        public static void ADBRestartServer()
        {
            LDPlayer.RestartServer();
        }
        public static LDClient Open(int id)
        {
            LDPlayer.Open(LDType.Id,id.ToString());
            return new LDClient(id);
        }
        public static LDevice GetDeviceRunningById(int id)
        {
            var devices = GetDevicesRunning();
            LDevice device = devices.FirstOrDefault(x => x.index == id);
            return device;
        }
        public static List<string> GetDevices()
        {
            return LDPlayer.GetDevices();
        }
        public static void SortWindow()
        {
            LDPlayer.SortWnd();
        }
        public static List<LDevice> GetDevicesRunning()
        {
            return LDPlayer.GetDevices2();
        }
        public static void OpenApp(int id,string packageName)
        {
            LDPlayer.RunApp(LDType.Id, id.ToString(), packageName);
        }
        public static void CloseApp(int id,string packageName)
        {
            LDPlayer.KillApp(LDType.Id, id.ToString(),packageName);
        }
    }
}
