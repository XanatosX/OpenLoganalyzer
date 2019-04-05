using System.Windows;

namespace OpenLoganalyzer.Core.Interfaces.Style
{
    public interface IStyleDict
    {
        ResourceDictionary Dictionary { get; }
        string Name { get; }
    }
}