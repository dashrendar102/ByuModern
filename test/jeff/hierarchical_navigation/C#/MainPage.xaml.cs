using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HierarchicalNavTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            Settings.HelpSettings helpSettingsFlyout = new Settings.HelpSettings();
            // When the settings flyout is opened from the app bar instead of from
            // the setting charm, use the ShowIndependent() method.
            helpSettingsFlyout.ShowIndependent();
            bottomAppBar.IsOpen = false;
        }
    }
}
