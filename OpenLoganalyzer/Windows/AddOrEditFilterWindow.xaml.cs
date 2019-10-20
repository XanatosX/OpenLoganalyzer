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

        public IFilter Filter => filterToEdit;
        private IFilter filterToEdit;

        private ILogLineFilter currentFilterLine;
        private IFilterColumn currentColumn;

        private string oldFilterName;

        public AddOrEditFilterWindow(
            ISettings settings,
            IFilterManager filterManager,
            ThemeManager themeManager,
            IFilter filterToEdit
        ) {
            InitializeComponent();

            currentFilterLine = null;
            currentColumn = null;

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
            B_AddNewFilterLine.IsEnabled = true;

            foreach (ILogLineFilter logLineFilter in filterToEdit.LogLineTypes)
            {
                AddLine(logLineFilter);
            }
        }

        private void B_AddNewFilterLine_Click(object sender, RoutedEventArgs e)
        {
            AddLine(null);
            if (filterToEdit == null)
            {
                filterToEdit = new Filter(TB_FilterName.Text);
            }
            
        }

        private void NewControl_Created(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(AddFilterLineControl))
            {
                return;
            }
            AddFilterLineControl context = (AddFilterLineControl)sender;
            filterToEdit.AddFilter(context.LogLineFilter);
        }

        private void NewControl_Changed(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(AddFilterLineControl))
            {
                return;
            }
            AddFilterLineControl context = (AddFilterLineControl)sender;
            filterToEdit.RemoveFilterLineByName(context.OldName);
            filterToEdit.AddFilter(context.LogLineFilter);
        }

        private void NewControl_Remove(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(AddFilterLineControl))
            {
                return;
            }

            AddFilterLineControl context = (AddFilterLineControl)sender;
            if (context.LogLineFilter != null)
            {
                filterToEdit.RemoveFilterLineByName(context.LogLineFilter.Name);
            }
            
            SP_FilterLines.Children.Remove(context);
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
            SP_RegexLines.Children.RemoveRange(1, SP_RegexLines.Children.Count - 1);
            currentFilterLine = logLine;
            if (logLine == null)
            {
                return;
            }

            foreach (IFilterColumn column in logLine.FilterColumns)
            {
                AddColumn(column);
            }
        }

        private void B_AddColumn_Click(object sender, RoutedEventArgs e)
        {
            AddColumn(null);
        }

        private void ColumnControl_Changed(object sender, EventArgs e)
        {
            if (currentFilterLine == null)
            {
                return;
            }
            if (sender.GetType() != typeof(FilterColumnControl))
            {
                return;
            }
            FilterColumnControl context = (FilterColumnControl)sender;

            currentFilterLine.RemoveColumnByType(context.OldType);
            currentFilterLine.AddColumn(context.Column);
        }

        private void ColumnControl_Created(object sender, EventArgs e)
        {
            if (currentFilterLine == null)
            {
                return;
            }
            if (sender.GetType() != typeof(FilterColumnControl))
            {
                return;
            }
            FilterColumnControl context = (FilterColumnControl)sender;

            currentFilterLine.AddColumn(context.Column);
        }

        private void ColumnControl_Remove(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(FilterColumnControl))
            {
                return;
            }
            FilterColumnControl filterColumnControl = (FilterColumnControl)sender;
            currentFilterLine.RemoveColumnByType(filterColumnControl.Column.Type);
            SP_FilterColumn.Children.Remove(filterColumnControl);
            currentColumn = null;
        }

        private void ColumnControl_Edit(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(FilterColumnControl))
            {
                return;
            }
            FilterColumnControl filterColumnControl = (FilterColumnControl)sender;
            IFilterColumn column = filterColumnControl.Column;
            if (column == null)
            {
                B_AddRegex.IsEnabled = false;
                return;
            }
            currentColumn = column;
            B_AddRegex.IsEnabled = true;

            SP_RegexLines.Children.RemoveRange(1, SP_RegexLines.Children.Count - 1);
            foreach (string regex in column.PossibleRegex)
            {
                AddRegex(regex);
            }
        }

        private void TB_FilterName_LostFocus(object sender, RoutedEventArgs e)
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

            if (filterToEdit != null)
            {
                filterToEdit.RenameFilter(TB_FilterName.Text);
            }
        }

        private void B_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void B_Save_Click(object sender, RoutedEventArgs e)
        {
            if (!filterManager.Save(filterToEdit))
            {
                //Error Handling!
            }
        }

        private void B_AddRegex_Click(object sender, RoutedEventArgs e)
        {
            AddRegex("");
        }

        private void AddLine(ILogLineFilter lineToAdd)
        {
            AddFilterLineControl newControl = new AddFilterLineControl(themeManager, lineToAdd);
            newControl.Edit += NewControl_Edit;
            newControl.Remove += NewControl_Remove;
            newControl.Changed += NewControl_Changed;
            newControl.Created += NewControl_Created;

            Binding widthBinding = new Binding("Value") { ElementName = SP_FilterLines.Name };
            BindingOperations.SetBinding(newControl, WidthProperty, widthBinding);
            SP_FilterLines.Children.Add(newControl);
        }

        private void AddColumn(IFilterColumn columnToAdd)
        {
            FilterColumnControl columnControl = new FilterColumnControl(themeManager, columnToAdd);
            columnControl.Edit += ColumnControl_Edit; ;
            columnControl.Remove += ColumnControl_Remove;
            columnControl.Changed += ColumnControl_Changed;
            columnControl.Created += ColumnControl_Created;

            Binding widthBinding = new Binding("Value") { ElementName = SP_FilterColumn.Name };
            BindingOperations.SetBinding(columnControl, WidthProperty, widthBinding);
            SP_FilterColumn.Children.Add(columnControl);
        }

        private void AddRegex(string regexToAdd)
        {
            TextBox box = new TextBox();
            box.LostFocus += Box_LostFocus;
            box.Text = regexToAdd;
            Binding widthBinding = new Binding("Value") { ElementName = SP_FilterColumn.Name };
            BindingOperations.SetBinding(box, WidthProperty, widthBinding);
            SP_RegexLines.Children.Add(box);
        }

        private void Box_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(TextBox))
            {
                return;
            }
            TextBox regexBox = new TextBox();
            if (regexBox.Text == "" || currentColumn == null)
            {
                currentColumn.Reset();
                List<TextBox> boxesToRemove = new List<TextBox>();
                foreach (Control control in SP_RegexLines.Children)
                {
                    if (control.GetType() != typeof(TextBox))
                    {
                        continue;
                    }
                    TextBox currentBox = (TextBox)control;
                    if (currentBox.Text == "")
                    {
                        boxesToRemove.Add(currentBox);
                        continue;
                    }
                    currentColumn.addNewRegex(currentBox.Text);
                    
                }
                foreach (TextBox textBoxToRemove in boxesToRemove)
                {
                    SP_RegexLines.Children.Remove(textBoxToRemove);
                }
                return;
            }

            currentColumn.addNewRegex(regexBox.Text);
        }
    }
}
