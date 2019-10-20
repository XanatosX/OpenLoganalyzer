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
    /// Interaction logic for FilterColumnControl.xaml
    /// </summary>
    public partial class FilterColumnControl : UserControl
    {
        public IFilterColumn Column => column;
        private readonly IFilterColumn column;

        public event EventHandler<EventArgs> Edit;

        public event EventHandler<EventArgs> Remove;

        public FilterColumnControl(ThemeManager themeManager, IFilterColumn filterColumn)
        {
            InitializeComponent();

            column = filterColumn;

            if (filterColumn != null)
            {
                TB_ColumnName.Text = filterColumn.Type;
            }
        }

        private void B_Edit_Click(object sender, RoutedEventArgs e)
        {
            EventHandler<EventArgs> handler = Edit;
            handler?.Invoke(this, null);
        }

        private void B_Remove_Click(object sender, RoutedEventArgs e)
        {
            EventHandler<EventArgs> handler = Remove;
            handler?.Invoke(this, null);
        }
    }
}
