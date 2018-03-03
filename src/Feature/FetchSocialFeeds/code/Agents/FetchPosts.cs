using Hackathon.SocialWall.Feature.FetchSocialFeeds.Models;
using Hackathon.SocialWall.Feature.FetchSocialFeeds.Repositories;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TweetSharp;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.Agents
{
    public class FetchPosts
    {
        public void Run(Sitecore.Data.Items.Item[] items, Sitecore.Tasks.CommandItem command,
      Sitecore.Tasks.ScheduleItem schedule)
        {
            try
            {
                SocialFeedsRepository socialFeedRepo = (SocialFeedsRepository)ServiceLocator.ServiceProvider.GetService(typeof(ISocialFeedsRepository));




                Log.Info("PostFetching-Scheduler: Event called! at " + DateTime.Now.ToString(), new object());

            }
            catch (Exception ex)
            {
                Log.Info("Exception Thrown in Form Feed Fetch Schedule, Error: " + ex.Message, new object());

            }
        }


      
    }
}