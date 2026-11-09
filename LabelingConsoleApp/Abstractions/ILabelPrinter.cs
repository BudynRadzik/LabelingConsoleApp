using LabelingConsole.Domain;

namespace LabelingConsole.Abstractions
{
    public interface ILabelPrinter
    {
        bool TryPrintLabel(Weighing weighing, string templatePath, out string? error);
    }
}
