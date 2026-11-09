using LabelingConsole.Domain;

namespace LabelingConsole.Services
{
    public class MenuService
    {
        public void DisplayMenu()
        {
            Console.WriteLine("=== MENU ETYKIETOWANIA ===");
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("1. Nowe ważenie");
            Console.WriteLine("2. Wyświetl historię ważeń");
            Console.WriteLine("3. Wyświetl podsumowanie");
            Console.WriteLine("4. Zmień ustawienia etykiet");
            Console.WriteLine("Q. Zakończ program");
        }
        public void DisplayLabelSettingsSubMenu()
        {
            Console.WriteLine("=== USTAWIENIA ETYKIET ===");
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("1. Zmień format etykiety");
            Console.WriteLine("2. Zmień liczbę etykiet");
            Console.WriteLine("3. Zmień tryb ważenia (Manualny/Automatyczny)");
            Console.WriteLine("Q. Powrót do głównego menu");
        }
        public void DisplayLabelFormats(List<LabelFormat> labelFormats)
        {
            Console.WriteLine("Dostępne formaty etykiet:");
            Console.WriteLine("0 - ważenie bez wydruku etykiet");
            foreach (var format in labelFormats)
            {
                Console.WriteLine($"Id: {format.Id}, Nazwa: {format.Name}, Ścieżka szablonu: {format.TemplatePath}");
            }
        }
        public void DisplayWeighingModeOptions()
        {
            Console.WriteLine("Wybierz tryb ważenia:");
            Console.WriteLine("1. Tryb Manualny");
            Console.WriteLine("2. Tryb Automatyczny");
        } 

    }
}
