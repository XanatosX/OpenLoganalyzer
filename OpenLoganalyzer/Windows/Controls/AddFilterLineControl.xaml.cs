using OpenLoganalyzer.Core.Style;
using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OpenLoganalyzer.Windows.Controls
{
    /// <summary>
    /// Interaction logic for AddFilterLineControl.xaml
    /// </summary>
    public partial class AddFilterLineControl : UserControl
    {
        public ILogLineFilter LogLineFilter => logLineFilter;
        private readonly ILogLineFilter logLineFilter;

        public event EventHandler<EventArgs> Edit;

        public event EventHandler<EventArgs> Remove;


        private readonly ThemeManager themeManager;

        public AddFilterLineControl(ThemeManager themeManager, ILogLineFilter logLineFilter)
        {
            InitializeComponent();

            this.logLineFilter = logLineFilter;

            if (logLineFilter != null)
            {
                TB_FilterLineName.Text = logLineFilter.Name;             
            }

            this.themeManager = themeManager;
        }

        private void B_Edit_Click(object sender, RoutedEventArgs e)
        {
            EventHandler<EventArgs> handler = Edit;
            handler?.Invoke(this, new EventArgs());
        }

        private void B_Remove_Click(object sender, RoutedEventArgs e)
        {
            EventHandler<EventArgs> handler = Remove;
            handler?.Invoke(this, new EventArgs());
        }
    }
}
