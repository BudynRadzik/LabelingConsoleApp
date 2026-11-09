using LabelingConsole.Abstractions;
using LabelingConsole.Domain;

namespace LabelingConsole.Services
{
    public class WeighingService
    {
        private readonly IWeightSource _weightSource;
        private readonly ILabelPrinter _labelPrinter;
        private readonly IAppOutput _output;
        private readonly IAppInput _input;

        private const int AutoPollingDelayMs = 100;
        public WeighingService(IWeightSource weightSource, ILabelPrinter labelPrinter, IAppOutput output, IAppInput input)
        {
           _weightSource = weightSource;
           _labelPrinter = labelPrinter;
           _output = output;
           _input = input;
        }

        public void ManualWeighing(WeighingSettings settings, List<Weighing> weighings, List<LabelFormat> labelFormats)
        {
            if (!_weightSource.TryReadWeight(out decimal measuredWeight, out string? error))
            {
                _output.Error(error?? "Nieznany błąd odczytu wagi.");
                return;
            }

            if (measuredWeight <= 0)
            {
                return;
            }            

            if (!TryGetSelectedFormat(settings, labelFormats, out var selectedFormat, out var errorFormat))
            {           
                _output.Error(errorFormat?? "Nieznany błąd wyboru formatu etykiety.");
                return;
            }
            ProcessWeight(measuredWeight, settings.LabelFormatId, settings.NumberOfLabels, weighings, selectedFormat?.TemplatePath ?? string.Empty);
        }

        public void AutoWeighing(WeighingSettings settings, List<Weighing> weighings, List<LabelFormat> labelFormats)
        {
           if (!TryGetSelectedFormat(settings, labelFormats, out var selectedFormat, out var errorFormat))
           {
                _output.Error(errorFormat?? "Nieznany błąd wyboru formatu etykiety.");
                return;
           }

            _output.Info("Tryb automatyczny uruchomiony. Naciśnij Q, aby zakończyć.");

            while (true)
            {
                if (_input.IsExitRequested())
                {
                    break;
                }

                if (!_weightSource.TryReadWeight(out decimal measuredWeight, out string? error))
                {
                    _output.Error(error?? "Nieznany błąd odczytu wagi.");
                    Thread.Sleep(AutoPollingDelayMs);
                    continue;
                }

                if (measuredWeight <= 0)
                {
                    Thread.Sleep(AutoPollingDelayMs);
                    continue; // zero = brak produktu, ignorujemy
                }

                _output.Info($"Odczyt z wagi: {measuredWeight:0.###} kg");

                ProcessWeight(
                    measuredWeight,
                    settings.LabelFormatId,
                    settings.NumberOfLabels,
                    weighings,
                    selectedFormat?.TemplatePath ?? string.Empty);
                Thread.Sleep(AutoPollingDelayMs);
            }
        }
        private void ProcessWeight(decimal measuredWeight, int labelFormatId, int numberOfLabels, List<Weighing> weighings, string templatePath)
        {
            if (measuredWeight <= 0)
            {
                _output.Info($"Czekam na produkt.");
                return;
            }
            var weighing = new Weighing(measuredWeight, labelFormatId, numberOfLabels);
            weighing.Id = weighings.Count + 1;
            weighings.Add(weighing);
            _output.Info($"Waga: {weighing.Weight} kg  ");
            _output.Info($"Liczba etykiet: {weighing.NumberOfLabels}  ");
            _output.Info($"Format etykiety: {weighing.LabelFormatId} \n ");
            if (!_labelPrinter.TryPrintLabel(weighing,templatePath, out var printError))
            {
                _output.Error(printError?? "Nieznany błąd drukowania etykiety.");
            }
        }
        private bool TryGetSelectedFormat(WeighingSettings settings, List<LabelFormat> labelFormats, out LabelFormat? selectedFormat,out string? error)
        {
            selectedFormat = labelFormats.FirstOrDefault(f => f.Id == settings.LabelFormatId);
            if (settings.LabelFormatId != 0 && selectedFormat is null)
            {
                error = $"Nie znaleziono formatu etykiety o ID: {settings.LabelFormatId}";
                return false;
            }
                error = null;
                return true;
            
           
            
        }
    }
}
