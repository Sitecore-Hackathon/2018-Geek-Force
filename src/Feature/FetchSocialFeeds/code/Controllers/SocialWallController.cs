using Hackathon.SocialWall.Feature.FetchSocialFeeds.Repositories;
using Hackathon.SocialWall.Feature.FetchSocialFeeds.Services;
using Sitecore.XA.Foundation.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.Controllers
{
    public class SocialWallController : StandardController
    {
        // GET: Forms
        protected readonly ISocialFeedsService SocialFeedsService;

        public SocialWallController(ISocialFeedsService socialFeedsService)
        {
            this.SocialFeedsService = socialFeedsService;
        }
        // GET: SocialWall
        protected override object GetModel()
        {
            SocialFeedsService.AddPostsByHashTags();
            return View();
        }
    }
}