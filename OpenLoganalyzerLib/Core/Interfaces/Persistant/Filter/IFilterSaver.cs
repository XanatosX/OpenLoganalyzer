using OpenLoganalyzerLib.Core.Interfaces.Configuration;

namespace OpenLoganalyzerLib.Core.Interfaces.Persistant.Filter
{
    public interface IFilterSaver
    {
        bool Save(IFilter filterToSave);

        void Delete(IFilter filterToRemove);
    }
}
