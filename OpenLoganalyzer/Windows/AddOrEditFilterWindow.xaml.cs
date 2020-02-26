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

        private bool subSelected;

        private UserControl currentControl;

        public AddOrEditFilterWindow(
            ISettings settings,
            IFilterManager filterManager,
            ThemeManager themeManager
        )
            : this(settings, filterManager, themeManager, null)
        {
        }

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

            SetupFilters();
            currentControl = null;
        }

        private void SetupFilters()
        {
            foreach (string filterName in filterManager.GetAvailableFilterNames())
            {
                TreeViewItem viewItem = new TreeViewItem();
                viewItem.Header = filterName;
                viewItem.Selected += ViewItem_Selected;
                IFilter filter = filterManager.LoadFilterByName(filterName);
                viewItem.Tag = filter;

                if (filter == null)
                {
                    return;
                }

                foreach (ILogLineFilter logLineFilter in filter.LogLineTypes)
                {
                    TreeViewItem subViewItem = new TreeViewItem();
                    subViewItem.Tag = logLineFilter;
                    subViewItem.Selected += SubViewItem_Selected; ;
                    subViewItem.Header = logLineFilter.Name;
                    viewItem.Items.Add(subViewItem);
                    foreach (IFilterColumn column in logLineFilter.FilterColumns)
                    {
                        TreeViewItem filterColumnItem = new TreeViewItem();
                        filterColumnItem.Tag = column;
                        //filterColumnItem.Selected += SubViewItem_Selected; ;
                        filterColumnItem.Header = column.Type;

                        subViewItem.Items.Add(filterColumnItem);
                    }
                }

                if (filter == filterToEdit)
                {
                    viewItem.IsSelected = true;
                    viewItem.IsExpanded = true;
                }
                TV_LogOverview.Items.Add(viewItem);

            }
        }

        private void SubViewItem_Selected(object sender, RoutedEventArgs e)
        {
            subSelected = true;
            ItemSelectedForRename((TreeViewItem)sender, "Logline name:");
            
        }

        private void ViewItem_Selected(object sender, RoutedEventArgs e)
        {
            if (subSelected)
            {
                subSelected = false;
                return;
            }
            ItemSelectedForRename((TreeViewItem)sender, "Filter name:");
        }

        private void ItemSelectedForRename(TreeViewItem item, string text)
        {
            if (currentControl != null)
            {
                G_InnerGrid.Children.Remove(currentControl);
                currentControl = null;
            }
            SimpleRename newControl = new SimpleRename(text, item);
            currentControl = newControl;
            Grid.SetColumn(currentControl, 1);
            G_InnerGrid.Children.Add(currentControl);
        }

        private IFilter GetFilterFromItem(TreeViewItem item)
        {
            if (item.Tag == null)
            {
                if (item.Parent != null && item is TreeViewItem)
                {
                    return GetFilterFromItem((TreeViewItem)item.Parent);
                }
            }

            if (item.Tag is IFilter)
            {
                return (IFilter)item.Tag;
            }

            return null;

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

        private void MI_NewFilter_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem viewItem = new TreeViewItem();
            
            viewItem.Header = "Unnamed Item";
            if (TV_LogOverview.SelectedItem == null)
            {
                viewItem.Selected += ViewItem_Selected;
                TV_LogOverview.Items.Add(viewItem);
                viewItem.Tag = new Filter((string)viewItem.Header);
                return;
            }

            object selectedItem = TV_LogOverview.SelectedItem;
            if (selectedItem is TreeViewItem)
            {
                TreeViewItem item = (TreeViewItem)selectedItem;
                if (GetDepth(item) > 1)
                {
                    return;
                }
                viewItem.Selected += SubViewItem_Selected;
                item.Items.Add(viewItem);
                item.IsExpanded = true;
            }
        }

        private int GetDepth(TreeViewItem item)
        {
            return GetDepth(item, 0);
        }

        private int GetDepth(TreeViewItem item, int startLevel)
        {
            if (item.Parent != null)
            {
                object parent = item.Parent;
                if (parent is TreeViewItem)
                {
                    startLevel += GetDepth((TreeViewItem)parent, startLevel + 1);
                }
            }

            return startLevel;
        }

        private void TV_LogOverview_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TreeView)
            {
                TreeView treeView = (TreeView)sender;
                TreeViewItem item = (TreeViewItem)treeView.SelectedItem;
                if (item != null)
                {
                    item.IsSelected = false;
                    treeView.Focus();
                }

            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            RemoveItemFromTreeView(TV_LogOverview.SelectedItem);
        }

        private void RemoveItemFromTreeView(object item)
        {
            if (item != null)
            {
                if (TV_LogOverview.Items.Contains(item))
                {
                    TV_LogOverview.Items.Remove(item);
                    return;
                }

                if (item is TreeViewItem)
                {
                    TreeViewItem treeViewItem = (TreeViewItem)item;
                    RemoveSubItem(treeViewItem);
                }
            }
        }

        private void RemoveSubItem(TreeViewItem itemToRemove)
        {
            if (itemToRemove.Parent == null)
            {
                return;
            }
            object parentObject = itemToRemove.Parent;

            if (parentObject is TreeViewItem)
            {
                TreeViewItem parentTreeView = (TreeViewItem)parentObject;
                if (parentTreeView.Items.Contains(itemToRemove))
                {
                    parentTreeView.Items.Remove(itemToRemove);
                    return;
                }
                RemoveSubItem(parentTreeView);
            }

            
        }
    }
}
