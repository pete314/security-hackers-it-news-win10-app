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
    /// Item model for Redit netsec entries
    /// </summary>
    public class ReditListingsModel
    {
        public ReditListingsModel() { }
        public string id { get; set; }
        public string title { get; set; } //if job
        
        public string score { get; set; }//story score      
        public string _lscore{ get { return "Score:\n" + this.score; } }
        public string comments { get { return num_comment != null ? "Comments:\n" + num_comment : "Comments:\n 0"; } }
        public string num_comment { get; set; }

        public string author { get; set; }
        //The permanent link on redit
        public string permalink { get; set; }
        public string _permalink { get { return "Redit:" + permalink; } }

        public string created { get; set; }

        //the original content url
        public string url { get; set; }
        public string _url
        {
            get { return "Url:" + this.url; }
        }

        //this is a short intro to the entry (mostly empty)
        public string selftext { get; set; }

        public string date { get { return parseTimeStamp(this.created);} }
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
