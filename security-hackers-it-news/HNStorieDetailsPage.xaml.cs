using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using security_hackers_it_news.Controllers;
using security_hackers_it_news.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace security_hackers_it_news
{
    /// <summary>
    /// Hackers news details page, showing the comment for article
    /// </summary>
    public sealed partial class HNStorieDetailsPage : Page
    {
        HNewsItemModel story;
        HNApiClient hnApiCli;
        ObservableCollection<HNewsItemModel> comments;

        public HNStorieDetailsPage()
        {
            this.InitializeComponent();
            comments = new ObservableCollection<HNewsItemModel>();
            hnApiCli = new HNApiClient();
            CommentsList.Source = comments;
        }
       
        //load comment for entry
        protected async void tryLoadContent() {
            //set the current page details & reformate details
            newsTitleTB.Text = story.title;
            newsDateTB.Text = reformatString(story.date, "\n");
            newsPubliserTb.Text = "Publisher: " + reformatString(story.by, "\n");
            newsUrlTb.Text = "Url: " + reformatString(story.url, "\n");
            newsScoreTb.Text = reformatString(story._lscore, "\n");
            //Show loading
            ProgressRing progressRing1 = new ProgressRing();
            progressRing1.IsActive = true;
            loaderRing.Children.Add(progressRing1);

            //load sub comments
            HNewsItemModel tmpHns;
            int iCnt = 0;//@todo: This should be a setting param
            foreach (string id in story.kids)
            {
                progressRing1.IsActive = false;
                tmpHns = await hnApiCli.getStoryById(id);
                comments.Add(
                    tmpHns
                    );
                //Load 100 articles only
                if (++iCnt == 100) break;
            }
        }

        protected string reformatString(string str, string replaceKey = "") {
            return str.Replace(replaceKey, "");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            //Set the sceen for the back navigation
            SystemNavigationManager systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            systemNavigationManager.BackRequested += OnBackRequested;
            systemNavigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            if (e.Parameter is HNewsItemModel)
            {
                story = (HNewsItemModel)e.Parameter;
                tryLoadContent();
            }
            else
                story = new HNewsItemModel();

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            SystemNavigationManager systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            systemNavigationManager.BackRequested -= OnBackRequested;
            systemNavigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            //Navigate back to the previous page
            Frame.GoBack(new DrillInNavigationTransitionInfo());
        }

        private async void newsUrlTb_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var text = ((TextBlock)sender).Text;
            string url = text.Replace("Url:", "");
            var uri = new Uri(@url);
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);
        }
        
    }

    internal class DrillInNavigationTransitionInfo : NavigationTransitionInfo
    {
    }
}
