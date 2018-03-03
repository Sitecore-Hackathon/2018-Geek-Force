using Hackathon.SocialWall.Feature.FetchSocialFeeds.Repositories;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.Agents
{
    public class FetchPosts
    {
        public void Run(Sitecore.Data.Items.Item[] items, Sitecore.Tasks.CommandItem command,
      Sitecore.Tasks.ScheduleItem schedule)
        {
            try
            {
                SocialFeedsRepository fetchRepo = (SocialFeedsRepository)ServiceLocator.ServiceProvider.GetService(typeof(ISocialFeedsRepository));
                IEnumerable<Models.HashTag> hashtags = fetchRepo.GetHashTags();

                foreach (var hashtag in hashtags)
                {
                    Posts.AddFromInstagram(hashtag.Hashtag, hashtag.InstagramMaxId);
                }
                Log.Info("PostFetching-Scheduler: Event called! at " + DateTime.Now.ToString(), new object());

            }
            catch (Exception ex)
            {
                Log.Info("Exception Thrown in Form Feed Fetch Schedule, Error: " + ex.Message, new object());

            }
        }
    }
}