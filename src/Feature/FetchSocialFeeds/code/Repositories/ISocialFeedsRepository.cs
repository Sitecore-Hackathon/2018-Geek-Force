using Hackathon.SocialWall.Feature.FetchSocialFeeds.Models;
using Sitecore.XA.Foundation.Mvc.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.Repositories
{
   public interface ISocialFeedsRepository : IModelRepository
    {
        IEnumerable<HashTag> GetHashTags();
        TwitterConfiguration GeTwitterConfiguration();
        
        long GetMaxTwitterPostId(string hashTag);
        long GetMinTwitterPostId(string hashTag);
        bool SaveFeeds(List<Post> posts);
        List<Post> GetPosts(int pageNumber, SocialNetwork socialNetwork, out int? totalCount, bool? isVipContent = null, PostType? postType = null);
    }
}
