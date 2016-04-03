using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
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

        protected async Task<string> tryParseUrl(string url) {
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
            return (string)jsonString;
        }

        protected object getResultAs(string type) {
            switch (type) {
                case "JsonArray":
                    return JsonValue.Parse((string)jsonString).GetArray();
                default:
                    return jsonString;
            }
        }

        /// <summary>
        /// Check offline version in isolated storage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string checkOfflineContent(string id = "")
        {
            string IStorageFileName = (string)String.Format("data_{0}.json", id);
            var store = IsolatedStorageFile.GetUserStoreForApplication();
            //check if local contetn is older than 15 minutes
            if (store.FileExists(IStorageFileName) && DateTime.Now.AddMinutes(-15) < store.GetLastWriteTime(IStorageFileName))
            {
                using (var fileStream = new IsolatedStorageFileStream(IStorageFileName, FileMode.Open, store))
                {
                    using (var stream = new StreamReader(fileStream))
                    {
                        return stream.ReadToEnd();
                    }
                }
            }
            else {
                //clean up
                if (store.FileExists(IStorageFileName))
                    store.DeleteFile(IStorageFileName);
                return null;
            }
        }

        protected void createOfflineContent(string data, string id = "")
        {
            string IStorageFileName = String.Format("data_{0}.json", id);
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
            catch
            {
                //@todo: notify user data local storage is not accessible
            }
        }
    }
}
