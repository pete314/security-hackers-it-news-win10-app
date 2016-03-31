using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace security_hackers_it_news.Models
{
    /// <summary>
    /// Item model for hackers news entries
    /// </summary>
    class HNewsItemModel
    {
        public string title { get; set; } //if job
        
        public string score { get; set; }//story score      

        


        public string url { get; set; }//the url for the story

        


        public string[] kids { get; set; }//comment childs

        


        public string parent { get; set; } //comment parent

        


        public string dead { get; set; }//item dead

        


        public string id { get; set; }

        

        public string type { get; set; } //story/job/comment/poll/pollopt

        

        public string deleted { get; set; }

        
        public string by { get; set; }//publisher name
        
        public string time { get; set; }//Unix timestamp
        
        public string text { get; set; }

    }
}
