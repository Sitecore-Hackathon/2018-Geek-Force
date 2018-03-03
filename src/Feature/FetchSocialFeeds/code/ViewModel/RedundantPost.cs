using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.ViewModel
{
    public class RedundantPost
    {
        public ID PostSource { get; set; }
        public ID PostType { get; set; }
        public string PostUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
        public DateTime PostDateCreated { get; set; }
        public string UserName { get; set; }
    }
}