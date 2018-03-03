using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.Models
{
    public class Post
    {
        public int Id { get; set; }
        public SocialNetwork PostSource { get; set; }
        public PostType PostType { get; set; }
        public string SocialNetworkPostId { get; set; }
        public long SocialNetworkUserId { get; set; }
        public string SocialNetworkUsername { get; set; }
        public string SocialNetworkUserPictureUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
        public DateTime PostDateCreated { get; set; }
        public string DateApproved { get; set; }
        public PostStatus Status { get; set; }
        public int Likes { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsVIPContent { get; set; }
        public string hashTag { get; set; }
    }
}