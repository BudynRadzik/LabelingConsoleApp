using LabelingConsole.Abstractions;
namespace LabelingConsole.Services
{
    internal class ManualWeighingSource : IWeightSource
    {
        public bool TryReadWeight(out decimal weight, out string? error)
        {
            Console.Write("Wprowadź wagę (kg): ");
            string? input = Console.ReadLine();
            if (decimal.TryParse(input, out weight))
            {
                error = null;
                return true;
            }
            else
            {
                error = "Nieprawidłowa wartość wagi.";
                return false;
            }

        }
    }
}
