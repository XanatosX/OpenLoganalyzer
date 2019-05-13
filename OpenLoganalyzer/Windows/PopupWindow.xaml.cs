﻿using OpenLoganalyzer.Core.Extensions;
using OpenLoganalyzer.Core.Helper;
using OpenLoganalyzer.Core.Interfaces;
using OpenLoganalyzer.Core.Notification;
using OpenLoganalyzer.Core.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Popup.xaml
    /// </summary>
    public partial class PopupWindow : Window
    {
        private delegate void CloseWindowCallback();

        private readonly Timer showTimer;
        private readonly AutoResetEvent autoReset;

        public PopupWindow(ISettings settings, ThemeManager themeManager, PopupData popupData)
        {
            Screen screen = new Screen();

            InitializeComponent();

            this.Left = screen.Width - this.Width;
            this.Top = screen.Height - this.Height;
            this.autoReset = new AutoResetEvent(false);
            this.showTimer = new Timer(CallbackMethod, autoReset, popupData.TimeToShow, 1);


            this.ChangeStyle(settings, themeManager);
        }

        private void CallbackMethod(object state)
        {
            AutoResetEvent localAutoEvent = (AutoResetEvent)state;
            this.Dispatcher.Invoke(new CloseWindowCallback(
                 delegate { this.Close(); }
                 ));
        }
    }
}