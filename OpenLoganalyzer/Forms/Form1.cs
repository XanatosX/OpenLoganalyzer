using OpenLoganalyzerLib.Core.Enum;
using OpenLoganalyzerLib.Core.Factories;
using OpenLoganalyzerLib.Core.Interfaces;
using OpenLoganalyzerLib.Core.LoaderCofiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenLoganalyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ILoaderConfigurationLoader configLoader = new SimpleConfigurationLoader();

            ILoaderConfiguration configuration = null;

            if (File.Exists(@"F:\Onedrive\Work share\config.json"))
            {
                configuration = configLoader.Load(@"F:\Onedrive\Work share\config.json");

            }
            else
            {
                Dictionary<string, string> filters = new Dictionary<string, string>();
                filters.Add(FilterTypeEnum.Severity.ToString(), "\\[([A-Z]*)\\]");
                filters.Add(FilterTypeEnum.Caller.ToString(), "(^[A-Z][a-zA-z\\/]*) \\[[A-Z]*\\]");
                filters.Add(FilterTypeEnum.Datetime.ToString(), "-> ([0-9]{2}:[0-9]{2}:[0-9]{2}): ");
                filters.Add(FilterTypeEnum.Message.ToString(), ": ([a-zA-Z][a-zA-Z:\\\\/ !0-9-]*)");


                Dictionary<string, string> additionalSettings = new Dictionary<string, string>();
                additionalSettings.Add(AdditionalSettingsEnum.FilePath.ToString(), @"F:\Onedrive\Work share\output.log");
                additionalSettings.Add(AdditionalSettingsEnum.DateTimeFormat.ToString(), "HH':'mm':'ss");


                configuration = new SimpleConfiguration(LoaderTypeEnum.FileLoader, filters, additionalSettings);
            }
           

            ILoaderFactory loader = new LoaderFactory();
            ILoader realLoader = loader.GetLoader(configuration);
            
            

            List<ILogLine> lines = realLoader.Load();

            configLoader.Save(configuration, @"F:\Onedrive\Work share\config.json");

        }
    }
}
