using Sitecore.Data;
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

    public struct SocialNetwork
    {
       public static ID Twitter =new ID("{0D45B97A-ED29-4F50-A75C-C9313F69F769}");
       public static ID Instagram = new ID("{EA5E01DA-3280-46FD-A7F2-4587EA2F5B51}");
    }
    public struct PostType
    {
        public static string All = "All";
        public static string Image = "Image";
        public static string Video = "Video";
        public static string Text = "Text";
        
    }

    //public enum PostType
    //{
    //    All = 0,
    //    Image = 1,
    //    Video = 2,
    //    Text = 3
    //}
    public struct PostStatus
    {
        public static string All = "All";
        public static string Pending = "Pending";
        public static string Approved = "Approved";
        public static string Rejected = "Rejected";
    }
    //public enum PostStatus
    //{
    //    All = 0,
    //    Pending = 1,
    //    Approved = 2,
    //    Rejected = 3
    //}

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