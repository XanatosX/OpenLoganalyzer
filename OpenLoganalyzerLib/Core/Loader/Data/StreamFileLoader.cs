using OpenLoganalyzerLib.Core.Interfaces.Persistant.Analyzing;
using System.Collections.Generic;
using System.IO;

namespace OpenLoganalyzerLib.Core.Loader.Data
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
                if (line == "")
                {
                    continue;
                }
                yield return line;
            }

            reader.Close();
        }
    }
}
