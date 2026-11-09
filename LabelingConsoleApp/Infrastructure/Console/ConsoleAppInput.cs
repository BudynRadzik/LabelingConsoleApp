using LabelingConsole.Abstractions;

namespace LabelingConsole.Infrastructure.Console
{
    internal class ConsoleAppInput : IAppInput
    {
        bool IAppInput.IsExitRequested()
        {
            if (System.Console.KeyAvailable)
            {
                var keyInfo = System.Console.ReadKey(intercept: true);
                if (keyInfo.Key == System.ConsoleKey.Q)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
