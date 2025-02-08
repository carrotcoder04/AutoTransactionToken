
using Auto_LDPlayer;
using Auto_LDPlayer.Enums;
using AutoTransactionToken.Config;
using System;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AutoTransactionToken.Simulator
{
    public class LDClient
    {
        public bool IsStart { get; private set; }
        public LDevice Device { get; private set; }
        public int ID => Device.index;
        public LDClient(int id)
        {
            Device = new LDevice();
            Device.index = id;
            SetDeviceAsync();
        }
        public void DisableAnimation()
        {
            LDPlayer.Adb(LDType.Id, Device.index.ToString(), $"shell settings put global window_animation_scale 0");
            LDPlayer.Adb(LDType.Id, Device.index.ToString(), $"shell settings put global transition_animation_scale 0");
            LDPlayer.Adb(LDType.Id, Device.index.ToString(), $"shell settings put global animator_duration_scale 0");
        }
        public LDClient(LDevice device)
        {
            this.Device = device;
            if(device.BindHandle != IntPtr.Zero)
            {
                IsStart = true;
            }
            else
            {
                SetDeviceAsync();
            }
        }

        private async Task SetDeviceAsync()
        {
            while(!IsStart)
            {
                var device = LDPlayerController.GetDeviceRunningById(Device.index);
                if (device == null)
                {
                    await Task.Delay(1000);
                    continue;
                }
                if (device.BindHandle != IntPtr.Zero)
                {
                    IsStart = true;
                    this.Device = device;
                }
                await Task.Delay(1000);
            }
        }

        public Bitmap ScreenShot()
        {
            return LDPlayer.ScreenShoot(LDType.Id, Device.index.ToString());
        }
        public void OpenApp(string packageName)
        {
            LDPlayerController.OpenApp(Device.index, packageName);
        }
        public void CloseApp()
        {
            LDPlayer.KillApp(LDType.Id,Device.index.ToString(),AppConfig.Instance.PackageName);
        }
        public void ClearData(string packageName)
        {
            LDPlayer.Adb(LDType.Id, Device.index.ToString(), $"shell pm clear {packageName}");
        }
        public bool FindImageAndClick(string path)
        {
            return LDPlayer.FindImageAndClick(LDType.Id,Device.index.ToString(), path);
        }
        public void Back()
        {
            LDPlayer.Back(LDType.Id, Device.index.ToString());
        }
        public void Swipe(int x1,int y1,int x2,int y2,int duration)
        {
            LDPlayer.Swipe(LDType.Id, Device.index.ToString(), x1, y1, x2, y2, duration);
        }
        public void Click(int x,int y)
        {
            LDPlayer.Tap(LDType.Id, Device.index.ToString(), x, y);
        }
        public void ClickPercent(float x, float y)
        {
            LDPlayer.TapByPercent(LDType.Id, Device.index.ToString(), x, y);
        }
        public void LongPress(int x,int y,int duration)
        {
            LDPlayer.LongPress(LDType.Id, Device.index.ToString(), x, y,duration);
        }
        public void InputText(string text)
        {
            LDPlayer.InputText(LDType.Id, Device.index.ToString(), text);
        }
        public void PressKey(LDKeyEvent key)
        {
            LDPlayer.PressKey(LDType.Id, Device.index.ToString(), key);
        }
        public string DumpXML()
        {
            return DumpXMLAndSave();
        }
        public string DumpXMLAndSave()
        {
            LDPlayer.Adb(LDType.Id,Device.index.ToString(), "shell uiautomator dump /sdcard/ui.xml");
            string path = $"{Directory.GetCurrentDirectory()}/ui-{Device.index}.xml";
            LDPlayer.Adb(LDType.Id, Device.index.ToString(), $"pull /sdcard/ui.xml {path}");
            return File.ReadAllText(path);
        }
        public bool DumpAndCheckKey(string key)
        {
            string result = DumpXML();
            if (result.Contains(key))
            {
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"Id: {Device.index}\nName: {Device.name}";
        }
    }
}
