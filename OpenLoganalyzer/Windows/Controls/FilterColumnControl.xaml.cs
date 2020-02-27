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
    /// Interaction logic for LogLineControl.xaml
    /// </summary>
    public partial class FilterColumnControl : UserControl
    {
        private TreeViewItem item;
        public TreeViewItem Item => item;

        private readonly IFilterColumn logLine;
        public IFilterColumn LogLine => logLine;

        private bool addMode;

        public FilterColumnControl(IFilterColumn logLine)
        {
            InitializeComponent();
            foreach (string regex in logLine.PossibleRegex)
            {
                ListViewItem item = new ListViewItem
                {
                    Content = regex
                };
                LV_RegexView.Items.Add(item);
            }

            this.logLine = logLine;
            LV_RegexView.SelectionChanged += LV_RegexView_SelectionChanged;
            LV_RegexView.MouseLeftButtonDown += LV_RegexView_MouseLeftButtonDown;

            TB_Regex.Text = string.Empty;
        }

        private void LV_RegexView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView)
            {
                ListView listView = (ListView)sender;
                listView.SelectedItem = null;
                listView.Focus();
            }
        }

        private void LV_RegexView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LV_RegexView.SelectedValue != null)
            {
                B_Remove.IsEnabled = true;
                B_SaveEdit.Visibility = Visibility.Visible;
                TB_Regex.Visibility = Visibility.Visible;
                TB_Regex.Text = ((ListViewItem)LV_RegexView.SelectedItem).Content.ToString();
            }
        }

        private void B_Add_Click(object sender, RoutedEventArgs e)
        {
            B_SaveEdit.Visibility = Visibility.Visible;
            TB_Regex.Visibility = Visibility.Visible;
            addMode = true;
            TB_Regex.Text = "New regex";
            TB_Regex.Focus();
            TB_Regex.SelectAll();
        }

        private void B_SaveEdit_Click(object sender, RoutedEventArgs e)
        {
            B_SaveEdit.Visibility = Visibility.Hidden;
            TB_Regex.Visibility = Visibility.Hidden;

            if (addMode)
            {
                ListViewItem newItem = new ListViewItem
                {
                    Content = TB_Regex.Text
                };
                LV_RegexView.Items.Add(newItem);
                addMode = false;
                return;
            }

            ListViewItem item = (ListViewItem)LV_RegexView.SelectedItem;
            item.Content = TB_Regex.Text;

            TB_Regex.Text = string.Empty;
            LV_RegexView.SelectedItem = null;
        }

        private void B_Remove_Click(object sender, RoutedEventArgs e)
        {
            if (LV_RegexView.SelectedValue == null)
            {
                return;
            }
            ListViewItem item = (ListViewItem)LV_RegexView.SelectedItem;
            logLine.RemoveRegex(item.Content.ToString());
            LV_RegexView.Items.Remove(LV_RegexView.SelectedItem);
        }
    }
}
