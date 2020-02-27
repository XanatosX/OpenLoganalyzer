using Microsoft.Win32;
using OpenLoganalyzer.Core.Commands;
using OpenLoganalyzer.Core.Extensions;
using OpenLoganalyzer.Core.Interfaces;
using OpenLoganalyzer.Core.Notification;
using OpenLoganalyzer.Core.Settings;
using OpenLoganalyzer.Core.Style;
using OpenLoganalyzer.Windows;
using OpenLoganalyzerLib.Core.Configuration.Loader;
using OpenLoganalyzerLib.Core.Factories.Filter;
using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using OpenLoganalyzerLib.Core.Interfaces.Factories;
using OpenLoganalyzerLib.Core.Interfaces.LogAnalyzing;
using OpenLoganalyzerLib.Core.Loader.Data;
using OpenLoganalyzerLib.Core.LogAnalyzing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace OpenLoganalyzer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private delegate void CreatePopUpCallback(PopupData data);

        private readonly ThemeManager themeManager;

        private readonly ISettingsManager settingsManager;

        private readonly IFilterManager filterManager;

        private ISettings settings;

        private readonly List<string> bindingMapping;

        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        public MainWindow()
        {
            bindingMapping = new List<string>();


            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string themeFolder = appdata + @"\OpenLoganalyzer\Themes\";
            string filterFolder = appdata + @"\OpenLoganalyzer\Filters\";

            appdata += @"\OpenLoganalyzer\settings.json";
            settingsManager = new SettingsManager(appdata);

            IFilterFactory factory = new JsonFilterManagerFactory(filterFolder);
            filterManager = factory.GetFilterManager();
           
            LoadSettings();

            InitializeComponent();

            themeManager = new ThemeManager();
            themeManager.ScanFolder(themeFolder);
            this.ChangeStyle(settings, themeManager);

            settingsManager.Save(settings);

            InitzialSetup();
            BuildMenu();
            SetupFilters();

            DisableLockedFeatures();
        }

        private void BuildViewGrid(IFilter filter)
        {
            List<string> columnNames = new List<string>();
            foreach (ILogLineFilter logLineFilter in filter.LogLineTypes)
            {
                foreach (IFilterColumn column in logLineFilter.FilterColumns)
                {
                    if (!columnNames.Contains(column.Type))
                    {
                        columnNames.Add(column.Type);
                    }
                }
            }

            LV_LogLines.Items.Clear();
            LV_LogLines.View = null;
            bindingMapping.Clear();
            GridView view = new GridView();
            view.AllowsColumnReorder = true;
            int counter = 0;
            foreach (string columnName in columnNames)
            {
                GridViewColumn column = new GridViewColumn();
                GridViewColumnHeader header = new GridViewColumnHeader();
                header.Click += Header_Click;
                header.Content = columnName;
                column.Header = header;
                column.DisplayMemberBinding = new Binding("[" + counter.ToString() + "]");
                view.Columns.Add(column);
                bindingMapping.Add(columnName);
                counter++;
            }
            LV_LogLines.View = view;
        }

        private void Header_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    GridViewColumnHeader realHeader = headerClicked.Column.Header as GridViewColumnHeader;
                    string headerName = realHeader.Content as string;

                    string sortBy = bindingMapping.FindIndex(item => item == headerName).ToString();
                    sortBy = "[" + sortBy + "]";

                    Sort(sortBy, direction);

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(LV_LogLines.Items);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        private void InitzialSetup()
        {
            B_EditFilter.IsEnabled = false;
            GB_Filter.Visibility = Visibility.Hidden;
        }

        private void SetupFilters()
        {
            List<string> filterNames = filterManager.GetAllFilterNames();
            CB_FilterBox.Items.Clear();
            foreach (string filterName in filterNames)
            {
                CB_FilterBox.Items.Add(filterName);
            }
        }

        private void DisableLockedFeatures()
        {
            MI_Style.IsEnabled = false;
            MI_Settings.IsEnabled = false;
            MI_ReportBug.IsEnabled = false;
        }

        private void LoadSettings()
        {
            ISettings newSettings = settingsManager.Load();
            if (newSettings == null)
            {
                newSettings = new Settings();
            }
            settings = newSettings;
            
        }

        private void BuildMenu()
        {
            MI_Style.Items.Clear();
            foreach (StyleDict style in themeManager.Styles)
            {
                MenuItem item = new MenuItem()
                {
                    Style = Resources["SubMenuItem"] as Style,
                    Name = "MI_"+style.Name,
                    Header = style.Name,
                    IsCheckable = true,
                    Tag = style
                };
                if (item.Header.ToString() == settings.GetSetting("theme"))
                {
                    item.IsChecked = true;
                }
                item.Click += Style_Click;
                MI_Style.Items.Add(item);
            }
        }

        private void Style_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(MenuItem))
            {
                return;
            }
            MenuItem item = (MenuItem)sender;
            if (item.Tag == null || item.Tag.GetType() != typeof(StyleDict))
            {
                return;
            }

            StyleDict style = (StyleDict)item.Tag;
            this.ChangeStyle(style.Dictionary);
            settings.AddSetting("theme", style.Name);
            settingsManager.Save(settings);
            BuildMenu();
        }

        private void MI_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CB_FilterBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender.GetType() != typeof(ComboBox))
            {
                return;
            }
            ComboBox box = (ComboBox)sender;
            if (box.Items.Count == 0)
            {
                GB_Filter.Visibility = Visibility.Hidden;
                return;
            }

            GB_Filter.Visibility = Visibility.Visible;
        }

        private void MI_Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow(settingsManager, settings, themeManager);
            settingsWindow.ShowDialog();
        }

        private void MI_ReportBug_Click(object sender, RoutedEventArgs e)
        {
            BugReportWindow bugReportWindow = new BugReportWindow(settings, themeManager);
            bugReportWindow.ShowDialog();
        }

        private void CB_FilterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() != typeof(ComboBox))
            {
                return;
            }
            ComboBox box = (ComboBox)sender;
            if (box.SelectedItem == null)
            {
                B_EditFilter.IsEnabled = false;
            }
            B_EditFilter.IsEnabled = true;

            LoadFile(box);
        }

        private void B_OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            string fileName = openFileDialog.FileName;
            TB_FileName.Text = fileName;

            if (CB_FilterBox.SelectedItem != null && CB_FilterBox.SelectedItem.ToString() != "" )
            {
                LoadFile(CB_FilterBox);
            }
        }

        private void TB_FileName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender.GetType() != typeof(TextBox))
            {
                return;
            }

            TextBox textBox = (TextBox)sender;
            if (textBox.Text == String.Empty)
            {
                GB_Filter.Visibility = Visibility.Hidden;
                return;
            }

            GB_Filter.Visibility = Visibility.Visible;
        }

        private void LoadFile(ComboBox box)
        {
            if (box.SelectedItem == null)
            {
                return;
            }
            IFilter filter = filterManager.GetFilter(box.SelectedItem.ToString());
            if (filter != null)
            {
                BuildViewGrid(filter);
                StreamFileLoader loader = new StreamFileLoader();
                loader.Init(TB_FileName.Text);
                ILogManager logManager = new LogManager();
                logManager.Init(filter);

                LV_LogLines.Items.Clear();
                ViewBase view = LV_LogLines.View;
                GridView realView = (GridView)view;
                foreach (ILogLine line in logManager.GetLogLines(loader.Load()))
                {
                    List<string> currentDataSet = new List<string>();
                    if (line == null)
                    {
                        //@TODO: Think about writing an error?
                        continue;
                    }

                    foreach (GridViewColumn column in realView.Columns)
                    {
                        

                        foreach (string bindingKey in bindingMapping)
                        {
                            
                            string value = line.FilteredLogLine.ContainsKey(bindingKey) ? line.FilteredLogLine[bindingKey] : "";
                            currentDataSet.Add(value);
                        }

                        
                    }
                    LV_LogLines.Items.Add(currentDataSet);
                }
            }
        }

        private void MI_ImportFilter_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();

            ImportFilter(fileDialog.FileName);
        }

        private void ImportFilter(string fileName)
        {
            if (File.Exists(fileName))
            {
                FileInfo fi = new FileInfo(fileName);
                JsonFilterLoader loader = new JsonFilterLoader(fi.DirectoryName);

                string filterName = fi.Name.Replace(fi.Extension, "");

                string messageBoxTitle = "MainWindow_Import_Filter_Message_Title";
                string messageBoxContent = "MainWindow_Import_Filter_Message_Content";

                messageBoxContent = messageBoxContent.GetTranslated();
                messageBoxContent = messageBoxContent.Replace("%name%", filterName);

                if (filterManager.GetFilter(filterName) != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        messageBoxContent,
                        messageBoxTitle.GetTranslated(),
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning
                        );

                    if (result != MessageBoxResult.Yes)
                    {
                        return;
                    }
                }

                IFilter filterToImport = loader.LoadFilterByName(filterName);
                string title = "MainWindow_Load_Filter_Headline_Success";
                string content = "MainWindow_Load_Filter_Content_Success";

                bool loadingDone = true;
                if (filterToImport == null || !filterManager.SaveFilter(filterToImport))
                {
                    title = "MainWindow_Load_Filter_Headline_Failed";
                    content = "MainWindow_Load_Filter_Content_Failed";
                    loadingDone = false;
                }

                content = content.GetTranslated();
                content = content.Replace("%file%", fileName);
                PopupData data = new PopupData(title.GetTranslated(), content, 5000);
                ShowPopupWindow window = new ShowPopupWindow(settings, themeManager, data);
                this.Dispatcher.Invoke(
                    new CreatePopUpCallback(CreatePopUp),
                    new Object[] { data }
                );

                if (loadingDone)
                {
                    SetupFilters();
                }
            }
        }

        private void MI_ExportFilter_Click(object sender, RoutedEventArgs e)
        {
            //SaveFileDialog fileDialog = new SaveFileDialog();
            //fileDialog.ShowDialog();
        }

        private void CreatePopUp(PopupData data)
        {
            ShowPopupWindow issueCreated = new ShowPopupWindow(
                settings,
                themeManager,
                data
                );
            issueCreated.Execute();
        }

        private void B_EditOrCreateFilter_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(Button))
            {
                return;
            }

            Button button = (Button)sender;
            bool IsEdit = button.Tag.ToString().ToLower() == "true";
            IFilter filter = null;
            if (IsEdit)
            {
                filter = filterManager.GetFilter(CB_FilterBox.SelectedItem.ToString());
            }
            AddOrEditFilterWindow addOrEditFilter = new AddOrEditFilterWindow(settings, filterManager, themeManager, filter);
            addOrEditFilter.ShowDialog();
            if (IsEdit)
            {
                IFilter newOrEditedFilter = addOrEditFilter.Filter;
                if (newOrEditedFilter == null)
                {
                    return;
                }

                BuildViewGrid(newOrEditedFilter);
                foreach (var item in CB_FilterBox.Items)
                {
                    string itemName = item.ToString();
                    if (itemName == newOrEditedFilter.Name)
                    {
                        CB_FilterBox.SelectedItem = item;
                        break;
                    }
                }
                LoadFile(CB_FilterBox);
            }
        }

        private void MI_EditFilter_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditFilterWindow addOrEditFilter = new AddOrEditFilterWindow(settings, filterManager, themeManager);
            addOrEditFilter.ShowDialog();
        }
    }
}
