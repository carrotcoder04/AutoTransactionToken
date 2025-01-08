
using AutoTransactionToken.Log;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AutoTransactionToken
{
    public static class Helper
    {
        public static List<string> GetContentDesc(string xml)
        {
            List<string> result = new List<string>();
            string[] parts = xml.Split("content-desc=\"");
            foreach (string part in parts)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < part.Length; i++)
                {
                    if (part[i] == '"')
                    {
                        break;
                    }
                    sb.Append(part[i]);
                }
                if(sb.Length > 0)
                {
                    result.Add(sb.ToString());
                }
            }
            return result;
        }
        public static int[] GetBound(string key,string xml)
        {
            int index = xml.IndexOf(key);
            int[] bound = new int[4];
            if (index != -1)
            {
                string r = "";
                for(int i =index;i<xml.Length;i++)
                {
                    if (xml[i] == '[')
                    {
                        r = xml.Substring(30);
                        break;
                    }
                }
                string[] parts = r.Split("][");
                string[] x1y1 = parts[0].Split('[')[1].Split(',');
                string[] x2y2 = parts[1].Split(']')[0].Split(',');
                bound[0] = int.Parse(x1y1[0]);
                bound[1] = int.Parse(x1y1[1]);
                bound[2] = int.Parse(x2y2[0]);
                bound[3] = int.Parse(x2y2[1]);
            }
            else
            {
                Logger.WriteLine($"Error at GetBound({key} / {xml})");
            }
            return bound;
        }
        public static int[] GetBound(string r)
        {
            int[] bound = new int[4];
            string[] parts = r.Split("][");
            string[] x1y1 = parts[0].Split('[')[1].Split(',');
            string[] x2y2 = parts[1].Split(']')[0].Split(',');
            bound[0] = int.Parse(x1y1[0]);
            bound[1] = int.Parse(x1y1[1]);
            bound[2] = int.Parse(x2y2[0]);
            bound[3] = int.Parse(x2y2[1]);
            return bound;
        }
        public static (int,int) GetButton(string key,string xml)
        {
            int[] bound = GetBound(key,xml);
            return ((bound[0] + bound[2])/2, (bound[1] + bound[3])/2);
        }
        public static List<Nodes> FindNodesWithContentDesc(Nodes node, Func<string, bool> condition)
        {
            List<Nodes> result = new List<Nodes>();

            if (node == null) return result;

            if (condition(node.ContentDesc))
            {
                result.Add(node);
            }
            if (node.Node != null)
            {
                foreach (var child in node.Node)
                {
                    result.AddRange(FindNodesWithContentDesc(child, condition));
                }
            }

            return result;
        }
    }
}
