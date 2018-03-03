using Hackathon.SocialWall.Feature.FetchSocialFeeds.Models;
using Hackathon.SocialWall.Feature.FetchSocialFeeds.Services;
using Hackathon.SocialWall.Feature.FetchSocialFeeds.ViewModel;
using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.Controllers
{
    public class SocialWallController : System.Web.Http.ApiController
    {
        // GET: Forms
        protected readonly ISocialFeedsService SocialFeedsService;

        public SocialWallController(ISocialFeedsService socialFeedsService)
        {
            this.SocialFeedsService = socialFeedsService;
        }

        public System.Web.Http.Results.JsonResult<FeedData> GetWall()
        {
            return Json(GetModel(1, 1, 1, 1, 1, 1));
        }
        public System.Web.Http.Results.JsonResult<FeedData> GetWall(int? vipTwtrPgNo, int? vipInstaPgNo, int? instaVdoPgNo, int? instaImgsPgNo, int? twtrImgsPgNo, int? twtrTwtsPgNo)
        {
            if (vipTwtrPgNo.HasValue && vipInstaPgNo.HasValue && instaVdoPgNo.HasValue && instaImgsPgNo.HasValue && twtrImgsPgNo.HasValue && twtrTwtsPgNo.HasValue)
            {
                return Json(GetModel(vipTwtrPgNo.Value, vipInstaPgNo.Value, instaVdoPgNo.Value, instaImgsPgNo.Value, twtrImgsPgNo.Value, twtrTwtsPgNo.Value));
            }
            else
            {
                return Json(GetModel(1, 1, 1, 1, 1, 1));
            }
        }


        private FeedData GetModel(int vipTwtrPgNo, int vipInstaPgNo, int instaVdoPgNo, int instaImgsPgNo, int twtrImgsPgNo, int twtrTwtsPgNo)
        {
            int? totalCount;
            List<Post> instaVdosPosts =  Get(instaVdoPgNo, SocialNetwork.Instagram, out totalCount, postType: PostType.Video);
            List<Post> instaImagesPosts = Get(instaImgsPgNo, SocialNetwork.Instagram, out totalCount, false, PostType.Image);
            List<Post> twittrImagesPosts = Get(twtrImgsPgNo, SocialNetwork.Twitter, out totalCount, false, PostType.Image);
            List<Post> twittrtweetsPosts = Get(twtrTwtsPgNo, SocialNetwork.Twitter, out totalCount, false, PostType.Text);

            //Get Instagram videos
            var instaVdos = (from a in instaVdosPosts
                             select new RedundantPost
                             {
                                 Description = a.Description,
                                 ThumbnailUrl = Helper.ToProtocolLessUrl(a.ThumbnailUrl),
                                 PostSource = a.PostSource,
                                 PostType = a.PostType,
                                 PostUrl = a.PostUrl.ToProtocolLessUrl(),//Helper.ToProtocolLessUrl(a.PostUrl),
                                 PostDateCreated = a.PostDateCreated
                             }).ToList();
            instaVdos = instaVdos.GroupBy(x => new { x.PostUrl }).Select(g => g.FirstOrDefault()).ToList();

            //Get Instagram Images
            var instaImages = (from a in instaImagesPosts
                               select new RedundantPost
                               {
                                   Description = a.Description,
                                   ThumbnailUrl = Helper.ToProtocolLessUrl(a.ThumbnailUrl),
                                   PostSource = a.PostSource,
                                   PostType = a.PostType,
                                   PostUrl = Helper.ToProtocolLessUrl(a.PostUrl),
                                   PostDateCreated = a.PostDateCreated,
                                   UserName = "@" + a.SocialNetworkUsername //Change
                               }).ToList();

            //Get Twitter Images
            var twittrImages = (from a in twittrImagesPosts
                                select new RedundantPost
                                {
                                    Description = a.Description,
                                    ThumbnailUrl = a.ThumbnailUrl,
                                    PostSource = a.PostSource,
                                    PostType = a.PostType,
                                    PostUrl = a.PostUrl,
                                    PostDateCreated = a.PostDateCreated,
                                    UserName = "@" + a.SocialNetworkUsername //Change
                                }).ToList();

            var twittrtweets = (from a in twittrtweetsPosts
                                select new RedundantPost
                                {
                                    Description = a.Description,
                                    ThumbnailUrl = a.ThumbnailUrl,
                                    PostSource = a.PostSource,
                                    PostType = a.PostType,
                                    PostUrl = a.PostUrl,
                                    PostDateCreated = a.PostDateCreated,
                                    UserName = "@" + a.SocialNetworkUsername //Change
                                }).ToList();

            FeedData fd = new FeedData(instaVdos, instaImages, twittrImages, twittrtweets, vipTwtrPgNo, vipInstaPgNo, instaVdoPgNo, instaImgsPgNo, twtrImgsPgNo, twtrTwtsPgNo);

            return fd;

        }

        
    }
}