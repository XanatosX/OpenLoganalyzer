using OpenLoganalyzer.Core.Interfaces;
using OpenLoganalyzer.Core.Style;
using OpenLoganalyzer.Windows.Controls;
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
using System.Windows.Shapes;

namespace OpenLoganalyzer.Windows
{
    /// <summary>
    /// Interaction logic for AddOrEditFilterWindow.xaml
    /// </summary>
    public partial class AddOrEditFilterWindow : Window
    {
        private readonly ISettings settings;
        private readonly IFilterManager filterManager;
        private readonly ThemeManager themeManager;
        private IFilter filterToEdit;

        private string oldFilterName;

        public AddOrEditFilterWindow(
            ISettings settings,
            IFilterManager filterManager,
            ThemeManager themeManager,
            IFilter filterToEdit
        ) {
            InitializeComponent();

            this.settings = settings;
            this.filterManager = filterManager;
            this.themeManager = themeManager;
            this.filterToEdit = filterToEdit;

            if (filterToEdit != null)
            {
                SetupEditFilter();
            }
        }

        private void SetupEditFilter()
        {
            oldFilterName = filterToEdit.Name;
            TB_FilterName.Text = oldFilterName;

            foreach (ILogLineFilter logLineFilter in filterToEdit.LogLineTypes)
            {
                Control newControl = new AddFilterLineControl(themeManager, logLineFilter);
                Binding widthBinding = new Binding("Value") { ElementName = SP_FilterLines.Name };
                BindingOperations.SetBinding(newControl, WidthProperty, widthBinding);

                SP_FilterLines.Children.Add(newControl);
            }
        }

        private void B_AddNewFilterLine_Click(object sender, RoutedEventArgs e)
        {
            AddFilterLineControl newControl = new AddFilterLineControl(themeManager, null);
            newControl.Edit += NewControl_Edit;
            newControl.Remove += NewControl_Remove;
            Binding widthBinding = new Binding("Value") { ElementName = SP_FilterLines.Name };
            BindingOperations.SetBinding(newControl, WidthProperty, widthBinding);

            SP_FilterLines.Children.Add(newControl);
        }

        private void NewControl_Remove(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(AddFilterLineControl))
            {
                return;
            }
            SP_FilterLines.Children.Remove((AddFilterLineControl)sender);
            B_AddColumn.IsEnabled = false;
        }

        private void NewControl_Edit(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(AddFilterLineControl))
            {
                return;
            }

            AddFilterLineControl addFilterLineControl = (AddFilterLineControl)sender;
            ILogLineFilter logLine = addFilterLineControl.LogLineFilter;
            B_AddColumn.IsEnabled = true;
            SP_FilterColumn.Children.RemoveRange(1, SP_FilterColumn.Children.Count - 1);
            if (logLine == null)
            {
                return;
            }

            foreach (IFilterColumn column in logLine.FilterColumns)
            {
                FilterColumnControl newControl = new FilterColumnControl(themeManager, column);
                //newControl.Edit += NewControl_Edit;
                //newControl.Remove += NewControl_Remove;
                Binding widthBinding = new Binding("Value") { ElementName = SP_FilterColumn.Name };
                BindingOperations.SetBinding(newControl, WidthProperty, widthBinding);
            }
        }

        private void B_AddColumn_Click(object sender, RoutedEventArgs e)
        {
            FilterColumnControl columnControl = new FilterColumnControl(themeManager, null);
            columnControl.Edit += ColumnControl_Edit; ;
            columnControl.Remove += ColumnControl_Remove;
            Binding widthBinding = new Binding("Value") { ElementName = SP_FilterColumn.Name };
            BindingOperations.SetBinding(columnControl, WidthProperty, widthBinding);
            SP_FilterColumn.Children.Add(columnControl);
        }

        private void ColumnControl_Remove(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(FilterColumnControl))
            {
                return;
            }
            SP_FilterColumn.Children.Remove((FilterColumnControl)sender);
        }

        private void ColumnControl_Edit(object sender, EventArgs e)
        {
            
        }

        private void TB_FilterName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender.GetType() != typeof(TextBox))
            {
                return;
            }

            TextBox box = (TextBox)sender;

            if (box.Text == "")
            {
                B_AddNewFilterLine.IsEnabled = false;
                return;
            }

            B_AddNewFilterLine.IsEnabled = true;

        }
    }
}
