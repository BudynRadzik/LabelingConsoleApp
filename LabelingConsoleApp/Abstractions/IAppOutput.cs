namespace LabelingConsole.Abstractions
{
    public interface IAppOutput
    {
        void Info(string message);
        void Error(string message);
    }
}