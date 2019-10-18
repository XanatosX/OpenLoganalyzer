using OpenLoganalyzerLib.Core.Interfaces.Loader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Loader
{
    public class StreamFileLoader : ILoader
    {
        private string filePath;
        private bool configured;

        public StreamFileLoader()
        {
            filePath = "";
            configured = false;
        }

        public void Init(string loaderInformation)
        {
            filePath = loaderInformation;
            configured = true;
        }

        public IEnumerable<string> Load()
        {
            if (!configured)
            {
                yield return default;
            }

            if (!File.Exists(filePath))
            {
                yield return  default;
            }

            StreamReader reader = new StreamReader(filePath);
            string line = "";

            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }

            reader.Close();
        }
    }
}
