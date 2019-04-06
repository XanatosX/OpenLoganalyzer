using OpenLoganalyzer.Core.Commands;
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
        private delegate void UpdateEnabledCallback(Control control, bool state);
        private delegate void CloseWindowCallback();

        public BugReportWindow()
        {
            InitializeComponent();

            InitialStyleSetup(TB_Description);
            InitialStyleSetup(TB_Subject);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(TextBox))
            {
                return;
            }
            TextBox textBox = (TextBox)sender;

            Style styleToUse = Resources["DefaultTextBox"] as Style;
            bool placeholder = (bool)textBox.Tag;

            if (placeholder)
            {
                textBox.Tag = false;
                textBox.Text = string.Empty;
            }

            textBox.Style = styleToUse;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(TextBox))
            {
                return;
            }
            TextBox textBox = (TextBox)sender;

            
            string type = textBox.Name.Replace("TB_", "");
            bool placeholder = (bool)textBox.Tag;
            string text = textBox.Text;

            if (!placeholder && string.IsNullOrEmpty(text))
            {
                textBox.Text = text.GetTranslated("ReportBug" + type);
                textBox.Tag = true;
                textBox.Style = Resources["PlaceholderTextBox"] as Style;
            }

            
        }

        private void InitialStyleSetup(TextBox textBox)
        {
            Style styleToUse = Resources["PlaceholderTextBox"] as Style;
            string type = textBox.Name.Replace("TB_", "");
            string text = string.Empty;
            text = text.GetTranslated("ReportBug" + type);

            textBox.Tag = true;
            textBox.Text = text;
            textBox.Style = styleToUse;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendBugReportCommand command = new SendBugReportCommand("", TB_Subject.Text, TB_Description.Text);
            Task<bool> task = command.AsyncExecute();
            task.ContinueWith(CreateIssueCompleted);

            TB_Description.IsEnabled = false;
            TB_Subject.IsEnabled = false;
        }

        private void CreateIssueCompleted(Task<bool> obj)
        {
            this.Dispatcher.Invoke(new CloseWindowCallback(
                    delegate { this.Close(); }
                    ));
            if (obj.Result)
            {
                this.Dispatcher.Invoke( new CloseWindowCallback( 
                    delegate { this.Close(); }
                    ));
            }
            else
            {
                TB_Description.Dispatcher.Invoke(
                    new UpdateEnabledCallback(UpdateIsEnabled),
                    new Object[] { TB_Description, true }
                    );
                TB_Subject.Dispatcher.Invoke(
                    new UpdateEnabledCallback(UpdateIsEnabled),
                    new Object[] { TB_Subject, true }
                    );
            }
        }

        private void UpdateIsEnabled(Control control, bool state)
        {
            control.IsEnabled = state;
        }
    }
}
