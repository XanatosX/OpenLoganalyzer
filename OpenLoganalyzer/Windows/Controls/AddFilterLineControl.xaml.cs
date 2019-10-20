using OpenLoganalyzer.Core.Style;
using OpenLoganalyzerLib.Core.Configuration;
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
        private ILogLineFilter logLineFilter;

        public string OldName => oldName;
        private string oldName;

        public event EventHandler<EventArgs> Created;

        public event EventHandler<EventArgs> Changed;

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
                oldName = logLineFilter.Name;
                B_Edit.IsEnabled = true;
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

        private void TB_FilterLineName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(TextBox))
            {
                return;
            }

            TextBox box = (TextBox)sender;

            if (box.Text == "")
            {
                B_Edit.IsEnabled = false;
                return;
            }

            EventHandler<EventArgs> handler = null;
            if (logLineFilter != null)
            {
                oldName = logLineFilter.Name;
                logLineFilter.RenameColumn(TB_FilterLineName.Text);
                handler = Changed;
                handler?.Invoke(this, new EventArgs());
                return;
            }

            logLineFilter = new FilterLine(TB_FilterLineName.Text);
            B_Edit.IsEnabled = true;
            handler = Created;
            handler?.Invoke(this, new EventArgs());
        }
    }
}
