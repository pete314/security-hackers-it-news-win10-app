using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace security_hackers_it_news
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        HNApiClient hnApiCli;
        List<HNewsItemModel> hnModelList;
        public MainPage()
        {
            this.InitializeComponent();
            hnApiCli = new HNApiClient();
            tryLoadContent();
        }

        protected async void tryLoadContent() {
            
            string[] hnTopIds = await hnApiCli.getTopStoriesIdList();
            hnModelList = new List<HNewsItemModel>();
            HNewsItemModel tmpItemModel = await hnApiCli.getStoryById(hnTopIds[0]);
            foreach (string id in hnTopIds)
            {
                tmpItemModel = await hnApiCli.getStoryById(id);
                hnDefaultSp.Children.Add(serializeHnModel(tmpItemModel));
                hnModelList.Add(tmpItemModel);
            }
        }

        private StackPanel serializeHnModel(HNewsItemModel hnim) {
            StackPanel sp = new StackPanel() { Name= "hnim_"+ hnim.id };
            TextBlock titleText = new TextBlock() { Text = hnim.title };
            TextBlock timeText = new TextBlock() { Text = hnim.time };
            TextBlock scoreText = new TextBlock() { Text = hnim.score };

            sp.Children.Add(titleText);
            sp.Children.Add(timeText);
            sp.Children.Add(scoreText);
            return sp;
        }
    }
}
