﻿using OpenLoganalyzer.Core.Commands;
using OpenLoganalyzer.Core.Enum;
using OpenLoganalyzer.Core.Extensions;
using OpenLoganalyzer.Core.Interfaces;
using OpenLoganalyzer.Core.Notification;
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
        private delegate void CreatePopUpCallback(PopupData data);
        private delegate string GetTextBoxValueCallback(TextBox textBox);
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

            CB_Labels.SelectedIndex = 0;
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
            B_Send.IsEnabled = false;
            B_Cancel.IsEnabled = false;
            CB_Labels.IsEnabled = false;
        }

        private void CreateIssueCompleted(Task<bool> obj)
        {
            string popupHeadline = "ReportBug_Headline_Error";
            string content = "ReportBug_Content_Error";
            if (obj.Result)
            {
                popupHeadline = "ReportBug_Headline_Success";
                content = "ReportBug_Content_Success";
                content = content.GetTranslated();
                string subject = string.Empty;
                string description = string.Empty;
                this.Dispatcher.Invoke(
                    new Action(() => subject = GetTextBoxValue(TB_Subject))
                );
                this.Dispatcher.Invoke(
                    new Action(() => subject = GetTextBoxValue(TB_Description))
                );
                content = content.Replace("%subject%", subject);
                content = content.Replace("%content%", description);

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
                B_Send.Dispatcher.Invoke(
                    new UpdateEnabledCallback(UpdateIsEnabled),
                    new Object[] { B_Send, true }
                    );
                B_Cancel.Dispatcher.Invoke(
                     new UpdateEnabledCallback(UpdateIsEnabled),
                     new Object[] { B_Cancel, true }
                     );
                CB_Labels.Dispatcher.Invoke(
                     new UpdateEnabledCallback(UpdateIsEnabled),
                     new Object[] { CB_Labels, true }
                     );

                content = content.GetTranslated();
            }
            PopupData data = new PopupData(popupHeadline.GetTranslated(), content, 10000);
            this.Dispatcher.Invoke(
                new CreatePopUpCallback(CreatePopUp),
                new Object[] { data }
            );
        }

        private string GetTextBoxValue(TextBox textBox)
        {
            return textBox.Text;
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

        private void UpdateIsEnabled(Control control, bool state)
        {
            control.IsEnabled = state;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
