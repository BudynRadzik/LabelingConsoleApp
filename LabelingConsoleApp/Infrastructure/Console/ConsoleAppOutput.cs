using LabelingConsole.Abstractions;

namespace LabelingConsole.Infrastructure.Console
{
    public class ConsoleAppOutput : IAppOutput
    {
        public void Info(string message) => System.Console.WriteLine(message);
        public void Error(string message) => System.Console.WriteLine(message);
    }
}