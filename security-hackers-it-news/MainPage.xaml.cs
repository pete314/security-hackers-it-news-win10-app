using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace security_hackers_it_news
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        HNApiClient hnApiCli;
        ObservableCollection<HNewsItemModel> news;
        public MainPage()
        {
            this.InitializeComponent();
            hnApiCli = new HNApiClient();
            news = new ObservableCollection<HNewsItemModel>();
            NewsList.Source = news;
            tryLoadContent();
        }

        public async void tryLoadContent()
        {
            string[] hnTopIds = await hnApiCli.getTopStoriesIdList();
            HNewsItemModel tmpHns;
            int iCnt = 0;//@todo: This should be a setting param
            foreach (string id in hnTopIds)
            {
                tmpHns = await hnApiCli.getStoryById(id);
                news.Add(
                    tmpHns
                    );
                //Load 100 articles only
                if (++iCnt == 100) break;
            }
        }

        private async void Url_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var text = ((TextBlock)sender).Text;
            string url = text.Replace("Url:", "");
            var uri = new Uri(@url);
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private void TopNavMenuClicked(object sender, RoutedEventArgs e)
        {
            TopNavMenu.TopNavSplitView.IsPaneOpen = !TopNavMenu.TopNavSplitView.IsPaneOpen;
        }

        private void Title_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var text = ((TextBlock)sender).Text;
            HNewsItemModel tmpModel = findByTitle(text);
            if (tmpModel != null) {
                this.Frame.Navigate(typeof(HNStorieDetailsPage), tmpModel);
            }
            else
                return;
        }

        private HNewsItemModel findByTitle(string title)
        {
            foreach (HNewsItemModel hnim in news)
                if (title == hnim.title)
                    return hnim;
            return null;
        }
    }
}
