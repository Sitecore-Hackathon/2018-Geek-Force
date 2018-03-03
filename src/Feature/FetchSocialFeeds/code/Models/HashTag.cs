using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.Models
{
    public class HashTag
    {
        public int Id { get; set; }
        public SocialNetwork PostSource { get; set; }
        public string Hashtag { get; set; }
        public long InstagramMaxId { get; set; }
    }
}