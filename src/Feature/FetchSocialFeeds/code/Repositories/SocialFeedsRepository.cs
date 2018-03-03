using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hackathon.SocialWall.Feature.FetchSocialFeeds.Models;
using Sitecore.XA.Foundation.Mvc.Repositories.Base;
using Sitecore.XA.Foundation.RenderingVariants.Repositories;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.Repositories
{
    public class SocialFeedsRepository : ListRepository, ISocialFeedsRepository
    {
        public IEnumerable<HashTag> GetHashTags()
        {
            throw new NotImplementedException();
        }

        public override IRenderingModelBase GetModel()
        {
            throw new NotImplementedException();
        }

        public TwitterConfiguration GeTwitterConfiguration()
        {
            throw new NotImplementedException();
        }

        public bool SaveFeeds(List<Post> posts)
        {
            throw new NotImplementedException();
        }

       public long GetMaxTwitterPostId(string hashTag)
        {
            throw new NotImplementedException();
        }
       public long GetMinTwitterPostId(string hashTag)
        {
            throw new NotImplementedException();
        }
    }
}