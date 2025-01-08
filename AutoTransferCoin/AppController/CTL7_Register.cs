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
            client.ClickPercent(16f, 8f);
            await Task.Delay(300);
            client.ClickPercent(66f, 28f);
            await Task.Delay(300);
            client.ClickPercent(66f, 28f);
            await Task.Delay(800);
            client.Swipe(200, 180, 200, 10, 500);

            while (client.DumpAndCheckKey("btn_basic_security"))
            {
                client.ClickPercent(50f, 55f);
                await Task.Delay(300);
                client.ClickPercent(50f, 90f);
            }
            await Task.Delay(500);
            bool flag = false;
            string[] keys = new string[12];
            do
            {
                client.LongPress(200, 460, 600);

                string content = client.DumpXML();
                var listContentDesc = Helper.GetContentDesc(content);
                for (int i = 0; i < listContentDesc.Count; i++)
                {
                    try
                    {
                        string s = listContentDesc[i];
                        if (s.Length == 0) continue;
                        if (s[0] < '0' || s[0] > '9') continue;
                        var parts = s.Split("&#10;");
                        int index = int.Parse(parts[0]);
                        string k = parts[1];
                        if (k[0] == '-')
                        {
                            continue;
                        }
                        keys[index - 1] = k.ToLower();
                        flag = true;
                    }
                    catch (Exception ex)
                    {
                        MainForm.MessageBox(ex.Message);
                    }
                }
            }
            while (!flag);
            while(client.DumpAndCheckKey("tbtn_agreement"))
            {
                client.ClickPercent(7.5f, 86f);
                await Task.Delay(300);
                client.ClickPercent(50f, 95f);
            }
            await Task.Delay(700);
            for (int i = 0; i < keys.Length; i++)
            {
                try
                {
                    await Task.Delay(340);
                    string key = keys[i];
                    string xm = client.DumpXML();
                    XmlSerializer serializer = new XmlSerializer(typeof(Hierarchy));
                    using (StringReader reader = new StringReader(xm))
                    {
                        var hierarchy = (Hierarchy)serializer.Deserialize(reader);
                        var node = Helper.FindNodesWithContentDesc(hierarchy.Node,x => x == key.ToUpper())[0];
                        var bound = Helper.GetBound(node.Bounds);
                        int x = (bound[0] + bound[2]) / 2; 
                        int y = (bound[1] + bound[3]) / 2; 
                        Logger.WriteLine($"{i}: {key} : {x} {y}");
                        client.Click(x, y);
                    }
                    //var a = Helper.GetButton(key.ToUpper(), x);
                    //Logger.WriteLine($"{i}: {key} : {a.Item1} {a.Item2}");
                }
                catch (Exception ex)
                {
                    MainForm.MessageBox(ex.ToString());
                }
            }
            await Task.Delay(300);
            client.ClickPercent(50f, 54f);
            await Task.Delay(300);
            client.ClickPercent(50f, 60f);
            await Task.Delay(800);
            client.ClickPercent(7.5f, 6.4f);
            await Task.Delay(200);
            client.ClickPercent(7.5f, 6.4f);
            await Task.Delay(200);
            client.ClickPercent(7.5f, 6.4f);
            await Task.Delay(600);
            client.ClickPercent(11f, 56.5f);
            await Task.Delay(1000);
            string address = string.Empty;
            string xml = client.DumpXML();
            XmlSerializer serial = new XmlSerializer(typeof(Hierarchy));
            using (StringReader reader = new StringReader(xml))
            {
                var hierarchy = (Hierarchy)serial.Deserialize(reader);
                var node = Helper.FindNodesWithContentDesc(hierarchy.Node, x => x.StartsWith("s"))[0];
                address = node.ContentDesc.Split("\n")[0];
            }
            RegisterData.Register(string.Join(" ", keys) + " " + address);
            client.ClickPercent(15f, 95f);
        }
    }
}
