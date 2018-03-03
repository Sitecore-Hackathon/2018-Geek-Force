using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hackathon.SocialWall.Feature.FetchSocialFeeds.Models;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.XA.Foundation.Mvc.Repositories.Base;
using Sitecore.XA.Foundation.RenderingVariants.Repositories;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.Repositories
{
    public class SocialFeedsRepository : ListRepository, ISocialFeedsRepository
    {
        public IEnumerable<HashTag> GetHashTags()
        {
            Item targetItem = Sitecore.Context.Database.Items["{81CC0D23-79FB-41DA-A5FB-13CF700F766A}"];
            Item[] hashtagItem = targetItem.GetChildren().ToArray();
            List<HashTag> HashtagList=new List<HashTag>();
            foreach(var temp in hashtagItem)
            {
                HashTag oHashtag = new HashTag();
                oHashtag.Id = temp.ID;
                oHashtag.PostSource = new ID(temp.Fields["PostSource"].Value);
                oHashtag.Hashtag = temp.Fields["Hashtag"].Value;
                HashtagList.Add(oHashtag);
            }
            return HashtagList.AsEnumerable<HashTag>();
        }

        public override IRenderingModelBase GetModel()
        {
            throw new NotImplementedException();
        }

        public TwitterConfiguration GeTwitterConfiguration()
        {
            Item targetItem = Sitecore.Context.Database.Items["{EE197B09-61EA-42CB-B5DE-F0BBAC2D54DF}"];
            TwitterConfiguration twitterconfig = new TwitterConfiguration();
            twitterconfig.ConsumerKey = targetItem.Fields["ConsumerKey"].Value;
            twitterconfig.ConsumerSecret= targetItem.Fields["ConsumerSecret"].Value;
            twitterconfig.Token= targetItem.Fields["Token"].Value;
            twitterconfig.TokenSecret= targetItem.Fields["TokenSecret"].Value;
            return twitterconfig;
        }

        public long GetMaxTwitterPostId(string hashTag)
        {
            throw new NotImplementedException();
        }
        public long GetMinTwitterPostId(string hashTag)
        {
            throw new NotImplementedException();
        }
        public bool SaveFeeds(List<Post> posts)
        {
            throw new NotImplementedException();
        }
        //public bool UpdateFeed(Post post)
        //{
        //    throw new NotImplementedException();
        //}
    }
}