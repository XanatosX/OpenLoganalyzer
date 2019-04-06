using OpenLoganalyzer.Core.Commands;
using OpenLoganalyzer.Core.Enum;
using OpenLoganalyzer.Core.Extensions;
using OpenLoganalyzer.Core.Interfaces;
using OpenLoganalyzer.Core.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private readonly ThemeManager themeManager;
        private readonly ISettings settings;

        public BugReportWindow(ISettings settings, ThemeManager themeManager)
        {
            this.themeManager = themeManager;
            this.settings = settings;
            
            InitializeComponent();

            this.ChangeStyle(settings, themeManager);

            FillComboBox();
            InitialStyleSetup(TB_Description);
            InitialStyleSetup(TB_Subject);

            
        }

        private void FillComboBox()
        {
            foreach (LabelsEnum value in Enum.GetValues(typeof(LabelsEnum)))
            {
                string[] split = Regex.Split(value.ToString(), @"(?<!^)(?=[A-Z])");
                string labelName = string.Join(" ", split);
                ComboBoxItem comboBoxItem = new ComboBoxItem(){
                    Tag = labelName,
                    Content = value.ToString().GetTranslated()
                };

                CB_Labels.Items.Add(comboBoxItem);
            }

            CB_Labels.SelectedItem = 0;
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
                text = "ReportBug" + type;
                textBox.Text = text.GetTranslated();
                textBox.Tag = true;
                textBox.Style = Resources["PlaceholderTextBox"] as Style;
            }

            
        }

        private void InitialStyleSetup(TextBox textBox)
        {
            Style styleToUse = Resources["PlaceholderTextBox"] as Style;
            string type = textBox.Name.Replace("TB_", "");
            string text = "ReportBug" + type; ; 
            text = text.GetTranslated();

            textBox.Tag = true;
            textBox.Text = text;
            textBox.Style = styleToUse;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)CB_Labels.SelectedItem;
            string label = item.Tag.ToString();
            label = label.ToLower() == "none" ? "" : label;
            SendBugReportCommand command = new SendBugReportCommand("", label, TB_Subject.Text, TB_Description.Text);
            Task<bool> task = command.AsyncExecute();
            task.ContinueWith(CreateIssueCompleted);

            TB_Description.IsEnabled = false;
            TB_Subject.IsEnabled = false;
        }

        private void CreateIssueCompleted(Task<bool> obj)
        {
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
