using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.Models
{
    public class User
    {
        public int Id { get; set; }
        public SocialNetwork PostSource { get; set; }
        public long SocialNetworkUserId { get; set; }
        public string SocialNetworkUsername { get; set; }
        public PostStatus Status { get; set; }
        public bool IsPreviousPosts { get; set; }
        public string InstagramMaxId { get; set; }
    }
}