
namespace AutoTransactionToken.Log
{
    public static class Report
    {
        public const string fileName = "report.txt";
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
