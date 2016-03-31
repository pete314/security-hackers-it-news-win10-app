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
        private string title; //if job

        public string Title
        {
            get { return title; }
            set { title = value; }
        }


        private string score;//story score      

        public string Score
        {
            get { return score; }
            set { score = value; }
        }


        private string url;//the url for the story

        public string Url
        {
            get { return url; }
            set { url = value; }
        }


        private List<int> kids;//comment childs

        public List<int> Kids
        {
            get { return kids; }
            set { kids = value; }
        }


        private int parent; //comment parent

        public int Parent
        {
            get { return parent; }
            set { parent = value; }
        }


        private bool dead;//item dead

        public bool Dead
        {
            get { return dead; }
            set { dead = value; }
        }


        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string type; //story/job/comment/poll/pollopt

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private bool deleted;

        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }

        private string by;//publisher name

        public string By
        {
            get { return by; }
            set { by = value; }
        }

        private long time;//Unix timestamp

        public long Time
        {
            get { return time; }
            set { time = value; }
        }

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

    }
}
