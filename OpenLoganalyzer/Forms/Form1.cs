using OpenLoganalyzer.Core.Enum;
using OpenLoganalyzer.Core.Factories;
using OpenLoganalyzer.Core.Interfaces;
using OpenLoganalyzer.Core.LoaderCofiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            

            Dictionary<string, string> filters = new Dictionary<string, string>();
            filters.Add(FilterTypeEnum.Caller.ToString(), "^[A-Z][a-zA-z\\/]* ");
            filters.Add(FilterTypeEnum.Datetime.ToString(), "-> [0-9]{2}:[0-9]{2}:[0-9]{2}:");
            filters.Add(FilterTypeEnum.Message.ToString(), " [a-zA-Z][a-zA-Z:\\\\/ !0-9-]*");
            filters.Add(FilterTypeEnum.Severity.ToString(), "INFO|DEBUG|WARNING|ERROR|CRITICAL");

            Dictionary<string, string> additionalSettings = new Dictionary<string, string>();
            additionalSettings.Add(AdditionalSettingsEnum.FilePath.ToString(), @"F:\Onedrive\Work share\output.log");
            additionalSettings.Add(AdditionalSettingsEnum.DateTimeFormat.ToString(), "'->' HH':'mm':'ss':'");


            ILoaderConfiguration configuration = new SimpleConfiguration(Core.Enum.LoaderTypeEnum.FileLoader, filters, additionalSettings);

            ILoaderFactory loader = new LoaderFactory();
            ILoader realLoder = loader.GetLoader(configuration);


            List<ILogLine> lines = realLoder.Load();

            configLoader.Save(configuration, @"F:\Onedrive\Work share\config.json");

        }
    }
}
