using Microsoft.Win32;
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

namespace OpenLoganalyzer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string defaultTheme;

        public MainWindow()
        {
            defaultTheme = "DarkTheme";
            InitializeComponent();

            ChangeStyle(defaultTheme);
            Style = Resources["WindowStyle"] as Style;
        }

        private void ChangeStyle(string themeName)
        {
            Resources.MergedDictionaries.Clear();
            try
            {
                Uri uri = new Uri("/OpenLoganalyzer;component/Styles/" + themeName + ".xaml", UriKind.Relative);
                Resources.MergedDictionaries.Add(new ResourceDictionary { Source = uri });
            }
            catch (Exception)
            {
                ChangeStyle(defaultTheme);
            }
        }

        private void MI_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
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
    }
}
