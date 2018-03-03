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
            bool status = false;
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
                            status = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        newItem.Editing.CancelEdit();
                        Sitecore.Diagnostics.Log.Info("SocialWall-" + newItem.ID + "- Master Mode - exception" + ex, typeof(object));
                        status = false;
                    }
                }

                return status;
            }
            //public bool UpdateFeed(Post post)
            //{
            //    throw new NotImplementedException();
            //}
        }

        public List<Post> GetPosts(int pageNumber, ID socialNetwork, out int? totalCount, bool? isVipContent = null, PostType? postType = null)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetPosts(int pageNumber, SocialNetwork socialNetwork, out int? totalCount, bool? isVipContent = null, PostType? postType = null)
        {
            List<Post> vdoList = new List<Post>();
            Item targetItem = Sitecore.Context.Database.Items["{F750B39A-3601-4AC2-8380-B8ABFE97D4DE}"];
            Item[] postItems = targetItem.GetChildren().ToArray();
            vdoList=postItems.Where(p => (p.Fields["PostSource"].Value).Equals(socialNetwork) && (p.Fields["PostType"]).Equals(postType)).Take(pageNumber).ToList<Post>();
            totalCount = tempList.Count();
            //foreach (var temp in postItems)
            //{
            //    Post oPost = new Post();
            //    oPost.Id = temp.ID;//Should be SItecore ID
            //    oPost.PostSource = new ID(temp.Fields["PostSource"].Value);
            //    oPost.DateApproved = temp.Fields["DateApproved"].Value;
            //    oPost.DateCreated = (temp.Fields["DateCreated"].Value);
            //    oPost.Description = temp.Fields["Description"].Value;
            //    oPost.hashTag = temp.Fields["DateApproved"].Value;
            //    oPost.IsVIPContent = temp.Fields["DateApproved"].Value;
            //    oPost.Likes = temp.Fields["DateApproved"].Value;
            //    oPost.PostDateCreated = temp.Fields["DateApproved"].Value;
            //    oPost.PostSource = temp.Fields["DateApproved"].Value;
            //    oPost.PostType = temp.Fields["DateApproved"].Value;
            //    oPost.PostUrl = temp.Fields["DateApproved"].Value;
            //    oPost.PostUrl = temp.Fields["DateApproved"].Value;
            //    oPost.SocialNetworkUsername = temp.Fields["DateApproved"].Value;
            //    oPost.SocialNetworkUserPictureUrl = temp.Fields["DateApproved"].Value;
            //    oPost.Status = temp.Fields["DateApproved"].Value;
            //    oPost.ThumbnailUrl = temp.Fields["DateApproved"].Value;
            //    HashtagList.Add(oHashtag);
            //}
            return tempList;
        }
    }
}