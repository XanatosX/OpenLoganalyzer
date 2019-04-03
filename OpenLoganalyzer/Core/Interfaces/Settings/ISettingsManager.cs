using OpenLoganalyzer.Core.Interfaces.ErrorManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core.Interfaces
{
    public interface ISettingsManager : IError
    {
        string FilePath { get; }

        ISettings Load();

        bool Save(ISettings settings);
    }
}
