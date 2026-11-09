namespace LabelingConsole.Services;
    using LabelingConsole.Domain;

    public class AppControllerService
    {
        private readonly WeighingsHistoryService _weighingsHistoryService;

        public AppControllerService(WeighingsHistoryService weighingsHistoryService)
        {
            _weighingsHistoryService = weighingsHistoryService;
        }
        public bool HandleMenuChoice(
        string? userInput,
        WeighingSettings settings,
        List<Weighing> weighings,
        List<LabelFormat> labelFormats,
        WeighingService manualWeighingService,
        WeighingService automaticWeighingService,
        SettingsService settingsService)
        {
            switch (userInput)
            {
                case "1":
                if (settings.WeighingMode == EWeighingMode.Manual)
                {
                    manualWeighingService.ManualWeighing(settings, weighings, labelFormats);
                }
                else
                {
                    automaticWeighingService.AutoWeighing(settings, weighings, labelFormats);
                }
                return true;
                case "2":
                    _weighingsHistoryService.DisplayWeighings(weighings);
                    return true;
                case "3":
                    _weighingsHistoryService.DisplaySummary(weighings);
                    return true;
                case "4":
                    settingsService.ChangeLabelSettings(settings, labelFormats);
                    return true;
                case "Q":
                    return false;
                default:
                    Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.");
                    return true;
            }
        }
    }

