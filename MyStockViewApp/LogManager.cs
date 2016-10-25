using System.IO;

namespace MyStockViewApp
{
    public class LogManager : ILogManager
    {
        private string filename = "ErrorLogs.txt";
        public LogManager()
        {
            File.Open(filename, FileMode.OpenOrCreate).Close();
        }

        public void Write(string message)
        {
            File.AppendAllText(filename, message);
        }
    }
}
