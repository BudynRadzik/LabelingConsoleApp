using LabelingConsole.Abstractions;
using LabelingConsole.Domain;
using LabelingConsole.Infrastructure.Console;
using LabelingConsole.Infrastructure.Printing;
using LabelingConsole.Infrastructure.Scale;
using LabelingConsole.Services;
using System.IO.Ports;

class Program
{
    static void Main(string[] args)
    {
        var labelFormats = new List<LabelFormat>
        {
            new LabelFormat { Id = 1, Name = "Standard 58x40", TemplatePath = "labels/58x40.epl" },
            new LabelFormat { Id = 2, Name = "Duża 100x70", TemplatePath = "labels/etykieta testowa1.prn" }
        };

        var menuService = new MenuService();
        var serialPortSettings = new SerialPortSettings("COM4");
        var initializeSerialPort = new InitializeSerialPort();
        //var labelPrinterService = new LabelPrinterService();
        var appOutput = new ConsoleAppOutput();
        var appInput = new ConsoleAppInput();
        var labelPrinterService = new ZebraUsbLabelPrinter("ZDesigner LP 2844");

        var manualWeighingService = new WeighingService(
            new ManualWeighingSource(),
            labelPrinterService,
            appOutput,
            appInput);
        var automaticWeighingService = new WeighingService(
           new SerialPortWeighingSource(initializeSerialPort, serialPortSettings),
           labelPrinterService,
           appOutput,
           appInput);

        var settingsService = new SettingsService(menuService); 
        var weighingsHistoryService = new WeighingsHistoryService();
        var appControllerService = new AppControllerService(weighingsHistoryService);
        

        Console.WriteLine("=== APLIKACJA ETYKIETOWANIA ===");

        int labelFormatId = ReadLabelFormatId(menuService, labelFormats);

        var settings = new WeighingSettings
        {
            LabelFormatId = labelFormatId,
        };

        var weighings = new List<Weighing>();
        bool isRunning = true;

        while (isRunning)
        {
            menuService.DisplayMenu();
            string? userInput = Console.ReadLine()?.Trim().ToUpper();

            isRunning = appControllerService.HandleMenuChoice(
                userInput,
                settings,
                weighings,
                labelFormats,
                manualWeighingService,
                automaticWeighingService,
                settingsService);
        }
    }

    private static int ReadLabelFormatId(MenuService menuService, List<LabelFormat> labelFormats)
    {
        while (true)
        {
            menuService.DisplayLabelFormats(labelFormats);
            Console.Write("Wybrany format etykiety (podaj numer): ");
            string? labelFormatInput = Console.ReadLine();

            if (InputValidationService.TryReadLabelFormatId(labelFormatInput, labelFormats, out int labelFormatId))
            {
                return labelFormatId;
            }
        }
    }
}


