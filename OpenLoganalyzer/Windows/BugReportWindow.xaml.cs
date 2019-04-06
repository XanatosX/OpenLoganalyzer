using OpenLoganalyzer.Core.Extensions;
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
            TB_Description.Tag = false;
            TB_Description.Text = string.Empty;
            TB_Subject.Tag = false;
            TB_Subject.Text = string.Empty;

            CorrectStyle(TB_Subject, false);
            CorrectStyle(TB_Description, false);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(TextBox))
            {
                return;
            }
            TextBox textBox = (TextBox)sender;
            CorrectStyle(textBox, true);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(TextBox))
            {
                return;
            }
            TextBox textBox = (TextBox)sender;
            CorrectStyle(textBox, false);
        }

        private void CorrectStyle(TextBox textBox, bool gotFocus)
        {
            Style styleToUse = Resources["DefaultTextBox"] as Style;
            if (textBox.Tag == null || textBox.Tag.GetType() != typeof(bool))
            {
                return;
            }

            bool placeholder = (bool)textBox.Tag;
            string type = textBox.Name.Replace("TB_", "");
            string text = string.Empty;

            if (placeholder && gotFocus && textBox.Text != text.GetTranslated("ReportBug" + type))
            {
                textBox.Tag = false;
            }

            if (!placeholder && !gotFocus && string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Tag = true;
                
                text = text.GetTranslated("ReportBug" + type);
                styleToUse = Resources["PlaceholderTextBox"] as Style;
            }

            textBox.Text = text;
            textBox.Style = styleToUse;
            
        }
    }
}
