

using LabelingConsole.Domain;

namespace LabelingConsole.Services
{
    public class WeighingsHistoryService
    {

        public void DisplayWeighings(List<Weighing> weighings)
        {
            if (weighings.Count == 0)
            {
                Console.WriteLine("Brak ważeń");
            }
            else
            {
                Console.WriteLine("Ważenia:");
                foreach (var weighing in weighings)
                {
                    Console.WriteLine(
                        $"Id: {weighing.Id}, " +
                        $"Waga: {weighing.Weight} kg, " +
                        $"Format etykiety: {weighing.LabelFormatId}, " +
                        $"Liczba etykiet: {weighing.NumberOfLabels}, " +
                        $"Ważenie wykonano: {weighing.WeighedAt.ToString("yyyy-MM-dd HH:mm:ss")}");
                    Console.WriteLine("--------------------------------------------------");
                }
            }
        }
        public void DisplaySummary(List<Weighing> weighings)
        {
            decimal totalWeight = weighings.Sum(w => w.Weight);
            Console.WriteLine($"Liczba wszystkich ważonych przedmiotów: {weighings.Count}");
            Console.WriteLine($"Łączna waga wszystkich ważonych przedmiotów: {totalWeight}");

        }
    }
}
