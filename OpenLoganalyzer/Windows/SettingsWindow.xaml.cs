﻿using OpenLoganalyzer.Core.Extensions;
using OpenLoganalyzer.Core.Interfaces;
using OpenLoganalyzer.Core.Style;
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
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly ISettingsManager settingsManager;

        private ISettings settings;

        private readonly ThemeManager themeManager;

        public SettingsWindow(ISettingsManager settingsManager, ISettings settings, ThemeManager themeManager)
        {
            this.settingsManager = settingsManager;
            this.settings = settings;
            this.themeManager = themeManager;

            this.ChangeStyle(settings, themeManager);

            InitializeComponent();
        }
    }
}
