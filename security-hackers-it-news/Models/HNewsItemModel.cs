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
        private int id = 0;//Unique id
        private bool deleted = false;//is deleted
        private string type = "story";//story/job/comment/poll/pollopt
        private string by = "John Doe";//publisher name
        private int time = 1;//Unix timestamp
        private string text = "";//the body of the item/comment
        private bool dead = false;//item is dead
        private int parent = 0; //comment parent
        private List<int> kids;
        private string url; //the url for the story
        private string score;
        private string title; //if job
        
    }
}
