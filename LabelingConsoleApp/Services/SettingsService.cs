using LabelingConsole.Domain;

namespace LabelingConsole.Services
{
    public class SettingsService
    {
        private readonly MenuService _menuService;

        public SettingsService(MenuService menuService)
        {
            _menuService = menuService;
        }

        public void ChangeLabelSettings(WeighingSettings settings, List<LabelFormat> labelFormats)
        {
            Console.WriteLine(
                $"Obecne ustawienia etykiet: Format etykiety ID: " +
                $"{settings.LabelFormatId}, " +
                $"Liczba etykiet: {settings.NumberOfLabels}" +
                $" \n Tryb ważenia: {settings.WeighingMode}");

            bool isChangingSettings = true;
            while (isChangingSettings)
            {
                _menuService.DisplayLabelSettingsSubMenu();
                string? userInput = Console.ReadLine()?.Trim().ToUpper();

                switch (userInput)
                {
                    case "1":
                        while (true)
                        {
                            _menuService.DisplayLabelFormats(labelFormats);
                            string? newLabelFormatInput = Console.ReadLine()?.Trim();

                            if (InputValidationService.TryReadLabelFormatId(newLabelFormatInput, labelFormats, out int newLabelFormatId))
                            {
                                settings.LabelFormatId = newLabelFormatId;
                                Console.WriteLine(
                                    newLabelFormatId == 0
                                        ? "Bez etykiety"
                                        : $"Ustawiono nowy format etykiety: {settings.LabelFormatId}");
                                break;
                            }
                        }
                        break;

                    case "2":
                        while (true)
                        {
                            Console.WriteLine("Podaj nową liczbę etykiet: ");
                            string? newNumberOfLabelsInput = Console.ReadLine()?.Trim();

                            if (InputValidationService.TryReadPositiveNumberOfLabels(newNumberOfLabelsInput, out int newNumberOfLabels))
                            {
                                settings.NumberOfLabels = newNumberOfLabels;
                                Console.WriteLine($"Ustawiono nową liczbę etykiet: {settings.NumberOfLabels}");
                                break;
                            }
                        }
                        break;
                
                    case "3":
                            while (true)
                            {
                            _menuService.DisplayWeighingModeOptions();
                            string? newWeighingModeInput = Console.ReadLine()?.Trim();
                            if (newWeighingModeInput == "1" || newWeighingModeInput == "2")
                            {
                                settings.WeighingMode = newWeighingModeInput == "1" ? EWeighingMode.Manual : EWeighingMode.Auto;
                                Console.WriteLine($"Ustawiono nowy tryb ważenia: {settings.WeighingMode}");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.");
                            }
                        }
                        break;

                    case "Q":
                        Console.WriteLine($"  Aktualna Ilość etykiet {settings.NumberOfLabels}," +
                            $"Aktualny format : {settings.LabelFormatId} ");
                        isChangingSettings = false;
                        break;

                    default:
                        Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.");
                        break;
                }
            }
        }
    }
}
