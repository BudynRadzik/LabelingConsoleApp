using LabelingConsole.Domain;

namespace LabelingConsole.Services
{
    public static class InputValidationService
    {
        public static bool TryReadLabelFormatId(string? input, List<LabelFormat> labelFormats, out int labelFormatId)
        {
            if (!int.TryParse(input, out labelFormatId))
            {
                Console.WriteLine("Format etykiety musi byæ liczb¹. Spróbuj ponownie.");
                return false;
            }
            int parsedLabelFormatId = labelFormatId;
            if (labelFormatId != 0 && !labelFormats.Any(f => f.Id == parsedLabelFormatId))
            {
                Console.WriteLine($"Format etykiety o id {parsedLabelFormatId} nie istnieje. Spróbuj ponownie.");
                return false;
            }

            return true;
        }

        public static bool TryReadPositiveNumberOfLabels(string? input, out int numberOfLabels)
        {
            if (!int.TryParse(input, out numberOfLabels) || numberOfLabels <= 0)
            {
                Console.WriteLine("Nieprawid³owa liczba etykiet. Sprobuj ponownie.");
                return false;
            }

            return true;
        }
    }
}