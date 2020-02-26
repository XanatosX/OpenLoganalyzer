﻿using OpenLoganalyzerLib.Core.Interfaces.Configuration;
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

namespace OpenLoganalyzer.Windows.Controls
{
    /// <summary>
    /// Interaction logic for LogLineControl.xaml
    /// </summary>
    public partial class FilterColumnControl : UserControl
    {
        private TreeViewItem item;
        public TreeViewItem Item => item;

        private readonly IFilterColumn logLine;
        public IFilterColumn LogLine => logLine;

        public FilterColumnControl(IFilterColumn logLine, string text, TreeViewItem item)
        {
            InitializeComponent();
            foreach (string regex in logLine.PossibleRegex)
            {
                LV_RegexView.Items.Add(regex);
            }

            this.logLine = logLine;
            this.item = item;
            L_Label.Content = text;
        }

        private void TB_NewName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                TB_NewName.Text = item.Header.ToString();
                return;
            }
            if (e.Key == Key.Enter)
            {
                item.Header = TB_NewName.Text;
                return;
            }
        }
    }
}
