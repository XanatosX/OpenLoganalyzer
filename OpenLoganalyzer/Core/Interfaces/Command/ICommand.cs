using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core.Interfaces.Command
{
    public interface ICommand
    {
        bool Execute();

        Task<bool> AsyncExecute();
    }
}
