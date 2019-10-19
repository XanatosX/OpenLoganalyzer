using Microsoft.Win32;
using OpenLoganalyzer.Core;
using OpenLoganalyzer.Core.Commands;
using OpenLoganalyzer.Core.Extensions;
using OpenLoganalyzer.Core.Filter;
using OpenLoganalyzer.Core.Interfaces;
using OpenLoganalyzer.Core.Notification;
using OpenLoganalyzer.Core.Settings;
using OpenLoganalyzer.Core.Style;
using OpenLoganalyzer.Windows;
using OpenLoganalyzerLib.Core.Configuration;
using OpenLoganalyzerLib.Core.Configuration.Loader;
using OpenLoganalyzerLib.Core.Configuration.Saver;
using OpenLoganalyzerLib.Core.Factories;
using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using OpenLoganalyzerLib.Core.Loader;
using OpenLoganalyzerLib.Core.Loader.Data;
using System;
using System.Collections.Generic;
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
        private readonly ThemeManager themeManager;

        private readonly ISettingsManager settingsManager;

        private readonly IFilterManager filterManager;

        private ISettings settings;

        public MainWindow()
        {
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
            this.SizeToContent = SizeToContent.Manual;

            InitzialSetup();
            BuildMenu();
            SetupFilters();
        }

        private void InitzialSetup()
        {
            L_Filter.Visibility = Visibility.Hidden;
            CB_FilterBox.Visibility = Visibility.Hidden;
        }

        private void SetupFilters()
        {
            List<string> filterNames = filterManager.GetAvailableFilterNames();
            CB_FilterBox.Items.Clear();
            foreach (string filterName in filterNames)
            {
                CB_FilterBox.Items.Add(filterName);
            }
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

        private void MI_Open_Click(object sender, RoutedEventArgs e)
        {

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
                box.Visibility = Visibility.Hidden;
                L_Filter.Visibility = Visibility.Visible;
                return;
            }
            
            box.Visibility = Visibility.Visible;
            L_Filter.Visibility = Visibility.Visible;
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
            if (box.SelectedItem.GetType() == typeof(FileInfo))
            {
                FileInfo fileInfo = (FileInfo)box.SelectedItem;

                StreamFileLoader loader = new StreamFileLoader();
                string fileName = TB_FileName.Text;
                loader.Init(fileName);
                LV_LogLines.ItemsSource = loader.Load();
            }

        }

        private void ApplyLogLineConfiguration(FileInfo fileInfo)
        {
            //filterManager.
        }

        private void B_OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            string fileName = openFileDialog.FileName;
            TB_FileName.Text = fileName;
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
                L_Filter.Visibility = Visibility.Hidden;
                CB_FilterBox.Visibility = Visibility.Hidden;
                return;
            }

            L_Filter.Visibility = Visibility.Visible;
            CB_FilterBox.Visibility = Visibility.Visible;
        }
    }
}
