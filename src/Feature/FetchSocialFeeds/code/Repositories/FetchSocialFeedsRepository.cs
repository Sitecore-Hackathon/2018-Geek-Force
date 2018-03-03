using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.XA.Foundation.Mvc.Repositories.Base;
using Sitecore.XA.Foundation.RenderingVariants.Repositories;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.Repositories
{
    public class FetchSocialFeedsRepository : ListRepository, IFetchSocialFeedsRepository
    {
        public IRenderingModelBase GetModel()
        {
            throw new NotImplementedException();
        }
    }
}