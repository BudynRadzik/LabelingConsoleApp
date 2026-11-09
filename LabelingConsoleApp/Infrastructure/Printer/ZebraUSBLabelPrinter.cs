using LabelingConsole.Abstractions;
using LabelingConsole.Domain;
using System.Globalization;
using System.Text;

namespace LabelingConsole.Infrastructure.Printing
{
    public class ZebraUsbLabelPrinter : ILabelPrinter
    {
        private readonly string _printerName;

        public ZebraUsbLabelPrinter(string printerName)
        {
            _printerName = printerName;
        }

        public bool TryPrintLabel(Weighing weighing, string templatePath, out string? error)
        {
            if (weighing.LabelFormatId == 0)
            {
                error = null;
                return true; // bez etykiety
            }

            var fullPath = Path.IsPathRooted(templatePath)
         ? templatePath
         : Path.Combine(AppContext.BaseDirectory, templatePath);

            if (!File.Exists(fullPath))
            {
                error = $"Nie znaleziono szablonu etykiety: {fullPath}";
                return false;
            }

            string template = File.ReadAllText(fullPath, Encoding.ASCII);

            string payload = template
                .Replace("{{WEIGHT}}", weighing.Weight.ToString("0.###", CultureInfo.InvariantCulture))
                .Replace("{{DATE}}", weighing.WeighedAt.ToLocalTime().ToString("yyyy-MM-dd "));
               // .Replace("{{FORMAT}}", weighing.LabelFormatId.ToString());

            for (int i = 0; i < weighing.NumberOfLabels; i++)
            {
                if (!RawPrinterHelper.SendStringToPrinter(_printerName, payload, out error))
                    return false;
            }

            error = null;
            return true;
        }

       
    }
}
