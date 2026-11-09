using LabelingConsole.Domain;
using System.Globalization;
using System.Text;

namespace LabelingConsole.Infrastructure.Printing
{
    public static class EplTemplateRenderer
    {
        

        public static string RenderWeighingLabel(Weighing weighing)
        {
            string weight = weighing.Weight.ToString("0.###", CultureInfo.InvariantCulture);
            string date = weighing.WeighedAt.ToString("yyyy-MM-dd HH:mm:ss");

            var sb = new StringBuilder();
            sb.AppendLine("N");
            sb.AppendLine($"A20,20,0,4,1,1,N,\"Waga: {weight} \"");
            sb.AppendLine($"A20,60,0,3,1,1,N,\"Format ID: {weighing.LabelFormatId}\"");
            sb.AppendLine($"A20,95,0,2,1,1,N,\"{date}\"");
            sb.AppendLine("P1");
            return sb.ToString();
        }
    }
}
