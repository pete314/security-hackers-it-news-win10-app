using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
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
        protected string single_story_uri = "/item/{0}.json";
        private string Data;

        protected string buildUrlString(string urlName, bool isPretty = false) {
            return isPretty ? ENDPOINT_HOST + urlName + MODIFIER_PRETTY : ENDPOINT_HOST + urlName; 
        } 

        public async Task<string[]> getTopStoriesIdList() {
            string jsonString = checkOfflineContent();
            if (jsonString == null)
            {
                jsonString = await tryParseUrl(
                    buildUrlString(top_stories_uri)
                );
                createOfflineContent(jsonString);
            }

            return JsonConvert.DeserializeObject<string[]>(jsonString);
        }

        public async Task<HNewsItemModel> getStoryById(string id) {
            string jsonString = checkOfflineContent(id);
            if (jsonString == null)
            {
                jsonString = await tryParseUrl(
                        buildUrlString(
                            string.Format(single_story_uri, id))
                    );
                createOfflineContent(jsonString, id);
            }
            //chould use a try catch, but it will return null if error
            return JsonConvert.DeserializeObject<HNewsItemModel>(jsonString);
        }

        private string checkOfflineContent(string id = "") {
            string IStorageFileName = (string)String.Format("HNdata_{0}.json", id);
            var store = IsolatedStorageFile.GetUserStoreForApplication();
            //check if local contetn is older than 15 minutes
            if (store.FileExists(IStorageFileName) && DateTime.Now.AddMinutes(-15) > store.GetLastWriteTime(IStorageFileName)){
                using (var fileStream = new IsolatedStorageFileStream(IStorageFileName, FileMode.Open, store))
                {
                    using (var stream = new StreamReader(fileStream))
                    {
                        return stream.ReadToEnd();
                    }
                }
            }
            else {
                //if file exist and is old(has to be at this point), clean up
                if (store.FileExists(IStorageFileName))
                    store.DeleteFile(IStorageFileName);
                return null;
            }
        }

        private void createOfflineContent(string data, string id = "") {
            string IStorageFileName = String.Format("HNdata_{0}.json", id);
            var store = IsolatedStorageFile.GetUserStoreForApplication();
            try
            {
                using (var fileStream = new IsolatedStorageFileStream(IStorageFileName, FileMode.Create, store))
                {
                    using (var stream = new StreamWriter(fileStream))
                    {
                        stream.Write(data);
                    }
                }
            }
            catch {
               //@todo: notfy user data local storage is not accessible
            }
        }
    }
}
