
using LabelingConsole.Domain;

namespace LabelingConsole.Abstractions
{
    public interface IWeightSource
    {
        bool TryReadWeight(out decimal weight, out string? error);
    }
}
