using OpenLoganalyzer.Core.Interfaces.Adapter;
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
    /// Interaction logic for SimpleRename.xaml
    /// </summary>
    public partial class SimpleRename : UserControl
    {
        private IFilterAdapter apdater;
        public IFilterAdapter Adapter => apdater;

        public SimpleRename(string labelName, IFilterAdapter adapter)
        {
            InitializeComponent();
            apdater = adapter;
            TB_NewName.Text = apdater.GetName();
            L_Label.Content = labelName;
        }

        public void AddUserControl(UserControl userControlToAdd)
        {
            if (userControlToAdd == null)
            {
                return;
            }
            SP_MainPanel.Children.Add(userControlToAdd);

        }

        private void TB_NewName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                TB_NewName.Text = apdater.GetName();
                return;
            }
            if (e.Key == Key.Enter)
            {
                apdater.SetName(TB_NewName.Text);
                return;
            }
        }
    }
}
