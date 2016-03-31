using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace security_hackers_it_news.Controllers
{
    /// <summary>
    /// Abstract class holding the reusable methods
    /// </summary>
    class AbstractRESTParser
    {
        object jsonString;
        public object JsonString { get { return jsonString; } private set { JsonString = value; } }

        private HttpClient client;
        /// <summary>
        /// The Http client to try parse rest api 
        /// </summary>
        public HttpClient Client
        {
            get { return client; }
            set { client = value; }
        }

        private string currentURL;
 
        public string CurrentUrl
        {
            get { return currentURL; }
            set { currentURL = value; }
        }

        private HttpResponseMessage httpResponse;

        public HttpResponseMessage HttpRpMsg
        {
            get { return httpResponse; }
            private set { httpResponse = value; }
        }

        public AbstractRESTParser()
        {
            client = new HttpClient();
        }

        protected async void tryParseUrl(string url) {
            currentURL = url;
            try {
                httpResponse = await client.GetAsync(new Uri(currentURL));
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK) {
                    jsonString = await httpResponse.Content.ReadAsStringAsync();
                }
            }
            catch{
                //No internet
            }
        }

        protected object getResultAs(string type) {
            switch (type) {
                case "JsonArray":
                    return JsonValue.Parse((string)jsonString).GetArray();
                default:
                    return jsonString;
            }
        }
    }
}
