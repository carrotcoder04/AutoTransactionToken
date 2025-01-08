namespace AutoTransactionToken.Log
{
    public class Logger
    {
        public const string fileName = "log.txt";
        public static void WriteLine(string report)
        {
            string line = $"{DateTime.Now} - {report}\n";
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine(line);
            }
        }
        public static string Content()
        {
            return File.ReadAllText(fileName);
        }
    }
}
