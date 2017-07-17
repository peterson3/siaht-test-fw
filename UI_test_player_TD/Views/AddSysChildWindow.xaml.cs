using MahApps.Metro.SimpleChildWindow;
using System.Windows;

namespace UI_test_player_TD.Views
{
    /// <summary>
    /// Interaction logic for CoolChildWindow.xaml
    /// </summary>
    public partial class AddSysChildWindow : ChildWindow
    {
        public AddSysChildUC addSysChildW { get; set; }
        public MainWindow mainWindow { get; set; }
        public SysView parentWindow { get; set; }

        public AddSysChildWindow(MainWindow mainWindow, SysView parentWindow)
        {
            this.InitializeComponent();
            this.mainWindow = mainWindow;
            CloseOnOverlay = false;
            addSysChildW = new AddSysChildUC(this, parentWindow, mainWindow);
            mainGrid.Children.Clear();
            mainGrid.Children.Add(addSysChildW);
            this.parentWindow = parentWindow;
        }

       
        private void CloseSec_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
