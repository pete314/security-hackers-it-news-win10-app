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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace security_hackers_it_news
{
    public sealed partial class TopMenuControl : UserControl
    {
        public TopMenuControl()
        {
            this.InitializeComponent();
        }

        private void NavigateSavedEntries(object sender, RoutedEventArgs e)
        {

        }
        private void NavigateHome(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
        }
        private void NavigateHackersNewsPage(object sender, RoutedEventArgs e)
        {

        }

        private void NavigateNetsecPage(object sender, RoutedEventArgs e)
        {

        }

        private void NavigateSettingsPage(object sender, RoutedEventArgs e)
        {

        }
    }
}
