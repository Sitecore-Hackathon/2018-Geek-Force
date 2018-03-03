using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds
{
    public class Constants
    {
        public const string BackendAdmin = "backendAdmin";
    }

    public enum SocialNetwork
    {
        All = 0,
        Twitter = 1,
        Instagram = 2
    }

    public enum PostType
    {
        All = 0,
        Image = 1,
        Video = 2,
        Text = 3
    }
    public enum PostStatus
    {
        All = 0,
        Pending = 1,
        Approved = 2,
        Rejected = 3
    }

    public enum Region
    {
        All = 0,
        KSA = 1,
        Egypt = 2
    }

    public enum CSRType
    {
        RamadanMusicVideo = 1,
        RamadanSpirit = 2
    }

    public enum Status
    {
        Active = 1,
        Inactive = 0
    }

    public enum ApplicationHosting
    {
        Microsite = 1,
        Facebook = 2
    }


    public enum PostUsersType
    {
        All = 0,
        VIP = 1,
        Other = 2
    }
}