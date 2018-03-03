using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.Services
{
    public interface ISocialFeedsService
    {
        void AddPostsByHashTags();
    }
}