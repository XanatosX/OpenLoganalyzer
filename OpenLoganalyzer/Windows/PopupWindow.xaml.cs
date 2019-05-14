using OpenLoganalyzer.Core.Extensions;
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

        private readonly Screen screen;

        public PopupWindow(ISettings settings, ThemeManager themeManager, PopupData popupData)
        {
            screen = new Screen();

            InitializeComponent();

            this.ChangeStyle(settings, themeManager);

            this.autoReset = new AutoResetEvent(false);
            this.showTimer = new Timer(CallbackMethod, autoReset, popupData.TimeToShow, 1);

            this.L_Headline.Content = popupData.Title;
            this.TB_Body.Text = popupData.Content;
        }

        public void SetPosition()
        {
            this.Left = screen.Width - this.ActualWidth;
            this.Top = screen.Height - this.ActualHeight;
        }

        private void CallbackMethod(object state)
        {
            AutoResetEvent localAutoEvent = (AutoResetEvent)state;
            this.Dispatcher.Invoke(new CloseWindowCallback(
                 delegate { this.Close(); }
                 ));
        }

        private void B_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RowDefinition_Loaded(object sender, RoutedEventArgs e)
        {
            SetPosition();
        }
    }
}