using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using security_hackers_it_news.Models;
using Windows.Data.Json;

namespace security_hackers_it_news.Controllers
{
    /// <summary>
    /// Hackers News Api Client
    /// Wrapper class for api requests
    /// </summary>
    class HNApiClient : AbstractRESTParser
    {
        public HNApiClient() : base(){}
        protected const string ENDPOINT_HOST = "https://hacker-news.firebaseio.com/v0";
        protected const string MODIFIER_PRETTY = "?print=pretty";
        //List of top stories
        protected string top_stories_uri = "/topstories.json";
        //Get single story (mind the {0})
        protected string single_story_uri


        protected string buildUrlString(string urlName, bool isPretty = false) {
            return isPretty ? ENDPOINT_HOST + urlName + MODIFIER_PRETTY : ENDPOINT_HOST + urlName; 
        } 

        public async Task<string[]> getTopStoriesIdList() {
            string jsonString = await tryParseUrl(
                    buildUrlString(top_stories_uri)
                );
            
            return JsonConvert.DeserializeObject<string[]>(jsonString);
        }

        public async Task<HNewsItemModel> getStoryById(string id) {
            string jsonString = await tryParseUrl(
                    buildUrlString(top_stories_uri)
                );

            return JsonConvert.DeserializeObject<HNewsItemModel>(jsonString);
        }
    }
}
