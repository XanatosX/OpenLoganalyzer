using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core.Interfaces.Adapter
{
    public interface IFilterAdapter
    {
        void SetName(string newName);
        string GetName();
    }
}
