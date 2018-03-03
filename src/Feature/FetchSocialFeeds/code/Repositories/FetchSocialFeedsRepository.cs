using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hackathon.SocialWall.Feature.FetchSocialFeeds.Models;
using Sitecore.XA.Foundation.Mvc.Repositories.Base;
using Sitecore.XA.Foundation.RenderingVariants.Repositories;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.Repositories
{
    public class FetchSocialFeedsRepository : ListRepository, IFetchSocialFeedsRepository
    {
        public IEnumerable<HashTag> GetHashTags()
        {
            throw new NotImplementedException();
        }

        public IRenderingModelBase GetModel()
        {
            throw new NotImplementedException();
        }
    }
}