using Auto_LDPlayer;
using Auto_LDPlayer.Enums;
using System.Diagnostics;

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
        public static void DisableAnimation()
        {
            ExecuteAdbCommand("shell settings put global window_animation_scale 0");
            ExecuteAdbCommand("shell settings put global transition_animation_scale 0");
            ExecuteAdbCommand("shell settings put global animator_duration_scale 0");
        }
        static void ExecuteAdbCommand(string command)
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "adb",
                    Arguments = command,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();
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
