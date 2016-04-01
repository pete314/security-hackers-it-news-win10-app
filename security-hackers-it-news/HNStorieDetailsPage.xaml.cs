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
        }
        public HNStorieDetailsPage(HNewsItemModel story)
        {
            this.InitializeComponent();
            comments = new ObservableCollection<HNewsItemModel>();
            hnApiCli = new HNApiClient();
            this.story = story;
        }

        //load comment for entry

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            SystemNavigationManager systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            systemNavigationManager.BackRequested += OnBackRequested;
            systemNavigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
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
    }

    internal class DrillInNavigationTransitionInfo : NavigationTransitionInfo
    {
    }
}
