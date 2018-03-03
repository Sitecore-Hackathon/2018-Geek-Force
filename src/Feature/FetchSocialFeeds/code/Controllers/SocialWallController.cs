using Hackathon.SocialWall.Feature.FetchSocialFeeds.Repositories;
using Hackathon.SocialWall.Feature.FetchSocialFeeds.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
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

        //public SocialWallController(ISocialFeedsService socialFeedsService)
        //{
        //    this.SocialFeedsService = socialFeedsService;
        //}

        public SocialWallController()
        {
            this.SocialFeedsService = ServiceLocator.ServiceProvider.GetService<ISocialFeedsService>();
        }
        // GET: SocialWall
        protected override object GetModel()
        {
            SocialFeedsService.AddPostsByHashTags();
            return View();
        }
    }
}