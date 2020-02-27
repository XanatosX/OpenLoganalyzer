using OpenLoganalyzerLib.Core.Interfaces.Configuration;

namespace OpenLoganalyzerLib.Core.Interfaces.Factories
{
    public interface IFilterFactory
    {
        IFilterManager GetFilterManager();
    }
}
