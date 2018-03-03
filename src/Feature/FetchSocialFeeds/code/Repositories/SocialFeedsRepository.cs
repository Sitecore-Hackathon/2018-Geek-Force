using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hackathon.SocialWall.Feature.FetchSocialFeeds.Models;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
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
            List<HashTag> HashtagList = new List<HashTag>();
            foreach (var temp in hashtagItem)
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
            twitterconfig.ConsumerSecret = targetItem.Fields["ConsumerSecret"].Value;
            twitterconfig.Token = targetItem.Fields["Token"].Value;
            twitterconfig.TokenSecret = targetItem.Fields["TokenSecret"].Value;
            return twitterconfig;
        }

        public long GetMaxTwitterPostId(string hashTag)
        {
            Item targetItem = Sitecore.Context.Database.Items["{F750B39A-3601-4AC2-8380-B8ABFE97D4DE}"];
            Item[] posts = targetItem.GetChildren().ToArray();
            return System.Convert.ToInt64(posts.Select(p => p.Fields["SocialNetworkPostId"]).Max());
        }
        public long GetMinTwitterPostId(string hashTag)
        {
            Item targetItem = Sitecore.Context.Database.Items["{F750B39A-3601-4AC2-8380-B8ABFE97D4DE}"];
            Item[] posts = targetItem.GetChildren().ToArray();
            return System.Convert.ToInt64(posts.Select(p => p.Fields["SocialNetworkPostId"]).Min());

        }
        public bool SaveFeeds(List<Post> posts)
        {
            var master = Sitecore.Configuration.Factory.GetDatabase("master");
            Item parentItem = master.Items["//sitecore/content/Hackathon/GeekForce/Data/Social Wall Folder/Post Folder"];
            TemplateItem template = master.GetTemplate("{D1654215-CF06-4FA7-B8E7-D64B24DBB24C}");
            using (new SecurityDisabler())
            {
                foreach(Post post in posts)
                {
                    Item newItem = parentItem.Add("Post-" + DateTime.Now.ToString("yyyyMMddHHmmss"), template);
                    try
                    {
                        if (newItem != null)
                        {
                            newItem.Editing.BeginEdit();
                            newItem["PostSource"] = post.PostSource.ToString();
                            newItem["PostType"] = post.PostType.ToString();
                            newItem["SocialNetworkPostId"] = post.SocialNetworkPostId;
                            newItem["SocialNetworkUserId"] = post.SocialNetworkUserId.ToString();
                            newItem["SocialNetworkUsername"] = post.SocialNetworkUsername;
                            newItem["SocialNetworkUserPictureUrl"] = post.SocialNetworkUserPictureUrl;
                            newItem["PostUrl"] = post.PostUrl;
                            newItem["ThumbnailUrl"] = post.ThumbnailUrl;
                            newItem["Description"] = post.Description;
                            newItem["PostDateCreated"] = DateUtil.ToIsoDate(post.PostDateCreated);
                            newItem["DateApproved"] = post.DateApproved;//DateUtil.ToIsoDate(post.DateApproved); SHOULD BE DATE IN MODEL
                            //newItem["TimeDifference"] = post.TimeDifference;
                            newItem["Status"] = post.Status.ToString();
                            newItem["Likes"] = post.Likes.ToString();
                            newItem["DateCreated"] = DateUtil.ToIsoDate(post.DateCreated);
                            newItem["hashTag"] = post.hashTag;
                            newItem.Editing.EndEdit();
                        }
                    }
                    catch (Exception ex)
                    {
                        newItem.Editing.CancelEdit();
                        Sitecore.Diagnostics.Log.Info("SMEForm-" + newItem.ID + "- Master Mode - exception" + ex, typeof(object));
                    }
                }
                
                throw new NotImplementedException();
            }
            //public bool UpdateFeed(Post post)
            //{
            //    throw new NotImplementedException();
            //}
        }
    }
}