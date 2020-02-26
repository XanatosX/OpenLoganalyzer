﻿using System;
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
    /// Interaction logic for SimpleRename.xaml
    /// </summary>
    public partial class SimpleRename : UserControl
    {
        private TreeViewItem item;
        public TreeViewItem Item => item;

        public SimpleRename(string labelName, TreeViewItem treeViewItem)
        {
            InitializeComponent();
            item = treeViewItem;
            TB_NewName.Text = item.Header.ToString();
            L_Label.Content = labelName;
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