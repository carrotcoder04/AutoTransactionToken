namespace AutoTransactionToken.Log
{
    public static class RegisterData
    {
        public static void Register(string value)
        {
            using (FileStream fs = new FileStream("register.txt", FileMode.Append, FileAccess.Write))
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes($"\n{value}");
                fs.Write(data, 0, data.Length);
            }
        }
    }
}
