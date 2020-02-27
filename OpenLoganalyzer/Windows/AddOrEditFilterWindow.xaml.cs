using OpenLoganalyzer.Core.Adapter;
using OpenLoganalyzer.Core.Interfaces;
using OpenLoganalyzer.Core.Interfaces.Adapter;
using OpenLoganalyzer.Core.Style;
using OpenLoganalyzer.Windows.Controls;
using OpenLoganalyzerLib.Core.Configuration;
using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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

        private readonly List<IFilter> filterToRemove;

        public IFilter Filter => filterToEdit;
        private readonly IFilter filterToEdit;

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

            filterToRemove = new List<IFilter>();
            this.settings = settings;
            this.filterManager = filterManager;
            this.themeManager = themeManager;
            this.filterToEdit = filterToEdit;

            SetupFilters();
            currentControl = null;
        }

        private void SetupFilters()
        {
            foreach (IFilter loadedFilter in filterManager.GetAllFilters())
            {
                TreeViewItem viewItem = CreateTreeViewEntry(loadedFilter.Name, "\\Images\\Icons\\FilterIcon.png");
                viewItem.Selected += ViewItem_Selected;
                viewItem.Tag = loadedFilter;

                if (loadedFilter == null)
                {
                    return;
                }

                foreach (ILogLineFilter logLineFilter in loadedFilter.LogLineTypes)
                {
                    TreeViewItem subViewItem = CreateTreeViewEntry(logLineFilter.Name);
                    subViewItem.Selected += SubViewItem_Selected;
                    viewItem.Items.Add(subViewItem);
                    foreach (IFilterColumn column in logLineFilter.FilterColumns)
                    {
                        TreeViewItem filterColumnItem = CreateTreeViewEntry(column.Type);
                        filterColumnItem.Selected += FilterColumnItem_Selected; ;

                        subViewItem.Items.Add(filterColumnItem);
                    }
                }

                if (loadedFilter.Name == filterToEdit.Name)
                {
                    viewItem.IsSelected = true;
                    ExtendTreeView(viewItem);
                }
                TV_LogOverview.Items.Add(viewItem);

            }
        }

        private void ExtendTreeView(TreeViewItem treeViewItem)
        {
            treeViewItem.IsExpanded = true;
            foreach (object subItem in treeViewItem.Items)
            {
                if (subItem is TreeViewItem)
                {
                    ExtendTreeView((TreeViewItem)subItem);
                }
            }
        }

        private void FilterColumnItem_Selected(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            ItemSelectedForRename((TreeViewItem)sender, "Column name:");
        }

        private void SubViewItem_Selected(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            ItemSelectedForRename((TreeViewItem)sender, "Logline name:");
        }

        private void ViewItem_Selected(object sender, RoutedEventArgs e)
        {
            ItemSelectedForRename((TreeViewItem)sender, "Filter name:");
        }

        private TreeViewItem CreateTreeViewEntry(string itemName)
        {
            return CreateTreeViewEntry(itemName, string.Empty);
        }

        private TreeViewItem CreateTreeViewEntry(string itemName, string imageToUse)
        {
            TreeViewItem returnItem = new TreeViewItem();

            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            string appPath = Application.ResourceAssembly.Location;
            FileInfo fileInfo = new FileInfo(appPath);
            if (imageToUse != string.Empty)
            {
                Uri imagePath = new Uri(fileInfo.DirectoryName + imageToUse);
                BitmapImage bitmap = new BitmapImage(imagePath);
                Image treeViewImage = new Image
                {
                    Source = bitmap,
                    Height = 16
                };

                stackPanel.Children.Add(treeViewImage);
            }

            TextBlock text = new TextBlock
            {
                Text = itemName
            };
            stackPanel.Children.Add(text);

            returnItem.Header = stackPanel;

            return returnItem;
        }

        private void ItemSelectedForRename(TreeViewItem item, string text)
        {
            if (currentControl != null)
            {
                G_InnerGrid.Children.Remove(currentControl);
                currentControl = null;
            }
            IFilter currentFilter = GetFilterFromItem(item);
            int Level = GetDepth(item);
            IFilterAdapter adapter = null;

            StackPanel panel = (StackPanel)item.Header;
            TextBlock block = GetTextBlock(panel);
            UserControl userControlToAdd = null;

            switch (Level)
            {
                case 0:
                    adapter = new FilterAdapter(currentFilter, block);
                    break;
                case 1:
                    string logName = block.Text;
                    adapter = new LogLineAdapter(currentFilter.GetLogLineFilterByName(logName), block);
                    break;
                case 2:
                    string columnName = block.Text;
                    TreeViewItem parent = (TreeViewItem)item.Parent;
                    string logScope = GetTextBlock((StackPanel)parent.Header).Text;
                    ILogLineFilter filter = currentFilter.GetLogLineFilterByName(logScope);
                    IFilterColumn column = filter.GetColumnByName(columnName);
                    adapter = new ColumnAdapter(column, block);
                    userControlToAdd = new FilterColumnControl(column);

                    break;
                default:
                    break;
            }
            SimpleRename newControl = new SimpleRename(text, adapter);
            newControl.AddUserControl(userControlToAdd);
            currentControl = newControl;
            Grid.SetColumn(currentControl, 1);
            G_InnerGrid.Children.Add(currentControl);
        }

        private TextBlock GetTextBlock(StackPanel stackPanel)
        {
            TextBlock block = null;
            foreach (UIElement children in stackPanel.Children)
            {
                if (children is TextBlock)
                {
                    block = (TextBlock)children;
                    break;
                }
            }
            return block;
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
            foreach (IFilter toRemove in filterToRemove)
            {
                filterManager.DeleteFilter(toRemove);
            }
            filterToRemove.Clear();

            foreach (TreeViewItem item in TV_LogOverview.Items)
            {
                IFilter filter = (IFilter)item.Tag;
                if (!filterManager.SaveFilter(filter))
                {
                    //Error Handling!
                }
            }
            this.Close();
        }

        private void MI_NewFilter_Click(object sender, RoutedEventArgs e)
        {
            string text = "Unnamed Item";
            TreeViewItem viewItem = CreateTreeViewEntry(text, "\\Images\\Icons\\FilterIcon.png");
            if (TV_LogOverview.SelectedItem == null)
            {
                viewItem.Selected += ViewItem_Selected;
                TV_LogOverview.Items.Add(viewItem);
                viewItem.Tag = new Filter(text);
                return;
            }

            object selectedItem = TV_LogOverview.SelectedItem;
            if (selectedItem is TreeViewItem)
            {
                TreeViewItem item = (TreeViewItem)selectedItem;
                IFilter filter = GetFilterFromItem(item);
                int depth = GetDepth(item);
                if (depth > 1)
                {
                    return;
                }
                viewItem = CreateTreeViewEntry(text);
                if (depth == 0)
                {
                    filter.AddFilter(new FilterLine(text));
                    viewItem.Selected += SubViewItem_Selected;
                }
                if (depth == 1)
                {
                    string filterLine = GetTextBlock((StackPanel)item.Header).Text;
                    IFilterColumn column = new FilterColumn(text);
                    filter.GetLogLineFilterByName(filterLine).AddColumn(column);
                    viewItem.Selected += FilterColumnItem_Selected;
                }

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
                    startLevel = GetDepth((TreeViewItem)parent, startLevel + 1);
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
                if (item is TreeViewItem)
                {
                    TreeViewItem treeViewItem = (TreeViewItem)item;
                    if (TV_LogOverview.Items.Contains(item))
                    {
                        TV_LogOverview.Items.Remove(item);

                        filterToRemove.Add((IFilter)treeViewItem.Tag);
                        return;
                    }
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
                int depth = GetDepth(itemToRemove);
                IFilter filter = GetFilterFromItem(itemToRemove);
                if (depth == 1)
                {
                    filter.RemoveFilterLineByName(GetTextBlock((StackPanel)itemToRemove.Header).Text);
                }
                if (depth == 2)
                {
                    ILogLineFilter lineFilter = filter.GetLogLineFilterByName(GetTextBlock((StackPanel)parentTreeView.Header).Text);
                    lineFilter.RemoveColumnByType(GetTextBlock((StackPanel)itemToRemove.Header).Text);
                }
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
