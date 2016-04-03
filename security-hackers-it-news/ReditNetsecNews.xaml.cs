using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using security_hackers_it_news.Controllers;
using security_hackers_it_news.Models;
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

namespace security_hackers_it_news
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReditNetsecNews : Page
    {
        ObservableCollection<ReditListingsModel> news;
        ReditApiClient reditApiCli; 
        public ReditNetsecNews()
        {
            this.InitializeComponent();
            reditApiCli = new ReditApiClient();
            news = new ObservableCollection<ReditListingsModel>();
            NewsList.Source = news;
            tryLoadContent();
        }

        public async void tryLoadContent()
        {
            //Show loading
            ProgressRing progressRing1 = new ProgressRing();
            progressRing1.IsActive = true;
            loaderRing.Children.Add(progressRing1);

            List<ReditListingsModel> latestEntries = await reditApiCli.getNewEtries();
            progressRing1.IsActive = false;
            int iCnt = 0;//@todo: This should be a setting param
            foreach (ReditListingsModel item in latestEntries)
            {
                news.Add(item);
                if (++iCnt == 100) break;
            }
        }


        private async void Url_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var text = ((TextBlock)sender).Text;
            string url = text.Replace("Url:", "");
            openUri(new Uri(@url));
        }

        private void TopNavMenuClicked(object sender, RoutedEventArgs e)
        {
            TopNavMenu.TopNavSplitView.IsPaneOpen = !TopNavMenu.TopNavSplitView.IsPaneOpen;
        }

        private void Permalink_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var text = ((TextBlock)sender).Text;
            string url = "https://www.reddit.com" + text.Replace("Redit:", "");
            openUri(new Uri(@url));
        }

        private async void openUri(Uri u) {
            await Windows.System.Launcher.LaunchUriAsync(u);
        }
        
    }
}
