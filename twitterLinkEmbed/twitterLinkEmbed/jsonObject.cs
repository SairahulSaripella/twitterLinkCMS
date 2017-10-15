using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twitterLinkEmbed
{
    class jsonObject
    {
        public string url { get; set; }
        public string author_name { get; set; }
        public string author_url { get; set; }
        public string html { get; set; }
        public int width { get; set; }
        public object height { get; set; }
        public string type { get; set; }
        public string cache_age { get; set; }
        public string provider_name { get; set; }
        public string provider_url { get; set; }
        public string version { get; set; }
    }
}
