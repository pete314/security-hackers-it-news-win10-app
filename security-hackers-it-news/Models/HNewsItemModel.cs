using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace security_hackers_it_news.Models
{
    /// <summary>
    /// Item model for hackers news entries
    /// </summary>
    public class HNewsItemModel
    {
        public HNewsItemModel() { }
        public string title { get; set; } //if job
        
        public string score { get; set; }//story score      
        public string _lscore{ get { return "Score:\n" + this.score; } }
        public string comments { get { return kids != null ? "Comments:\n" + kids.Length.ToString() : "Comments:\n 0"; } }
        
        public string url { get; set; }//the url for the story
        public string _url {
            get { return "Url:" +this.url; }
        }
        public string[] kids { get; set; }//comment childs
        
        public string parent { get; set; } //comment parent
        
        public string dead { get; set; }//item dead
        
        public string id { get; set; }
        
        public string type { get; set; } //story/job/comment/poll/pollopt
        
        public string deleted { get; set; }
        
        public string by { get; set; }//publisher name
        
        public string time { get; set; }//Unix timestamp
        
        public string text { get; set; }
        //Simple removal of html entities and tags
        public string _notagtext {
            get {
                try
                {
                    return WebUtility.HtmlDecode(Regex.Replace(text, "<.*?>", String.Empty));
                }
                catch { }
                return "";
            }
        }
        public string date { get { return parseTimeStamp(this.time);} }
        /// <summary>
        /// Parse string time to string date
        /// </summary>
        /// <param name="tStamp"></param>
        /// <returns></returns>
        private string parseTimeStamp(String tStamp) {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(Convert.ToInt32(tStamp)).ToLocalTime();
            return "Created:\n" + dtDateTime.ToString();
        }
    }
}
