namespace LabelingConsole.Domain
{
    public class WeighingSettings
    {
        public int  LabelFormatId { get; set; }= 0;
        public int NumberOfLabels { get; set; } = 1;
        public EWeighingMode WeighingMode { get; set; } = EWeighingMode.Manual;

    }
}
