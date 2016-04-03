using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using security_hackers_it_news.Models;

namespace security_hackers_it_news.Controllers
{
    /// <summary>
    /// Wrapper class for redit api requests
    /// </summary>
    class ReditApiClient : AbstractRESTParser
    {
        public ReditApiClient() : base() { }

        protected const string ENDPOINT_HOST = "https://www.reddit.com/r";

        protected string latestNewsEndpoint = "/netsec.json";

        protected string buildUrlString(string urlName, bool isPretty = false)
        {
            return ENDPOINT_HOST + urlName;
        }

        public async Task<List<ReditListingsModel>> getNewEtries()
        {
            string jsonString = base.checkOfflineContent("NetsecTopStories");
            if (String.IsNullOrEmpty(jsonString))
            {
                jsonString = await tryParseUrl(
                    buildUrlString(latestNewsEndpoint)
                );
                base.createOfflineContent(jsonString, "NetsecTopStories");
            }

            return tryParseEtries(jsonString);
        }

        /// <summary>
        /// The result is voming in the format of {
	//"kind": "Listing",
	//"data": {
	//	"modhash": "",
	//	"children": [{
	//		"kind": "t3",
	//		"data": {...
    // Only interested in "children"."data" segment 
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        protected List<ReditListingsModel> tryParseEtries(string jsonString) {
            dynamic jsonObject = JsonConvert.DeserializeObject(jsonString);
            List<ReditListingsModel> itemsList = new List<ReditListingsModel>();
            foreach(var item in jsonObject.data.children)
            {
                var dataItem = item.data;
                var itemModel = new ReditListingsModel()
                {
                    author = dataItem.author,
                    url = dataItem.url,
                    id = dataItem.id,
                    created = dataItem.created,
                    score = dataItem.score,
                    permalink = dataItem.permalink,
                    title = dataItem.title,
                    num_comment = dataItem.num_comment
                };

                itemsList.Add(itemModel);
            }

            return itemsList;
        }
    }
}
