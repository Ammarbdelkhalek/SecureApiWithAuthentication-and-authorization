namespace SecureApiWithAuthentication.Logging
{
    public class LogginServies : IloggerServices
    {
        public void Log(string message)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "log", "log.txt");
            Console.WriteLine(filePath);
            File.AppendAllText(filePath, message);
        }
    }
}
