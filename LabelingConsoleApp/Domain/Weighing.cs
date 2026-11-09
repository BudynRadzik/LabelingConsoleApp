namespace LabelingConsole.Domain
{
    public class Weighing
    {
        public int Id { get; set; }
        public decimal Weight { get; set; }
        public int LabelFormatId { get; set; }
        public int NumberOfLabels { get; set; }
        public DateTime WeighedAt { get; set; } = DateTime.Now;

        public Weighing(decimal weight, int labelFormatId, int numberOfLabels)
        {
            Weight = weight;
            LabelFormatId = labelFormatId;
            NumberOfLabels = numberOfLabels;
        }

    }
}
