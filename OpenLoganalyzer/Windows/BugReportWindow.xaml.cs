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
    /// Interaktionslogik für BugReportWindow.xaml
    /// </summary>
    public partial class BugReportWindow : Window
    {
        public BugReportWindow()
        {
            InitializeComponent();
            TB_Description.Tag = true;
            TB_Subject.Tag = true;

            CorrectStyle(TB_Subject);
            CorrectStyle(TB_Description);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(TextBox))
            {
                return;
            }
            TextBox textBox = (TextBox)sender;
            CorrectStyle(textBox);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(TextBox))
            {
                return;
            }
            TextBox textBox = (TextBox)sender;
            CorrectStyle(textBox);
        }

        private void CorrectStyle(TextBox textBox)
        {
            Style styleToUse = Resources["DefaultTextBox"] as Style;
            if (textBox.Tag == null || textBox.Tag.GetType() != typeof(bool))
            {
                return;
            }

            bool placeholder = (bool)textBox.Tag;

            if (placeholder && !string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Tag = false;
                textBox.Text = string.Empty;
            }

            if (!placeholder && string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Tag = true;
                textBox.Text = "WHUPS!";
            }

            textBox.Style = styleToUse;
            
        }
    }
}
