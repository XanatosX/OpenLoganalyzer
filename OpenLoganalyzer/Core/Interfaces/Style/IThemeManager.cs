using System.Collections.Generic;

namespace OpenLoganalyzer.Core.Interfaces.Style
{
    public interface IThemeManager
    {
        List<IStyleDict> Styles { get; }

        IStyleDict GetThemeByName(string Name);
        void ScanFolder(string FolderPath);
    }
}