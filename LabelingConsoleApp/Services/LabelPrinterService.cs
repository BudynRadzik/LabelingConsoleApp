using LabelingConsole.Abstractions;
using LabelingConsole.Domain;


namespace LabelingConsole.Services
{
    public class LabelPrinterService : ILabelPrinter
    {
        public bool TryPrintLabel(Weighing weighing, string templatePath, out string? error)
        {
            if (weighing.LabelFormatId == 0)
            {
                error = null;
                return true; // bez etykiety to też poprawny scenariusz
            }

            for (int i = 1; i <= weighing.NumberOfLabels; i++)
            {
                Console.WriteLine($"Drukowanie etykiety {i} z {weighing.NumberOfLabels} (template: {templatePath})");
            }

            error = null;
            return true;
        }
    }
}
