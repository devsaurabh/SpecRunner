using NSpec.Domain.Formatters;

namespace NSpecRunner.Core.Domain.Formatters
{
    public interface IFormatterWithOutput : IFormatter
    {
        string GetFormattedString { get; }
    }
}
